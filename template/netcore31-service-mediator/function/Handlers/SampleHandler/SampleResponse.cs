using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Handlers
{
    public class SampleResponse
    {
        #region Properties

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Payload { get; set; }

        public bool IsSuccessStatusCode => StatusCode >= 200 && StatusCode <= 299;

        #endregion
    }
}
