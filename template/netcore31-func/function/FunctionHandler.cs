using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace function
{
    public class FunctionHandler
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Constructors

        public FunctionHandler(ILogger<FunctionHandler> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Handler

        [HttpPost]
        public async Task<(int, string)> Handle(HttpRequest request)
        {
            using StreamReader reader = new StreamReader(request.Body);
            string input = await reader.ReadToEndAsync();

            Employee employee = JsonConvert.DeserializeObject<Employee>(input);
            employee.FirstName = "John";
            _logger.LogInformation("Function executed....");

            return (201, JsonConvert.SerializeObject(employee));
        }

        #endregion
    }

    public class Employee
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
    }
}