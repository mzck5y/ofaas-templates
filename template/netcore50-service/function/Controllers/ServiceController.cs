using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Handlers.PostActionHandler;
using Service.Requests;
using Service.Responses;
using System.Net;

namespace Service.Controllers
{
    [Route("api")]
    public class ServiceController : ControllerBase
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Constrollers

        public ServiceController(ILogger<ServiceController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Actions

        [HttpPost]
        public IActionResult PostAction([FromBody] PostActionRequest request)
        {
            PostActionResponse response = new PostActionResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = HttpStatusCode.OK.ToString(),
                Payload = new
                {
                    request.Id
                }
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetAction(string id)
        {
            GetActionResponse response = new GetActionResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = HttpStatusCode.OK.ToString(),
                Payload = new
                {
                    id
                }
            };

            return Ok(response);
        }


        #endregion
    }
}
