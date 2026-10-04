using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.CatalogPriceFeatures.Commands.CatalogPriceCreate;
using SIGC.ApplicationService.Features.CatalogPriceFeatures.Queries.CatalogPriceList;
using SIGC.DomainModel.Dtos.CatalogPrice;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class CatalogPriceController : BaseController
    {
        [HttpPost("CatalogPriceCreate")]
        [SwaggerOperation(Summary = "Crear un precio", Description = "Permite crear un precio.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogPriceCreate([FromBody] CatalogPriceCreateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
        /*
        [HttpPut("CatalogPriceChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado de la presentación", Description = "Permite cambiar el estado de la presentación.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogPriceChangeState([FromBody] CatalogPriceChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
         */
        [HttpGet("CatalogPriceList/{CatalogID}")]
        [SwaggerOperation(Summary = "Listar los precios por catálogo", Description = "Permite listar los precios por catálogo.")]
        [ProducesResponseType(typeof(MsgResponse<List<CatalogPriceListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogPriceList([FromRoute] int CatalogID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CatalogPriceListQueryRequest(CatalogID), CancellationToken));
        }
     
    }
}