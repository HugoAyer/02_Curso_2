using Microsoft.AspNetCore.Mvc;
using Curso_1.Models;
using Newtonsoft.Json;
using Curso_1.Authorization;
using Curso_1.Utils;

namespace Curso_1.Controllers
{
    [Route("api/ObtainToken")]
    [ApiController]
    public class ObtainTokenController : Controller
    {
        public IConfiguration _configuration;

        public ObtainTokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Post([FromBody] string content)
        {
            try
            {
                ConnectionParams _data = JsonConvert.DeserializeObject<ConnectionParams>(content);
                return Ok(TokenContext.CreateToken(_configuration,_data));
            }
            catch(Exception ex)
            {
                return BadRequest(new Respuesta
                {
                    _code = Constants.ErrorGenConnecting,
                    _message = ex.Message
                });
            }
        }
    }
}
