using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsApp_Webhook.Models
{
    public class Gupshup_Agent_V2
    {
        
        //[JsonIgnore]

        //public string MobileEncrypted { get; set; }
        [JsonProperty("app")]
        public string App { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("payload")]
        public WelcomePayload Payload { get; set; }

        public partial class WelcomePayload
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("source")]
            public string Source { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("payload")]
            public PayloadPayload Payload { get; set; }

            [JsonProperty("sender")]
            public Sender Sender { get; set; }

            [JsonProperty("context")]
            public Context Context { get; set; }
        }

        public partial class Context
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("gsId")]
            public Guid GsId { get; set; }
        }

        public partial class PayloadPayload
        {
            [JsonProperty("text")]
            public string Text { get; set; }
        }

        public partial class Sender
        {
            [JsonProperty("phone")]
            public string Phone { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("country_code")]

            public long CountryCode { get; set; }

            [JsonProperty("dial_code")]
            public string DialCode { get; set; }
        }
    }
}

