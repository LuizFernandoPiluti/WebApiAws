

using Microsoft.Extensions.Options;
using WebApiAws.Dominio.Settings;
using WebApiAws.Infra.Aws;
using WebApiAws.Servico.Interfaces;

namespace WebApiAws.Servico.servicos
{
    public class AwsService : IAwsService
    {
        private readonly Settings _settings;
        private readonly IClientAwsService _clientAwsService;
        public AwsService(IOptions<Settings> options, IClientAwsService clientAwsService ) 
        {
            _settings = options.Value;
            _clientAwsService = clientAwsService;
        } 
        public async Task<string> ReceiveMessageAsync()
        {
            try
            {
               var msg =  await _clientAwsService.ReceiveMessageAsync(_settings.Uri,_settings.Region).ConfigureAwait(false);
                return msg;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> SendMessageAsync(string message)
        {
            try
            {
               bool statusEnvio = await _clientAwsService.SendMessageAsync(_settings.Uri,_settings.Region, message).ConfigureAwait(false);
                return statusEnvio;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
