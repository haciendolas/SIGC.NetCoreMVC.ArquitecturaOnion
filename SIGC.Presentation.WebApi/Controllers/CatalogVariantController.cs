using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.CatalogVariantFeatures.Commands.CatalogVariantCreate;
using SIGC.ApplicationService.Features.CatalogVariantFeatures.Commands.CatalogVariantUpdate;
using SIGC.ApplicationService.Features.CatalogVariantFeatures.Queries.CatalogVariantList;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{ 
    public class CatalogVariantController : BaseController
    {        
        [HttpPost("CatalogVariantCreate")]
        [SwaggerOperation(Summary = "Crear un variante", Description = "Permite crear un variante.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogVariantCreate([FromBody] CatalogVariantCreateCommandRequest Command, CancellationToken CancellationToken)
        {            
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
   
        [HttpPut("CatalogVariantUpdate")]
        [SwaggerOperation(Summary = "Editar un variante", Description = "Permite editar un variante.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogVariantUpdate([FromBody] CatalogVariantUpdateCommandRequest Command, CancellationToken CancellationToken)
        { 
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
        /*
        [HttpPut("CatalogVariantChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado del establecimiento", Description = "Permite cambiar el estado del establecimiento.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogVariantChangeState([FromBody] CatalogVariantChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        } 
        */
        [HttpGet("CatalogVariantList/{CatalogID}")]
        [SwaggerOperation(Summary = "Listar las variantes por catálogo", Description = "Permite listar las variantes por catálogo.")]
        [ProducesResponseType(typeof(MsgResponse<List<CatalogVariantListQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogVariantList([FromRoute] int CatalogID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CatalogVariantListQueryRequest(CatalogID), CancellationToken));
        }
    }
}
