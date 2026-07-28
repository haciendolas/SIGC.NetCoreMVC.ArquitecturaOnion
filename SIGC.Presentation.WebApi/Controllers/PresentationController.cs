using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.PresentationFeatures.Queries.PresentationList;
using SIGC.DomainModel.Dtos.Presentation;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class PresentationController : BaseController
    {         
        [HttpGet("PresentationList/{UnitMeasureID}")]
        [SwaggerOperation(Summary = "Listar las presentaciones por unidad de medida", Description = "Permite listar las presentaciones por unidad de medida.")]
        [ProducesResponseType(typeof(MsgResponse<List<PresentationListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PresentationList([FromRoute] int UnitMeasureID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new PresentationListQueryRequest(UnitMeasureID), CancellationToken));
        }
    }
}