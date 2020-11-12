using MediatR;
using Service.Requests;
using Service.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Handlers.GetActionHandler
{
    public class GetActionRequestHandler : IRequestHandler<GetActionRequest, GetActionResponse>
    {
        public Task<GetActionResponse> Handle(GetActionRequest request, CancellationToken cancellationToken)
        {
            // TODO Implekent this method

            var response = new GetActionResponse
            {
                StatusCode = 200,
                Message = "Success",
                Payload = new
                {
                    request.Id
                }
            };

            return Task.FromResult(response);
        }
    }

}
