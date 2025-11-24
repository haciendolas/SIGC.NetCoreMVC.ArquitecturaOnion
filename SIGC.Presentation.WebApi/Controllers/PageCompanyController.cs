using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.PageCompanyFeatures.Commands.PageCompanyCreate;
using SIGC.ApplicationService.Features.PageCompanyFeatures.Queries.PageCompanyList;
using SIGC.DomainModel.Dtos.Page;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class PageCompanyController : BaseController
    {
        [HttpPost("PageCompanyDeleteCreate")]
        [SwaggerOperation(Summary = "Eliminar y Crear paginas a empresa", Description = "Permite eliminar y crear paginas a empresa.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PageCompanyDeleteCreate([FromBody] PageCompanyDeleteCreateCommandRequest Command, CancellationToken CancellationToken)
        {           
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpGet("PageCompanyList/{CompanyID}")]
        [SwaggerOperation(Summary = "Listar las paginas", Description = "Permite listar las paginas.")]
        [ProducesResponseType(typeof(MsgResponse<List<PageListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PageCompanyList([FromRoute] int CompanyID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new PageCompanyListQueryRequest(CompanyID), CancellationToken));
        }
    }
}