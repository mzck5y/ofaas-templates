using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Handlers;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class ServiceController : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Constructors

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Function Handler Action

        [HttpPost]
        public async Task<IActionResult> FunctionHandlerAsync([FromBody]SampleRequest request)
        {
            SampleResponse response = await _mediator.Send(request);

            return response.IsSuccessStatusCode
                ? Ok(response.Payload)
                : (IActionResult)StatusCode(response.StatusCode);
        }

        #endregion
    }
}
