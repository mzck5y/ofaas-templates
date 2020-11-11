using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MyFunction.Handler
{
    public class FuntionRequestHandler 
        : IRequestHandler<FunctionRequest, FunctionResponse>
    {
        #region Public Methods

        public Task<FunctionResponse> Handle(FunctionRequest request, CancellationToken cancellationToken)
        {
            FunctionResponse response = new FunctionResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = HttpStatusCode.OK.ToString(),
                Payload = new
                {
                    request.Id
                }
            };

            return Task.FromResult(response);
        }

        #endregion
    }
}
