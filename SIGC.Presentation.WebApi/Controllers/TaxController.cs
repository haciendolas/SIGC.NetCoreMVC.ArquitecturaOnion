using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.TaxFeatures.Queries.TaxList;
using SIGC.DomainModel.Dtos.Tax;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class TaxController : BaseController
    {   
        [HttpGet("TaxList")]
        [SwaggerOperation(Summary = "Listar los impuestos", Description = "Permite listar los impuestos.")]
        [ProducesResponseType(typeof(MsgResponse<List<TaxListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TaxList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new TaxListQueryRequest(), CancellationToken));
        }
    }
}