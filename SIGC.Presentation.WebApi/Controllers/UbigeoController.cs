using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.UbigeoFeatures.Queries.UbigeoListByClassAndCodeAndLenCode;
using SIGC.ApplicationService.Features.UbigeoFeatures.Queries.UbigeoListByUbigeoClass;
using SIGC.ApplicationService.Features.UbigeoFeatures.Queries.UbigeoListSearch;
using SIGC.DomainModel.Dtos.Ubigeo;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{  
    public class UbigeoController : BaseController
    {
        [HttpGet("UbigeoListSearch")]
        [SwaggerOperation(Summary = "Buscar ubigeo", Description = "Permite buscar ubigeos.")]
        [ProducesResponseType(typeof(MsgResponse<List<UbigeoListSearchResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UbigeoListSearch([FromQuery] UbigeoListSearchQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query,CancellationToken));
        }

        [HttpGet("UbigeoListByUbigeoClass/{UbigeoClass}")]
        [SwaggerOperation(Summary = "Obtener listado de ubigeos por clase", Description = "Permite obtener un listado de ubigeos por clase.")]
        [ProducesResponseType(typeof(MsgResponse<List<UbigeoListByUbigeoClassResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UbigeoListByUbigeoClass([FromRoute] int UbigeoClass, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new UbigeoListByUbigeoClassQueryRequest(UbigeoClass), CancellationToken));
        }

        [HttpGet("UbigeoListByClassAndCodeAndLenCode")]
        [SwaggerOperation(Summary = "Obtener listado de ubigeos por parametros", Description = "Permite obtener un listado de ubigeos por parametros.")]
        [ProducesResponseType(typeof(MsgResponse<List<UbigeoListByClassAndCodeAndLenCodeResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UbigeoListByClassAndCodeAndLenCode([FromQuery] UbigeoListByClassAndCodeAndLenCodeQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query, CancellationToken));
        }
    }
}