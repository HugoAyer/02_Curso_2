using Microsoft.AspNetCore.Mvc;
using Curso_1.Models;
using Newtonsoft.Json;
using Curso_1.Authorization;
using Curso_1.Utils;
using Curso_1.DataActions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Connections;
using System.Xml.Linq;

namespace Curso_1.Controllers
{
    [Route("api/WorkOrders")]
    [ApiController]
    public class WorkOrdersController : Controller
    {
        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add([FromBody] string content)
        {
            try
            {            
            WorkOrder _data = JsonConvert.DeserializeObject<WorkOrder>(content);

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ConnectionParams connectionParams = IdentityContext.getCompanyParams(identity);
            Respuesta respuesta = DataActions.ConnectionContext.connectSAP(connectionParams);

            if (respuesta._code != Constants.OkCode)
            {
                return Ok(respuesta);
            }

                Respuesta response = WorkOrderContext.Add(_data);

                if(response._code != Constants.OkCode) 
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }

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

        [HttpPost("Get")]
        [Authorize]
        public IActionResult Get(string content)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                ConnectionParams connectionParams = IdentityContext.getCompanyParams(identity);
                Respuesta respuesta = DataActions.ConnectionContext.connectSAP(connectionParams);

                if (respuesta._code != Constants.OkCode)
                {
                    return Ok(respuesta);
                }

                Respuesta response = WorkOrderContext.Get(int.Parse(content));
                if (response._code == Constants.ErrorGenConnecting) return NotFound(response);
                else return Ok(response);

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
