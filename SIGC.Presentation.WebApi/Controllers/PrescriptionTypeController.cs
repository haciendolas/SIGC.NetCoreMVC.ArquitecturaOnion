using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.PrescriptionTypeFeatures.Queries.PrescriptionTypeList;
using SIGC.DomainModel.Dtos.PrescriptionType;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class PrescriptionTypeController : BaseController
    {
        [AllowAnonymous]
        [HttpGet("PrescriptionTypeList")]
        [SwaggerOperation(Summary = "Listar los tipos de recetas", Description = "Permite listar los tipos de recetas.")]
        [ProducesResponseType(typeof(MsgResponse<List<PrescriptionTypeListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PrescriptionTypeList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new PrescriptionTypeListQueryRequest(), CancellationToken));
        }
    }
}