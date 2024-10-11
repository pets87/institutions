using InstitutionsService.Util;
using RabbitMQ.Client;
using System.Text;
namespace InstitutionsService.RabbitMQ
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private const string EXCHANGE_NAME = "InstitutionServiceExchange";
        private readonly string rabbitMQConnectionString;
        private readonly ILogger<RabbitMqClient> logger;
        public RabbitMqClient(IConfiguration configuration, ILogger<RabbitMqClient> logger)
        {
            this.logger = logger;
            rabbitMQConnectionString = GetConfiguration(ConfigurationVariables.RABBIT_CONNECTIONSTRING, configuration) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(rabbitMQConnectionString))
            {
                var host = GetConfiguration(ConfigurationVariables.RABBIT_HOST, configuration);
                var port = GetConfiguration(ConfigurationVariables.RABBIT_PORT, configuration);
                var user = GetConfiguration(ConfigurationVariables.RABBIT_USER, configuration);
                var password = GetConfiguration(ConfigurationVariables.RABBIT_PASSWORD, configuration);
                if(!string.IsNullOrWhiteSpace(host) &&
                    !string.IsNullOrWhiteSpace(port) &&
                    !string.IsNullOrWhiteSpace(user) &&
                    !string.IsNullOrWhiteSpace(password))
                    rabbitMQConnectionString = $"amqp://{user}:{password}@{host}:{port}";
            }

            if (string.IsNullOrWhiteSpace(rabbitMQConnectionString))
                throw new InvalidOperationException("Rabbit configuration not found. Either add environment variables or in appsettings.json. " +
                    $"Needed configuration is: ('{ConfigurationVariables.RABBIT_CONNECTIONSTRING}') or " +
                    $"('{ConfigurationVariables.RABBIT_HOST}' and '{ConfigurationVariables.RABBIT_PORT}' and '{ConfigurationVariables.RABBIT_USER}' and '{ConfigurationVariables.RABBIT_PASSWORD}')");
        }

        public static string GetQueueName(string system, string environment) => $"{system}_{environment}";
        private static string GetRoutingName(string queueName) => $"{queueName}_routing";
        private static string? GetConfiguration(string name, IConfiguration configuration)
        {
            //1. Try to read configuration from environment variable
            var conf = Environment.GetEnvironmentVariable(name);

            if (string.IsNullOrWhiteSpace(conf))
            {
                //2. try to read from appsettings.json
                conf = configuration[name];
            }

            return conf;
        }


        private IConnection? connection = null;
        public IConnection GetConnection()
        {
            if (connection != null && connection.IsOpen)
            {
                return connection;
            }
            ConnectionFactory factory = new();
            factory.Uri = new Uri(rabbitMQConnectionString);
            factory.ClientProvidedName = "InstitutionService";

            connection = factory.CreateConnection();
            return connection;
        }

        public bool CreateQueues(List<string> queueNames)
        {
            if (!queueNames.Any())
            {
                logger.LogInformation($"Creating queues skipped. Nothing to create.");
                return true; 
            }
            logger.LogInformation($"Creating queues. {string.Join(",", queueNames)}");
            try
            {
                using (IConnection con = GetConnection())
                {
                    using (IModel channel = con.CreateModel())
                    {
                        var queueType = new Dictionary<string, object>
                        {
                            { "x-queue-type", "quorum" }  //use quorum queue for better consistency https://www.rabbitmq.com/docs/quorum-queues
                        };

                        foreach (string queueName in queueNames)
                        {
                            var routingKey = GetRoutingName(queueName);
                            channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct);
                            channel.QueueDeclare(queueName, true, false, false, queueType); // make it also durable, so it will not lose data on restart
                            channel.QueueBind(queueName, EXCHANGE_NAME, routingKey, null);
                        }

                        channel.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Creating queues failed. {string.Join(",", queueNames)}");
                return false;
            }
            logger.LogInformation($"Creating queues succed. {string.Join(",", queueNames)}");
            return true;
        }

        public bool Publish(string system, string environment, string data)
        {
            logger.LogInformation($"Publishing to system. system: '{system}', environment: '{environment}', data: {data}");
            try
            {
                using (IConnection con = GetConnection())
                {
                    using (IModel channel = con.CreateModel())
                    {

                        byte[] messageBytes = Encoding.UTF8.GetBytes(data);

                        var routingKey = GetRoutingName(GetQueueName(system, environment));
                        channel.BasicPublish(EXCHANGE_NAME, routingKey, null, messageBytes);

                        channel.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Publishing to system failed. system: '{system}', environment: '{environment}', data: {data}");
                return false;
            }
            logger.LogInformation($"Publishing to system succeed. system: '{system}', environment: '{environment}', data: {data}");
            return true;
        }
    }
}
