using Microsoft.Extensions.Configuration;
using ProjectsAppMvc.Models.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectsAppMvc.Messaging.Consumers
{
    public class ProjectUpdatedConsumer : ITypedConsumer<ProjectUpdated>
    {
        private IConfiguration _configuration;
        public ProjectUpdatedConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task ConsumeAsync(ProjectUpdated message, CancellationToken cancellationToken)
        {
            var consumerHelpers = new ConsumerHelpers(_configuration);
            await consumerHelpers.Consume(message.Id).ConfigureAwait(false);
        }
    }
}