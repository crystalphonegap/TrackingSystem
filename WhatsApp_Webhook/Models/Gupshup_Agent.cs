using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsApp_Webhook.Models
{
    public class Gupshup_Agent
    {
        public string botname { get; set; }
        public Contextobj contextobj { get; set; }
        public Messageobj messageobj { get; set; }
        public Senderobj senderobj { get; set; }
        public string channel { get; set; }




        public class Gupshup_Agent_Request
        {
            public string botname { get; set; }
            public string contextobj { get; set; }
            public string messageobj { get; set; }
            public string senderobj { get; set; }
            public string channel { get; set; }


        }

        public class Contextobj
        {
            public string botname { get; set; }
            public string channeltype { get; set; }
            public string contextid { get; set; }
            public string contexttype { get; set; }
            public string senderName { get; set; }


        }

        public class Senderobj
        {
            public string channelid { get; set; }
            public string channeltype { get; set; }
            public string display { get; set; }
            public string subdisplay { get; set; }



        }

        public class Messageobj
        {
            public string from { get; set; }
            public string id { get; set; }
            public string text { get; set; }
            public string timestamp { get; set; }
            public string type { get; set; }

        }
    }
}
