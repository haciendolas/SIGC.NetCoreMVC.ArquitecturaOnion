using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentChangeState;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentCreate;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentUpdate;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentGet;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentList;
using SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentPagination;
using SIGC.ApplicationService.Features.WarehouseFeatures.Commands.WarehouseCreate;
using SIGC.ApplicationService.Features.WarehouseFeatures.Commands.WarehouseUpdate;
using SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehouseGet;
using SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehousePagination;
using SIGC.DomainModel.Dtos.Establishment;
using SIGC.Infrastructure.CrossCutting.Wrappers;
using SIGC.Infrastructure.GeneralService.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SIGC.Presentation.WebApi.Controllers
{ 
    public class WarehouseController : BaseController
    {
       
        [HttpPost("WarehouseCreate")]
        [SwaggerOperation(Summary = "Crear un almacén", Description = "Permite crear un almacén.")]
        [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> WarehouseCreate([FromBody] WarehouseCreateCommandRequest Command, CancellationToken CancellationToken)
        {          
            return Ok(await Mediator.Send(Command, CancellationToken));
        }
   
       [HttpPut("WarehouseUpdate")]
       [SwaggerOperation(Summary = "Editar un almacén", Description = "Permite editar un almacén.")]
       [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> WarehouseUpdate([FromForm] WarehouseUpdateCommandRequest Command, CancellationToken CancellationToken)
       {        
           return Ok(await Mediator.Send(Command, CancellationToken));
       }
      /*
       [HttpPut("EstablishmentChangeState")]
       [SwaggerOperation(Summary = "Cambiar el estado del establecimiento", Description = "Permite cambiar el estado del establecimiento.")]
       [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> EstablishmentChangeState([FromBody] EstablishmentChangeStateCommandRequest Command, CancellationToken CancellationToken)
       {
           return Ok(await Mediator.Send(Command, CancellationToken));
       }
    */
       [HttpGet("WarehouseGet/{WarehouseID}")]
       [SwaggerOperation(Summary = "Obtener un almacén por Id", Description = "Permite obtener un almacén por id.")]
       [ProducesResponseType(typeof(MsgResponse<EstablishmentGetResponseDto?>), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> WarehouseGet([FromRoute] int WarehouseID, CancellationToken CancellationToken)
       {
           return Ok(await Mediator.Send(new WarehouseGetQueryRequest(WarehouseID), CancellationToken));
       }
        /*
       [HttpGet("EstablishmentList/{PersonID}")]
       [SwaggerOperation(Summary = "Listar los establecimiento de la empresa", Description = "Permite listar los establecimientos de la empresa.")]
       [ProducesResponseType(typeof(MsgResponse<List<EstablishmentListResponseDto>>), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> EstablishmentList([FromRoute] int? PersonID, CancellationToken CancellationToken)
       {
           return Ok(await Mediator.Send(new EstablishmentListQueryRequest(PersonID), CancellationToken));
       }
       */
        [HttpPost("WarehousePagination")]
        [SwaggerOperation(Summary = "Paginación de almacen", Description = "Permite la paginación de almacen.")]
        [ProducesResponseType(typeof(MsgResponse<PaginationResultDto<WarehousePaginationQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> WarehousePagination([FromQuery] WarehousePaginationQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query, CancellationToken));
        }
    }
}
