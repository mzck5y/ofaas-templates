using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Responses
{
    public class PostActionResponse
    {
        #region Properties

        public int StatusCode { get; init; }
        public string Message { get; init; }
        public object Payload { get; init; }

        #endregion
    }
}
