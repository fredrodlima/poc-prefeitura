using Microsoft.Extensions.Configuration;
using ProjectsAppMvc.Models.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectsAppMvc.Messaging.Consumers
{
    public class ProjectPhaseCreatedConsumer : ITypedConsumer<ProjectPhaseCreated>
    {
        private IConfiguration _configuration;
        public ProjectPhaseCreatedConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task ConsumeAsync(ProjectPhaseCreated message, CancellationToken cancellationToken)
        {
            var consumerHelpers = new ConsumerHelpers(_configuration);
            await consumerHelpers.Consume(message.ProjectId).ConfigureAwait(false);
        }
    }
}