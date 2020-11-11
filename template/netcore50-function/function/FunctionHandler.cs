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
            return Ok($"Hello from serverless function using the .net 5.0 runtime. Today is {DateTime.Now}");
        }
    }
}
