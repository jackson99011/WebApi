using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Datas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private SmtpSettings _smtpSettings;

        public TestController(IOptionsSnapshot<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        // GET: api/<TestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("~/config", Name = "GetConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<SmtpSettings> GetConfig()
        {
            if (this._smtpSettings == null)
            {
                return NotFound();
            }

            return Ok(this._smtpSettings);
        }
    }
}
