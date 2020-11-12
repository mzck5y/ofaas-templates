using MediatR;
using Service.Requests;
using Service.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Handlers.PostActionHandler
{
    public class PostActionRequestHandler : IRequestHandler<PostActionRequest, PostActionResponse>
    {
        public Task<PostActionResponse> Handle(PostActionRequest request, CancellationToken cancellationToken)
        {
            var response = new PostActionResponse
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
