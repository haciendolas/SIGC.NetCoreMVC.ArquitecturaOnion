using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.PriceTypeFeatures.Queries.PriceTypeList;
using SIGC.DomainModel.Dtos.PriceType;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class PriceTypeController : BaseController
    {   
        [HttpGet("PriceTypeList")]
        [SwaggerOperation(Summary = "Listar los tipos de precios", Description = "Permite listar los tipos de precios.")]
        [ProducesResponseType(typeof(MsgResponse<List<PriceTypeListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PriceTypeList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new PriceTypeListQueryRequest(), CancellationToken));
        }
    }
}