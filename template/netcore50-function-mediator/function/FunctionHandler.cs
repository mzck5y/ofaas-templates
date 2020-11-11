using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFunction.Handler;

namespace Function
{
    [ApiController]
    [Route("/")]
    public class FunctionHandler : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        #endregion

        #region Constructors

        public FunctionHandler(IMediator mediator, ILogger<FunctionHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #endregion

        #region Run Action

        public async Task<IActionResult> RunAsync([FromBody] FunctionRequest request)
        {
            FunctionResponse response = await _mediator.Send(request);

            _logger.LogInformation("Function Run Finished at {0}", DateTime.UtcNow);

            return Ok(response);
        }

        #endregion
    }
}
