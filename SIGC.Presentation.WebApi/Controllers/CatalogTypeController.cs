using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.CatalogTypeFeatures.Queries.CatalogTypeList;
using SIGC.DomainModel.Dtos.CatalogType;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class CatalogTypeController : BaseController
    {        
        [HttpGet("CatalogTypeList")]
        [SwaggerOperation(Summary = "Listar tipo de catalogos", Description = "Permite listar los tipo de catalogos.")]
        [ProducesResponseType(typeof(MsgResponse<List<CatalogTypeListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatalogTypeList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CatalogTypeListQueryRequest(), CancellationToken));
        }
    }
}