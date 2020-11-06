using MediatR;

namespace Service.Handlers
{
    public class SampleRequest : IRequest<SampleResponse>
    {
        #region Properties

        public string Id { get; set; }

        #endregion
    }
}
