using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.DurableTask.ContextImplementations;
using Microsoft.Azure.WebJobs.Extensions.DurableTask.Options;
using Microsoft.Extensions.Options;

namespace KhumaloCraft.Services

{
    public class DurableClientFactory : IDurableClientFactory
    {
        private readonly IOptions<DurableTaskOptions> _options;
        private readonly IDurableClientFactory _durableClientFactory;

        public DurableClientFactory(IOptions<DurableTaskOptions> options, IDurableClientFactory durableClientFactory)
        {
            _options = options;
            _durableClientFactory = durableClientFactory;
        }

        public IDurableClient CreateClient()
        {
            return _durableClientFactory.CreateClient(new DurableClientOptions
            {
                TaskHub = _options.Value.HubName,
                ConnectionName = _options.Value.AzureStorageConnectionStringName,
                NotificationUrl = _options.Value.NotificationUrl
            });
        }

        public IDurableClient CreateClient(DurableClientOptions options)
        {
            return _durableClientFactory.CreateClient(options);
        }
    }
}
