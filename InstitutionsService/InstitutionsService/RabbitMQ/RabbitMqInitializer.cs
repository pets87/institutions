using InstitutionsService.Services;
using System.Runtime.CompilerServices;

namespace InstitutionsService.RabbitMQ
{
    /// <summary>
    /// Used in startup, because we cannot use scoped services inside singletone, but RabbitMqClient need to load classifiers for createing queues
    /// </summary>
    public static class RabbitMqInitializer
    {
        public static async Task Run(IRabbitMqClient rabbitMqClient, IClassifierService classifierService) 
        {
            var classifierGroups = new List<string> { Util.Constants.CLASSIFIER_GROUP_REPLICAITON_ENV, Util.Constants.CLASSIFIER_GROUP_REPLICAITON_SYSTEM };
            var classifiers = await classifierService.GetClassifiersByGroups(classifierGroups);
            var systems = classifiers.Where(x => x.Group == Util.Constants.CLASSIFIER_GROUP_REPLICAITON_SYSTEM).ToList();
            var environments = classifiers.Where(x => x.Group == Util.Constants.CLASSIFIER_GROUP_REPLICAITON_ENV).ToList();

            var queueNames = new List<string>();
            foreach (var system in systems)
            {
                foreach (var environment in environments)
                {
                    queueNames.Add(RabbitMqClient.GetQueueName(system.Name, environment.Name));
                }
            }

            rabbitMqClient.CreateQueues(queueNames);
        }
    }
}
