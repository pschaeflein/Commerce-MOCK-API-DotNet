using Newtonsoft.Json;

namespace MSCorp.CrestMockWebApi.Models.Common
{
    public class Links
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject self { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject snapshot { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject update { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject patch { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject payer { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject recipient { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject profiles { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject addresses { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject order { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject offer { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject update_friendly_name { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject transition_sources { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject transition_targets { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject add_suspension { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject remove_suspension { get; set; } 
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject update_advisor_partner_id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LinkObject entitlement { get; set; }

    }
}