using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyChangeState;
using SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyCreate;
using SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyUpdate;
using SIGC.ApplicationService.Features.CompanyFeatures.Queries.CompanyGet;
using SIGC.ApplicationService.Features.CompanyFeatures.Queries.CompanyList;
using SIGC.ApplicationService.Features.CompanyFeatures.Queries.CompanyPagination;
using SIGC.DomainModel.Dtos.Company;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using SIGC.Infrastructure.GeneralService.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class CompanyController : BaseController
    {
        [HttpPost("CompanyCreate")]
        [SwaggerOperation(Summary = "Crear una compañia", Description = "Permite crear una compañia.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompanyCreate([FromForm] CompanyCreateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {            
            if (FormFile != null) Command.File = new FormFileService(FormFile);
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("CompanyUpdate")]
        [SwaggerOperation(Summary = "Editar una compañia", Description = "Permite editar una compañia.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompanyUpdate([FromForm] CompanyUpdateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile);
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("CompanyChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado de la compañia", Description = "Permite cambiar el estado de la compañia.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompanyChangeState([FromBody] CompanyChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpGet("CompanyGet/{CompanyID}")]
        [SwaggerOperation(Summary = "Obtener una compañia por Id", Description = "Permite obtener una compañia por id.")]
        [ProducesResponseType(typeof(MsgResponse<CompanyGetQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompanyGet([FromRoute] int CompanyID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CompanyGetQueryRequest(CompanyID), CancellationToken));
        }

        [HttpPost("CompanyPagination")]
        [SwaggerOperation(Summary = "Paginación de compañia", Description = "Permite la paginación de compañia.")]
        [ProducesResponseType(typeof(MsgResponse<PaginationResultDto<CompanyPaginationQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompanyPagination([FromQuery] CompanyPaginationQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query, CancellationToken));
        }

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