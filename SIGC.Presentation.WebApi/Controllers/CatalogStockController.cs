using Microsoft.AspNetCore.Mvc; 
using SIGC.ApplicationService.Features.CatalogStockFeatures.Commands.CatalogStockCreate;
using SIGC.ApplicationService.Features.CatalogStockFeatures.Queries.CatalogStockList;
using SIGC.DomainModel.Dtos.CatalogStock;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class CatalogStockController : BaseController
    {       
        [HttpPost("CatalogStockCreate")]
        [SwaggerOperation(Summary = "Crear un stock", Description = "Permite crear un stock.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogStockCreate([FromBody] CatalogStockCreateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
        /*
          [HttpPut("CatalogStockUpdate")]
          [SwaggerOperation(Summary = "Editar un impuesto", Description = "Permite editar un impuesto.")]
          [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
          public async Task<IActionResult> CatalogStockUpdate([FromBody] CatalogStockUpdateCommandRequest Command, CancellationToken CancellationToken)
          {
              return Ok(await Mediator.Send(Command, CancellationToken));
          }

          [HttpPut("CatalogStockChangeState")]
          [SwaggerOperation(Summary = "Cambiar el estado del impuesto", Description = "Permite cambiar el estado del impuesto.")]
          [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
          public async Task<IActionResult> CatalogStockChangeState([FromBody] CatalogStockChangeStateCommandRequest Command, CancellationToken CancellationToken)
          {
              return Ok(await Mediator.Send(Command, CancellationToken));
          }
        */
       [HttpGet("CatalogStockList/{CatalogID}")]
       [SwaggerOperation(Summary = "Listar los stock por catálogo", Description = "Permite listar los stock por catálogo.")]
       [ProducesResponseType(typeof(MsgResponse<List<CatalogStockListResponseDto>>), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> CatalogStockList([FromRoute] int CatalogID, [FromQuery] int? EstablishmentID, CancellationToken CancellationToken)
       {
           return Ok(await Mediator.Send(new CatalogStockListQueryRequest(CatalogID, EstablishmentID), CancellationToken));
       }  
     
    }
}