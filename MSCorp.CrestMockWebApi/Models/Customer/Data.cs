using Newtonsoft.Json;

namespace MSCorp.CrestMockWebApi.Models.Customer
{
    public class Data
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string tid { get; set; }
       
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]      
        public string etid { get; set; }
       
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string eoid { get; set; }
    }
}