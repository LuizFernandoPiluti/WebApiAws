
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace WebApiAws.Infra.Aws
{

    public class ClientAwsService : IClientAwsService
    {

        public async Task<string> ReceiveMessageAsync(string UriSQS,string region)
        {
            try
            {
                string msg = string.Empty;
                var regionEndpoint = ResolveRegion(region);

                var ClientAwsService = new AmazonSQSClient(regionEndpoint);
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = UriSQS
                };

            
                var response = await ClientAwsService.ReceiveMessageAsync(request);
                if (response != null)
                {
                    foreach (var mensagem in response.Messages)
                    {

                        await ClientAwsService.DeleteMessageAsync(UriSQS, mensagem.ReceiptHandle);
                        msg =  mensagem.Body;

                    }
                }
                else
                {
                    msg =  string.Empty;
                }
                return msg;
           
                

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> SendMessageAsync(string UriSQS,string region, string Mensagem)
        {
            try
            {
                var regionEndpoint = ResolveRegion(region);
                var client = new AmazonSQSClient(regionEndpoint);
                var request = new SendMessageRequest
                {
                    QueueUrl = UriSQS,
                    MessageBody = Mensagem
                };

                await client.SendMessageAsync(request);
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private RegionEndpoint ResolveRegion(string region)
        {
            var regionEndpoint = region switch
            {
                "sa-east-1" => RegionEndpoint.SAEast1,
                _ => throw new ArgumentException("Região inválida!")
            };
            return regionEndpoint;
        }
    }
}



