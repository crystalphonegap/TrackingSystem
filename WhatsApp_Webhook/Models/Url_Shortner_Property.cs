using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsApp_Webhook.Models
{
    public class Url_Shortner_Property
    {
        public Url url { get; set; }
    }

    public class Url
    {
        public string status { get; set; }
        public string fullLink { get; set; }
        public string date { get; set; }
        public string shortLink { get; set; }
        public string title { get; set; }
        
    }
}
