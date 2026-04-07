

namespace WebApiAws.Infra.Aws
{

    public interface IClientAwsService
    {
        public Task<string> ReceiveMessageAsync(string UriSQS,string region);
        public Task<bool> SendMessageAsync(string UriSQS, string region,string Mensagem);

    }

}
