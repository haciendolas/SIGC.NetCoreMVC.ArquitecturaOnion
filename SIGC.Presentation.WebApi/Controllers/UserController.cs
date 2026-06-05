using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Features.UserFeatures.Commands.UserCreate;
using SIGC.ApplicationService.Features.UserFeatures.Commands.UserUpdate;
using SIGC.ApplicationService.Features.UserFeatures.Queries.UserPagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using SIGC.Infrastructure.GeneralService.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{  
    public class UserController : BaseController
    {
        [HttpPost("UserCreate")]
        [SwaggerOperation(Summary = "Crear un usuario", Description = "Permite crear un usuario.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UserCreate([FromForm] UserCreateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile);
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("UserUpdate")]
        [SwaggerOperation(Summary = "Editar un usuario", Description = "Permite editar un usuario.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UserUpdate([FromForm] UserUpdateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile);
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPost("UserPagination")]
        [SwaggerOperation(Summary = "Paginación de usuario", Description = "Permite la paginación de usuario.")]
        [ProducesResponseType(typeof(MsgResponse<PaginationResultDto<UserPaginationQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UserPagination([FromQuery] UserPaginationQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query, CancellationToken));
        }
    }
}