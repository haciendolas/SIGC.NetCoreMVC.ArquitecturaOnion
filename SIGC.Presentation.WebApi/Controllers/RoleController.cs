using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Features.Role.Queries.RolePagination;
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
    }
}
