using Microsoft.Extensions.Configuration;
using ProjectsAppMvc.Models.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectsAppMvc.Messaging.Consumers
{
    public class ProjectCreatedConsumer : ITypedConsumer<ProjectCreated>
    {
        private IConfiguration _configuration;
        public ProjectCreatedConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task ConsumeAsync(ProjectCreated message, CancellationToken cancellationToken)
        {
            var consumerHelpers = new ConsumerHelpers(_configuration);
            await consumerHelpers.Consume(message.Id).ConfigureAwait(false);
        }


    }
}