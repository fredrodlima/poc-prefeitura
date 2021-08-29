using Microsoft.Extensions.Configuration;
using ProjectsAppMvc.Models.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectsAppMvc.Messaging.Consumers
{
    public class ProjectPhaseUpdatedConsumer : ITypedConsumer<ProjectPhaseUpdated>
    {
        private IConfiguration _configuration;
        public ProjectPhaseUpdatedConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task ConsumeAsync(ProjectPhaseUpdated message, CancellationToken cancellationToken)
        {
            var consumerHelpers = new ConsumerHelpers(_configuration);
            await consumerHelpers.Consume(message.ProjectId).ConfigureAwait(false);
        }
    }
}