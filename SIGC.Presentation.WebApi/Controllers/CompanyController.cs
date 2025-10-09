using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.CompanyFeatures.Queries;
using SIGC.DomainModel.Dtos.Company;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class CompanyController : BaseController
    {      
        [HttpGet("CompanyList/{CompanyIDRegister}")]
        [SwaggerOperation(Summary = "Listar las empresas por código de compañia que lo ha registrado", Description = "Permite listar las empresas por código de compañia que lo ha registrado.")]
        [ProducesResponseType(typeof(MsgResponse<List<CompanyListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompanyList([FromRoute] int CompanyIDRegister, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CompanyListQueryRequest(CompanyIDRegister), CancellationToken));
        }
    }
}