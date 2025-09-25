using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleChangeState;
using SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleCreate;
using SIGC.ApplicationService.Features.RoleFeatures.Queries.RolePagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{ 
    public class RoleController : BaseController
    {        
        [HttpPost("RolePagination")]
        [SwaggerOperation(Summary = "Paginación de rol", Description = " Permite la paginación de rol.")]
        [ProducesResponseType(typeof(MsgResponse<PaginationResultDto<RolePaginationQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RolePagination([FromQuery] RolePaginationQueryRequest Query, CancellationToken CancellationToken)
        {
            var Response = await Mediator.Send(Query);
            return Ok(Response);
        }
        
        [HttpPut("RoleChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado del rol", Description = " Permite cambiar el estado de rol.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoleChangeState([FromBody] RoleChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {
            var Response = await Mediator.Send(Command);
            return Ok(Response);
        }

        [HttpPost("RoleCreate")]
        [SwaggerOperation(Summary = "Crear un role ", Description = " Permite crear un rol.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoleCreate([FromBody] RoleCreateCommandRequest Command, CancellationToken CancellationToken)
        {
            var Response = await Mediator.Send(Command);
            return Ok(Response);
        }
    }
}
