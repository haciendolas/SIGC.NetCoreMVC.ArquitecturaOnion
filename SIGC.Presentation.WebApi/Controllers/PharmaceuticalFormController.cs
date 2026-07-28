using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.PharmaceuticalFormFeatures.Queries.PharmaceuticalFormList;
using SIGC.DomainModel.Dtos.PharmaceuticalForm;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class PharmaceuticalFormController : BaseController
    {   
        [HttpGet("PharmaceuticalFormList")]
        [SwaggerOperation(Summary = "Listar las formas farmaceuticas", Description = "Permite listar las formas farmaceuticas.")]
        [ProducesResponseType(typeof(MsgResponse<List<PharmaceuticalFormListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PharmaceuticalFormList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new PharmaceuticalFormListQueryRequest(), CancellationToken));
        }
    }
}