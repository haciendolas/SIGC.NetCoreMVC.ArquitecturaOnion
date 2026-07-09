using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.BrandFeatures.Queries.BrandList;
using SIGC.DomainModel.Dtos.Brand;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class BrandController : BaseController
    {   
        [HttpGet("BrandList")]
        [SwaggerOperation(Summary = "Listar los marcar", Description = "Permite listar las marcas.")]
        [ProducesResponseType(typeof(MsgResponse<List<BrandListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BrandList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new BrandListQueryRequest(), CancellationToken));
        }
    }
}