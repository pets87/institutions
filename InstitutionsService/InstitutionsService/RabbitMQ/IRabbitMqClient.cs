using InstitutionsService.Models;
using RabbitMQ.Client;

namespace InstitutionsService.RabbitMQ
{
    public interface IRabbitMqClient
    {
        IConnection GetConnection();
        bool CreateQueues(List<string> queueNames);
        bool Publish(string system, string environment, string data);
    }
}
