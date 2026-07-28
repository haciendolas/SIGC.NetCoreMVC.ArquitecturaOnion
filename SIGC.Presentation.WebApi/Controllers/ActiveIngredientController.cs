using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.ActiveIngredientFeatures.Queries.ActiveIngredientList;
using SIGC.DomainModel.Dtos.ActiveIngredient;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class ActiveIngredientController : BaseController
    {       
        [HttpGet("ActiveIngredientList")]
        [SwaggerOperation(Summary = "Listar los ingredientes activos", Description = "Permite listar los ingredientes activos.")]
        [ProducesResponseType(typeof(MsgResponse<List<ActiveIngredientListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActiveIngredientList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new ActiveIngredientListQueryRequest(), CancellationToken));
        }
    }
}