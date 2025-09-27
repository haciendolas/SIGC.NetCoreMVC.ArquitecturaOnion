using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.RolePermissionFeatures.Queries.RolePermissionList;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class RolePermissionController : BaseController
    {      
        [HttpPost("RolePermissionList")]
        [SwaggerOperation(Summary = "Listar los permisos por usuario y empresa", Description = "Permite listar los permisos por usuario y empresa.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RolePermissionList([FromBody] RolePermissionListQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query, CancellationToken));
        }
    }
}