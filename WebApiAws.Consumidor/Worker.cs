
using Microsoft.Extensions.Hosting;
using WebApiAws.Servico.Interfaces;

namespace WebApiAws.Consumidor
{
    public class Worker : IHostedService
    {
        private readonly IAwsService _awsService;
        public Worker(IAwsService awsService)
        {
            _awsService = awsService;
                
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true) {
                var msg = await _awsService.ReceiveMessageAsync().ConfigureAwait(false);
                Console.WriteLine(msg);
            }   
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping...");
            return Task.CompletedTask;
        }
    
    }
}
