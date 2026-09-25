using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogChangeState;
using SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogCreate;
using SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogUpdate;
using SIGC.ApplicationService.Features.CatalogFeatures.Queries.CatalogGet;
using SIGC.ApplicationService.Features.CatalogFeatures.Queries.CatalogPagination;
using SIGC.DomainModel.Dtos.Catalog;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using SIGC.Infrastructure.GeneralService.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{ 
    public class CatalogController : BaseController
    {        
        [HttpPost("CatalogCreate")]
        [SwaggerOperation(Summary = "Crear un producto", Description = "Permite crear un producto.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogCreate([FromForm] CatalogCreateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile);
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
   
        [HttpPut("CatalogUpdate")]
        [SwaggerOperation(Summary = "Editar un producto", Description = "Permite editar un producto.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogUpdate([FromForm] CatalogUpdateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile); 
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
      
        [HttpPut("CatalogChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado del producto", Description = "Permite cambiar el estado del producto.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogChangeState([FromBody] CatalogChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {
           return Ok(await Mediator.Send(Command, CancellationToken));
        }
      
        [HttpGet("CatalogGet/{CatalogID}")]
        [SwaggerOperation(Summary = "Obtener un catalogo por Id", Description = "Permite obtener un catalogo por id.")]
        [ProducesResponseType(typeof(MsgResponse<CatalogGetResponseDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogGet([FromRoute] int CatalogID, CancellationToken CancellationToken)
        {
           return Ok(await Mediator.Send(new CatalogGetQueryRequest(CatalogID), CancellationToken));
        }

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
