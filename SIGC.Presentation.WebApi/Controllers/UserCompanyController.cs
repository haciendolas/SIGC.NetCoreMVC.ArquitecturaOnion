using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.UserCompanyFeatures.Commands.UserCompanyChangeState;
using SIGC.ApplicationService.Features.UserCompanyFeatures.Queries.UserCompanyGet;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{  
    public class UserCompanyController : BaseController
    {
        [HttpPut("UserCompanyChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado del usuario", Description = "Permite cambiar el estado de usuario.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UserCompanyChangeState([FromBody] UserCompanyChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
        
        [HttpGet("UserCompanyGet/{UserID}/{CompanyID}")]
        [SwaggerOperation(Summary = "Obtener un usuario por su id y empresa", Description = "Permite obtener un usuario por su id y empresa.")]
        [ProducesResponseType(typeof(MsgResponse<UserCompanyGetQueryResponse?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UserCompanyGet([FromRoute] int UserID, [FromRoute]  int CompanyID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new UserCompanyGetQueryRequest(UserID, CompanyID), CancellationToken));
        }
    }
}