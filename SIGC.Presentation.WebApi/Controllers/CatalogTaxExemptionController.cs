using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Commands.CatalogTaxExemptionCreate;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class CatalogTaxExemptionController : BaseController
    {
       
        [HttpPost("CatalogTaxExemptionCreate")]
        [SwaggerOperation(Summary = "Crear una exoneración de impuesto", Description = "Permite crear una exoneración de impuesto.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogTaxExemptionCreate([FromBody] CatalogTaxExemptionCreateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
        /*
           [HttpPut("CatalogTaxUpdate")]
           [SwaggerOperation(Summary = "Editar un impuesto", Description = "Permite editar un impuesto.")]
           [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
           [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
           public async Task<IActionResult> CatalogTaxUpdate([FromBody] CatalogTaxUpdateCommandRequest Command, CancellationToken CancellationToken)
           {
               return Ok(await Mediator.Send(Command, CancellationToken));
           }

           [HttpPut("CatalogTaxChangeState")]
           [SwaggerOperation(Summary = "Cambiar el estado del impuesto", Description = "Permite cambiar el estado del impuesto.")]
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

            [HttpGet("CatalogTaxList/{CatalogID}")]
            [SwaggerOperation(Summary = "Listar los impuesto por catálogo", Description = "Permite listar los impuesto por catálogo.")]
            [ProducesResponseType(typeof(MsgResponse<List<CatalogTaxListResponseDto>>), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> CatalogTaxList([FromRoute] int CatalogID, CancellationToken CancellationToken)
            {
                return Ok(await Mediator.Send(new CatalogTaxListQueryRequest(CatalogID), CancellationToken));
            }
           */
    }
}