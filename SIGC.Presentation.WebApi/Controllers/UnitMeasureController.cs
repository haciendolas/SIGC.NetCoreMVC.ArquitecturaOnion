using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.UnitMeasureFeatures.Queries.UnitMeasureList;
using SIGC.DomainModel.Dtos.UnitMeasure;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class UnitMeasureController : BaseController
    {  
        [HttpGet("UnitMeasureList")]
        [SwaggerOperation(Summary = "Listar las unidades de medidas", Description = "Permite listar las unidades de medidas.")]
        [ProducesResponseType(typeof(MsgResponse<List<UnitMeasureListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UnitMeasureList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new UnitMeasureListQueryRequest(), CancellationToken));
        }
    }
}