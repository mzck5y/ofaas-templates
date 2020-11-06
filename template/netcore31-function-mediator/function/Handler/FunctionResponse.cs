using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFunction.Handler
{
    public class FunctionResponse
    {
        #region Properties

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Payload { get; set; }

        #endregion
    }
}
