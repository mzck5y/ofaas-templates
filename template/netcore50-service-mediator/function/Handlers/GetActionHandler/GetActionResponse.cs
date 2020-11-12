using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Service.Responses
{
    public class GetActionResponse
    {
        #region Properties

        public int StatusCode { get; init; }        
        public string Message { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object Payload { get; init; }

        #endregion
    }
}
