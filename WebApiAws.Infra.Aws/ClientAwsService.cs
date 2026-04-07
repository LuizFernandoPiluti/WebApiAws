using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace WebApiAws.Infra.Aws
{

    public class ClientAwsService : IClientAwsService
    {
        private readonly string _awsAccessKeyId;
        private readonly string _awsSecretAccessKey;
        public ClientAwsService()
        {
            _awsAccessKeyId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            _awsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
        }

        public async Task<string> ReceiveMessageAsync(string UriSQS,string region)
        {
            try
            {
                string msg = string.Empty;
                var regionEndpoint = ResolveRegion(region);

                var credentials = new BasicAWSCredentials(_awsAccessKeyId, _awsSecretAccessKey);
                var client = new AmazonSQSClient(credentials, regionEndpoint);
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = UriSQS
                };

            
                var response = await client.ReceiveMessageAsync(request);
                if (response.Messages != null)
                {
                    foreach (var mensagem in response.Messages)
                    {

                        await client.DeleteMessageAsync(UriSQS, mensagem.ReceiptHandle);
                        msg =  mensagem.Body;

                    }
                }
                else
                {
                    msg =  "Nenhuma mensagem a ser processada";
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
     

                var credentials = new BasicAWSCredentials(_awsAccessKeyId, _awsSecretAccessKey);
                var client = new AmazonSQSClient(credentials,regionEndpoint);
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



