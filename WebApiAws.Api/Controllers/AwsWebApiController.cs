using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiAws.Api.Request;
using WebApiAws.Servico.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiAws.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwsWebApiController : ControllerBase
    {
        private readonly IAwsService _awsService;
        public AwsWebApiController(IAwsService awsService)
        {
            _awsService = awsService;
        }

        // GET: api/<AwsWebApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AwsWebApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AwsWebApiController>
        [HttpPost]
        public async Task<IActionResult> Post(MensagemRequest request)
        {
            await _awsService.SendMessageAsync(request.Mensagem);
            return Ok(request.Mensagem);
        }

        // PUT api/<AwsWebApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AwsWebApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
