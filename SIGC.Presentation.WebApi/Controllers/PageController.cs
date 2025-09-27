using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.PageFeatures.Queries.PageList;
using SIGC.DomainModel.Dtos.Page;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class PageController : BaseController
    {        
        [HttpGet("PageList")]
        [SwaggerOperation(Summary = "Listar las paginas", Description = "Permite listar las paginas.")]
        [ProducesResponseType(typeof(MsgResponse<List<PageListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PageList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new PageListQueryRequest(), CancellationToken));
        }
    }
}