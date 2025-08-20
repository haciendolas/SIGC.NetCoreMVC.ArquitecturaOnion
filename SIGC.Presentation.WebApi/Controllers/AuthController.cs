using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGC.ApplicationService.Features.AuthFeatures.Queries.UserLogin;

namespace SIGC.Presentation.WebApi.Controllers
{ 
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("SignIn")]
        //[SwaggerOperation(Summary = "Inicar sesión", Description = " Permite Inicar sesión.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
       // [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromBody] UserLoginQueryRequest Query, CancellationToken CancellationToken)
        {
            return Ok(await Mediator.Send(Query));
        }

    }
}
