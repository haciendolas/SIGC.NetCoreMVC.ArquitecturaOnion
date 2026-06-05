using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentChangeState;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentCreate;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentUpdate;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentGet;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentList;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentPagination;
using SIGC.DomainModel.Dtos.Establishment;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using SIGC.Infrastructure.GeneralService.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{ 
    public class EstablishmentController : BaseController
    {
        [HttpPost("EstablishmentCreate")]
        [SwaggerOperation(Summary = "Crear un establecimiento", Description = "Permite crear un establecimiento.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EstablishmentCreate([FromForm] EstablishmentCreateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile);
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("EstablishmentUpdate")]
        [SwaggerOperation(Summary = "Editar un establecimiento", Description = "Permite editar un establecimiento.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EstablishmentUpdate([FromForm] EstablishmentUpdateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile); 
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("EstablishmentChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado del establecimiento", Description = "Permite cambiar el estado del establecimiento.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EstablishmentChangeState([FromBody] EstablishmentChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpGet("EstablishmentGet/{EstablishmentID}")]
        [SwaggerOperation(Summary = "Obtener un establecimiento por Id", Description = "Permite obtener un establecimiento por id.")]
        [ProducesResponseType(typeof(MsgResponse<EstablishmentGetResponseDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EstablishmentGet([FromRoute] int EstablishmentID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new EstablishmentGetQueryRequest(EstablishmentID), CancellationToken));
        }

        [HttpGet("EstablishmentList/{PersonID}")]
        [SwaggerOperation(Summary = "Listar los establecimiento de la empresa", Description = "Permite listar los establecimientos de la empresa.")]
        [ProducesResponseType(typeof(MsgResponse<List<EstablishmentListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EstablishmentList([FromRoute] int? PersonID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new EstablishmentListQueryRequest(PersonID), CancellationToken));
        }

        [HttpPost("EstablishmentPagination")]
        [SwaggerOperation(Summary = "Paginación de establecimiento", Description = "Permite la paginación de establecimiento.")]
        [ProducesResponseType(typeof(MsgResponse<PaginationResultDto<EstablishmentPaginationQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EstablishmentPagination([FromQuery] EstablishmentPaginationQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query, CancellationToken));
        }
    }
}
