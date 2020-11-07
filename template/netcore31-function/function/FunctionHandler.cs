using Microsoft.AspNetCore.Mvc;
using System;

namespace Function
{
    [Route("/")]
    public class FunctionHandler : ControllerBase
    {
        [HttpPost]
        public IActionResult Run()
        {
            return Ok($"Hello Serverless Function. The current time is {DateTime.Now}");
        }
    }
}
