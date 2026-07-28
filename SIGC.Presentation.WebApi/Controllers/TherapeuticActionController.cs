using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.TherapeuticActionFeatures.Queries.TherapeuticActionList;
using SIGC.DomainModel.Dtos.TherapeuticAction;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class TherapeuticActionController : BaseController
    { 
        [HttpGet("TherapeuticActionList")]
        [SwaggerOperation(Summary = "Listar las acciones terapeuticas", Description = "Permite listar las acciones terapeuticas.")]
        [ProducesResponseType(typeof(MsgResponse<List<TherapeuticActionListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TherapeuticActionList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new TherapeuticActionListQueryRequest(), CancellationToken));
        }
    }
}