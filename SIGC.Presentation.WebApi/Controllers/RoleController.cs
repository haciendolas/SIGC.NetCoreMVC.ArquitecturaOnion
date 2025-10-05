using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleChangeState;
using SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleCreate;
using SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleUpdate;
using SIGC.ApplicationService.Features.RoleFeatures.Queries.RoleGet;
using SIGC.ApplicationService.Features.RoleFeatures.Queries.RolePagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{ 
    public class RoleController : BaseController
    {

        [HttpPost("RoleCreate")]
        [SwaggerOperation(Summary = "Crear un rol", Description = " Permite crear un rol.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoleCreate([FromBody] RoleCreateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("RoleUpdate")]
        [SwaggerOperation(Summary = "Editar un rol", Description = " Permite editar un rol.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoleUpdate([FromBody] RoleUpdateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("RoleChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado del rol", Description = "Permite cambiar el estado de rol.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoleChangeState([FromBody] RoleChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {           
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpGet("RoleGet/{RoleID}")]
        [SwaggerOperation(Summary = "Obtener un rol por Id", Description = "Permite obtener un rol por id.")]
        [ProducesResponseType(typeof(MsgResponse<RoleGetQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoleGet([FromRoute] int RoleID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new RoleGetQueryRequest(RoleID), CancellationToken));
        }

        [HttpPost("RolePagination")]
        [SwaggerOperation(Summary = "Paginación de rol", Description = "Permite la paginación de rol.")]
        [ProducesResponseType(typeof(MsgResponse<PaginationResultDto<RolePaginationQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RolePagination([FromQuery] RolePaginationQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query, CancellationToken));
        }
    }
}
