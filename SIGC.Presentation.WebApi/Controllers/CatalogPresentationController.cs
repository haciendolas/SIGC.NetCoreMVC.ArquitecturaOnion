using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.CatalogPresentationFeatures.Commands.CatalogPresentationCreate;
using SIGC.ApplicationService.Features.CatalogPresentationFeatures.Queries.CatalogPresentationList;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class CatalogPresentationController : BaseController
    {
        [HttpPost("CatalogPresentationCreate")]
        [SwaggerOperation(Summary = "Crear un presentación", Description = "Permite crear un presentación.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogPresentationCreate([FromBody] CatalogPresentationCreateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpGet("CatalogPresentationList/{CatalogID}")]
        [SwaggerOperation(Summary = "Listar las presentaciones por catálogo", Description = "Permite listar las presentaciones por catálogo.")]
        [ProducesResponseType(typeof(MsgResponse<List<CatalogVariantListQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogPresentationList([FromRoute] int CatalogID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CatalogPresentationListQueryRequest(CatalogID), CancellationToken));
        }
    }
}