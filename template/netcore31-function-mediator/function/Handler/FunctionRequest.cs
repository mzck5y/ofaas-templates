using MediatR;

namespace MyFunction.Handler
{
    public class FunctionRequest : IRequest<FunctionResponse>
    {
        #region Properties

        public string Id { get; set; }

        #endregion
    }
}
