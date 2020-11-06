using MediatR;
using System;
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
            throw new NotImplementedException("TODO you need to implement the reqeust handler");
        }

        #endregion
    }
}
