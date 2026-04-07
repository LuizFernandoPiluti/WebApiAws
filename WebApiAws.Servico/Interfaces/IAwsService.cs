

namespace WebApiAws.Servico.Interfaces
{
    public interface IAwsService
    {
        public Task<bool> SendMessageAsync(string message);
        public Task<string> ReceiveMessageAsync();
    }
}
