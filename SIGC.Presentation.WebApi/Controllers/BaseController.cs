using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SIGC.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {        
            private IMediator? _mediator;
            protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();  
    }
}