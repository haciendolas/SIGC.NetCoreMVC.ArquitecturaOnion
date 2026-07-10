using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos; 
using SIGC.ApplicationService.Features.CatalogFeatures.Queries.CatalogPagination; 
using SIGC.Infrastructure.CrossCutting.Wrappers; 
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{ 
    public class CatalogController : BaseController
    {
        /*
        [HttpPost("CatalogCreate")]
        [SwaggerOperation(Summary = "Crear un establecimiento", Description = "Permite crear un establecimiento.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogCreate([FromForm] CatalogCreateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile);
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("CatalogUpdate")]
        [SwaggerOperation(Summary = "Editar un establecimiento", Description = "Permite editar un establecimiento.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogUpdate([FromForm] CatalogUpdateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile); 
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("CatalogChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado del establecimiento", Description = "Permite cambiar el estado del establecimiento.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogChangeState([FromBody] CatalogChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpGet("CatalogGet/{CatalogID}")]
        [SwaggerOperation(Summary = "Obtener un establecimiento por Id", Description = "Permite obtener un establecimiento por id.")]
        [ProducesResponseType(typeof(MsgResponse<CatalogGetResponseDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogGet([FromRoute] int CatalogID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CatalogGetQueryRequest(CatalogID), CancellationToken));
        }

        [HttpGet("CatalogList/{PersonID}")]
        [SwaggerOperation(Summary = "Listar los establecimiento de la empresa", Description = "Permite listar los establecimientos de la empresa.")]
        [ProducesResponseType(typeof(MsgResponse<List<CatalogListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogList([FromRoute] int? PersonID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CatalogListQueryRequest(PersonID), CancellationToken));
        }
        */
        [HttpPost("CatalogPagination")]
        [SwaggerOperation(Summary = "Paginación de catalogo", Description = "Permite la paginación de catalogo.")]
        [ProducesResponseType(typeof(MsgResponse<PaginationResultDto<CatalogPaginationQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogPagination([FromQuery] CatalogPaginationQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query, CancellationToken));
        }
    }
}
