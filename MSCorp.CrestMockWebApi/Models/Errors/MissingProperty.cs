
using Newtonsoft.Json;

namespace MSCorp.CrestMockWebApi.Models.Errors
{
    public class MissingProperty 
    {
        public string error_code { get; set; }       
        public string message { get; set; }
        public Parameter parameters { get; set; }
        public string object_type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AdditionalError[] other_errors { get; set; }
    }
}