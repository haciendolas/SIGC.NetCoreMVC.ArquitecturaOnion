using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.ManufacturerFeatures.Queries.ManufacturerList;
using SIGC.DomainModel.Dtos.Manufacturer;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class ManufacturerController : BaseController
    {   
        [HttpGet("ManufacturerList")]
        [SwaggerOperation(Summary = "Listar los fabricantes", Description = "Permite listar los fabricantes.")]
        [ProducesResponseType(typeof(MsgResponse<List<ManufacturerListResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ManufacturerList(CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new ManufacturerListQueryRequest(), CancellationToken));
        }
    }
}