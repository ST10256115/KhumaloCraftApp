using KhumaloCraft.Models;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading.Tasks;

namespace KhumaloCraft.Services
{
    public class OrderService
    {
        private readonly IDurableOrchestrationClient _durableOrchestrationClient;

        public OrderService(IDurableOrchestrationClient durableOrchestrationClient)
        {
            _durableOrchestrationClient = durableOrchestrationClient;
        }

        public async Task<string> PlaceOrderAsync(OrderData orderData)
        {
            string instanceId = await _durableOrchestrationClient.StartNewAsync("OrderOrchestrator", orderData);
            return instanceId;
        }
    }
}
