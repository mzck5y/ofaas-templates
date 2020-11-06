using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Handlers
{
    public class SampleRequestHandler : IRequestHandler<SampleRequest, SampleResponse>
    {
        #region Public Methods

        public Task<SampleResponse> Handle(SampleRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
