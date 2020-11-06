using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFunction.Handler;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace MyFunction
{
    public class FunctionHandler
    {
        #region Fields

        private readonly IMediator _mediator;
        
        #endregion

        #region Constructors

        public FunctionHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Handler

        [HttpPost]
        public async Task<(int, string)> Handle(HttpRequest request)
        {
            using StreamReader reader = new StreamReader(request.Body);
            string input = await reader.ReadToEndAsync();

            FunctionRequest req = JsonConvert.DeserializeObject<FunctionRequest>(input);
            FunctionResponse response = await _mediator.Send(req);

            return (200, JsonConvert.SerializeObject(response));
        }

        #endregion
    }
}