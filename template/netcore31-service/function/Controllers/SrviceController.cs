using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Service.Controllers
{
    [ApiController]
    [Route("/")]
    public class ServiceController : ControllerBase
    {
        #region Fields

        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        #endregion

        #region Constructors

        public ServiceController(
            IWebHostEnvironment environment,
            IConfiguration configuration,
            ILogger<ServiceController> logger)
        {
            _environment = environment;
            _configuration = configuration;
            _logger = logger;
        }

        #endregion

        #region Function Handler Action

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("POST was called.");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("GET was called");
        }

        #endregion
    }
}
