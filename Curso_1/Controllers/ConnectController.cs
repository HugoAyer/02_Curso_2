using Microsoft.AspNetCore.Mvc;
using Curso_1.Models;
using Newtonsoft.Json;
using Curso_1.Authorization;
using Curso_1.Utils;
using Curso_1.DataActions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Connections;

namespace Curso_1.Controllers
{
    [Route("api")]
    [ApiController]
    public class ConnectController : Controller
    {
        [HttpGet("Connect")]
        [Authorize]
        public IActionResult Connect()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                ConnectionParams connectionParams = IdentityContext.getCompanyParams(identity);
                return Ok(DataActions.ConnectionContext.connectSAP(connectionParams));
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
