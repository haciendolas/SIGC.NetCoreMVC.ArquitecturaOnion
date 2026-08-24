using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Features.WarehouseFeatures.Commands.WarehouseChangeState;
using SIGC.ApplicationService.Features.WarehouseFeatures.Commands.WarehouseCreate;
using SIGC.ApplicationService.Features.WarehouseFeatures.Commands.WarehouseUpdate;
using SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehouseGet;
using SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehouseList;
using SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehousePagination;
using SIGC.DomainModel.Dtos.Establishment;
using SIGC.DomainModel.Dtos.Warehouse;
using SIGC.Infrastructure.CrossCutting.Wrappers;
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
       public async Task<IActionResult> WarehouseUpdate([FromBody] WarehouseUpdateCommandRequest Command, CancellationToken CancellationToken)
       {        
           return Ok(await Mediator.Send(Command, CancellationToken));
       }
    
       [HttpPut("WarehouseChangeState")]
       [SwaggerOperation(Summary = "Cambiar el estado del almacén", Description = "Permite cambiar el estado del almacén.")]
       [ProducesResponseType(typeof(MsgResponse<object?>), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> WarehouseChangeState([FromBody] WarehouseChangeStateCommandRequest Command, CancellationToken CancellationToken)
       {
           return Ok(await Mediator.Send(Command, CancellationToken));
       }
   
       [HttpGet("WarehouseGet/{WarehouseID}")]
       [SwaggerOperation(Summary = "Obtener un almacén por Id", Description = "Permite obtener un almacén por id.")]
       [ProducesResponseType(typeof(MsgResponse<EstablishmentGetResponseDto?>), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> WarehouseGet([FromRoute] int WarehouseID, CancellationToken CancellationToken)
       {
           return Ok(await Mediator.Send(new WarehouseGetQueryRequest(WarehouseID), CancellationToken));
       }

       [HttpGet("WarehouseList/{EstablishmentID}")]
       [SwaggerOperation(Summary = "Obtener una lista de almacenes por establecimiento id", Description = "Permite obtener una lista de almacenes por establecimiento id.")]
       [ProducesResponseType(typeof(MsgResponse<WarehouseListResponseDto>), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(JsonExceptionResult), StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> WarehouseList([FromRoute] int EstablishmentID, CancellationToken CancellationToken)
       {
            return Ok(await Mediator.Send(new WarehouseListQueryRequest(EstablishmentID), CancellationToken));
       }

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
