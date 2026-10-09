using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.CatalogTaxFeatures.Commands.CatalogTaxCreate;
using SIGC.ApplicationService.Features.CatalogTaxFeatures.Commands.CatalogTaxUpdate;
using SIGC.ApplicationService.Features.CatalogTaxFeatures.Queries.CatalogTaxList;
using SIGC.DomainModel.Dtos.CatalogTax;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class CatalogTaxController : BaseController
    {
       
        [HttpPost("CatalogTaxCreate")]
        [SwaggerOperation(Summary = "Crear un impuesto", Description = "Permite crear un impuesto.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogTaxCreate([FromBody] CatalogTaxCreateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
    
       [HttpPut("CatalogTaxUpdate")]
       [SwaggerOperation(Summary = "Editar un impuesto", Description = "Permite editar un impuesto.")]
       [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> CatalogTaxUpdate([FromBody] CatalogTaxUpdateCommandRequest Command, CancellationToken CancellationToken)
       {
           return Ok(await Mediator.Send(Command, CancellationToken));
       }
        /*
   [HttpPut("CatalogTaxChangeState")]
   [SwaggerOperation(Summary = "Cambiar el estado del precio", Description = "Permite cambiar el estado del precio.")]
   [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> CatalogTaxChangeState([FromBody] CatalogTaxChangeStateCommandRequest Command, CancellationToken CancellationToken)
   {
       return Ok(await Mediator.Send(Command, CancellationToken));
   }

   [HttpDelete("CatalogTaxDelete/{CatalogTaxID}")]
   [SwaggerOperation(Summary = "Eliminar un precio por su id", Description = "Permite eliminar un precio por su id.")]
   [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> CatalogTaxDelete([FromRoute] int CatalogTaxID, CancellationToken CancellationToken)
   {
       return Ok(await Mediator.Send(new CatalogTaxDeleteCommandRequest(CatalogTaxID), CancellationToken));
   }
   */
        [HttpGet("CatalogTaxList/{CatalogID}")]
        [SwaggerOperation(Summary = "Listar los impuesto por catálogo", Description = "Permite listar los impuesto por catálogo.")]
        [ProducesResponseType(typeof(MsgResponse<List<CatalogTaxListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogTaxList([FromRoute] int CatalogID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CatalogTaxListQueryRequest(CatalogID), CancellationToken));
        }
     
    }
}