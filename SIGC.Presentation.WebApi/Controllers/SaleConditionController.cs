using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.SaleConditionFeatures.Queries.SaleConditionList;
using SIGC.DomainModel.Dtos.SaleCondition;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class SaleConditionController : BaseController
    {   
        [HttpGet("SaleConditionList")]
        [SwaggerOperation(Summary = "Listar los tipos de condición de ventas", Description = "Permite listar los tipos de condición de ventas.")]
        [ProducesResponseType(typeof(MsgResponse<List<SaleConditionListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaleConditionList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new SaleConditionListQueryRequest(), CancellationToken));
        }
    }
}