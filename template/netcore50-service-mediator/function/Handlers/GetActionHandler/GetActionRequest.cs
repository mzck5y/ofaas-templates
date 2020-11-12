using MediatR;
using Service.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Requests
{
    public class GetActionRequest : IRequest<GetActionResponse>
    {
        #region Properties

        public string Id { get; set; }

        #endregion
    }
}
