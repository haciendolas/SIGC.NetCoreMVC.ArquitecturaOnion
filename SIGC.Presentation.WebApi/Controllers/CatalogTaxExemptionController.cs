using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Commands.CatalogTaxExemptionChangeState;
using SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Commands.CatalogTaxExemptionCreate;
using SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Commands.CatalogTaxExemptionDelete;
using SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Queries.CatalogTaxExemptionList;
using SIGC.DomainModel.Dtos.CatalogTaxExemption;
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

        [HttpPut("CatalogTaxExemptionChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado de exoneración de impuesto", Description = "Permite cambiar el estado de exoneración de impuesto.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogTaxExemptionChangeState([FromBody] CatalogTaxExemptionChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {
           return Ok(await Mediator.Send(Command, CancellationToken));
        }
       
        [HttpDelete("CatalogTaxExemptionDelete/{CatalogTaxExemptionID}")]
        [SwaggerOperation(Summary = "Eliminar un exoneración de impuesto por su id", Description = "Permite eliminar un exoneración de impuesto por su id.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogTaxExemptionDelete([FromRoute] int CatalogTaxExemptionID, CancellationToken CancellationToken)
        {
             return Ok(await Mediator.Send(new CatalogTaxExemptionDeleteCommandRequest(CatalogTaxExemptionID), CancellationToken));
        }
     
        [HttpGet("CatalogTaxExemptionList/{CatalogID}")]
        [SwaggerOperation(Summary = "Listar exoneración de impuestos por catálogo", Description = "Permite listar exoneración  de impuestos por catálogo.")]
        [ProducesResponseType(typeof(MsgResponse<List<CatalogTaxExemptionListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogTaxExemptionList([FromRoute] int CatalogID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CatalogTaxExemptionListQueryRequest(CatalogID), CancellationToken));
        }          
    }
}