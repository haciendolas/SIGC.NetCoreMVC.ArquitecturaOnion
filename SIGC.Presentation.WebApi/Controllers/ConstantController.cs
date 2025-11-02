using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.ConstantFeatures.Queries.ConstantList;
using SIGC.DomainModel.Dtos.Constant;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{  
    public class ConstantController : BaseController
    {      
        [HttpGet("ConstantList")]
        [SwaggerOperation(Summary = "Obtener listado de constantes por clase", Description = "Permite obtener un listado de constantes por clase.")]
        [ProducesResponseType(typeof(MsgResponse<List<ConstantListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConstantList([FromQuery] string ConstantClassConcat, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new ConstantListQueryRequest(ConstantClassConcat), CancellationToken));
        }
    }
}