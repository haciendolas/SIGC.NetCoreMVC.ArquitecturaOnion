using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryChangeState;
using SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryCreate;
using SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryUpdate;
using SIGC.ApplicationService.Features.CategoryFeatures.Queries.CategoryGet;
using SIGC.ApplicationService.Features.CategoryFeatures.Queries.CategoryPagination;
using SIGC.DomainModel.Dtos.Category;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using SIGC.Infrastructure.GeneralService.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{ 
    public class CategoryController : BaseController
    {
        [HttpPost("CategoryCreate")]
        [SwaggerOperation(Summary = "Crear una categoria", Description = "Permite crear una categoria.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CategoryCreate([FromForm] CategoryCreateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile);
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("CategoryUpdate")]
        [SwaggerOperation(Summary = "Editar una categoria", Description = "Permite editar una categoria.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompanyUpdate([FromForm] CategoryUpdateCommandRequest Command, IFormFile? FormFile, CancellationToken CancellationToken)
        {
            if (FormFile != null) Command.File = new FormFileService(FormFile);
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpPut("CategoryChangeState")]
        [SwaggerOperation(Summary = "Cambiar el estado de la categoria", Description = "Permite cambiar el estado de la categoria.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CategoryChangeState([FromBody] CategoryChangeStateCommandRequest Command, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Command, CancellationToken));
        }

        [HttpGet("CategoryGet/{CategoryID}")]
        [SwaggerOperation(Summary = "Obtener una categoria por Id", Description = "Permite obtener una categoria por id.")]
        [ProducesResponseType(typeof(MsgResponse<CategoryGetResponseDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CategoryGet([FromRoute] int CategoryID, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new CategoryGetQueryRequest(CategoryID), CancellationToken));
        }

        [HttpPost("CategoryPagination")]
        [SwaggerOperation(Summary = "Paginación de categoria", Description = "Permite la paginación de categoria.")]
        [ProducesResponseType(typeof(MsgResponse<PaginationResultDto<CategoryPaginationQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CategoryPagination([FromQuery] CategoryPaginationQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query, CancellationToken));
        } 
    }
}
