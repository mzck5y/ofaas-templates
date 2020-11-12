using MediatR;
using Service.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Requests
{
    public class PostActionRequest : IRequest<PostActionResponse>
    {
        #region Properties

        public string Id { get; set; }

        #endregion
    }
}
