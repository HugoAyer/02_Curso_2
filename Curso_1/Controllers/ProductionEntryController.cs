using Curso_1.Authorization;
using Curso_1.DataActions;
using Curso_1.Models;
using Curso_1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Curso_1.Controllers
{
    [Route("api/ProductionEntry")]
    public class ProductionEntryController : Controller
    {
        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add([FromBody]string content)
        {
            try
            {
                ProductionEntry _data = JsonConvert.DeserializeObject<ProductionEntry>(content);
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                ConnectionParams connectionParams = IdentityContext.getCompanyParams(identity);
                Respuesta respuesta = DataActions.ConnectionContext.connectSAP(connectionParams);

                if (respuesta._code != Constants.OkCode)
                {
                    return Ok(respuesta);
                }

                Respuesta response = ProductionEntryContext.Add(_data);

                return (int.Parse(response._code) < Constants.OkNum) ? BadRequest(response) : Ok(response);
            }
            catch (Exception ex)
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
