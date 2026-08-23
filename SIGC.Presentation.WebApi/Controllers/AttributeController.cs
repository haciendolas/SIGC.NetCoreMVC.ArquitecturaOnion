using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.AttributeValueFeatures.Queries.AttributeValueList;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{   
    public class AttributeController : BaseController
    {         
        [HttpGet("AttributeValueList")]
        [SwaggerOperation(Summary = "Listar los atributos con su detalle", Description = "Permite listar los atributos con su detalle.")]
        [ProducesResponseType(typeof(MsgResponse<List<AttributeListQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AttributeValueList(bool? AttributeIsVariant, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(new AttributeValueListQueryRequest(AttributeIsVariant), CancellationToken));
        }
    }
}