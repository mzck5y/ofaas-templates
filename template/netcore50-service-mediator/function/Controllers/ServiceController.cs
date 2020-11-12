using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Requests;
using Service.Responses;
using System.Net;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api")]
    public class ServiceController : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Constrollers

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Actions

        [HttpPost]
        public async Task<IActionResult> PostActionAsync([FromBody] GetActionRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActionAsync(string id)
        {
            var response = await _mediator.Send(new GetActionRequest { Id = id });
            return Ok(response);
        }


        #endregion
    }
}
