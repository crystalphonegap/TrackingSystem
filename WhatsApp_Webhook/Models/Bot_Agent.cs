using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Cloud.Dialogflow.V2.Intent.Types;

namespace WhatsApp_Webhook.Models
{
    public class Bot_Agent
    {
        public Bot_Agent()
        {
            Id = string.Empty;
            Session = new Session();
            Request = new Request();
            Response = new Response();
        }

        public string Id { get; set; }
        public string Context { get; set; }
        public string Context_Name { get; set; }
        public string Context_Name_Session { get; set; }

        public Struct Parameters_Set { get; set; }
        internal Session Session { get; set; }
        internal Request Request { get; set; }
        internal Response Response { get; set; }
    }

    internal class Request
    {
        public string Id { get; set; }
        public string Intent { get; set; }
        public string State { get; set; }
        public Struct Parameters { get; set; }
        public Struct OutputContexts { get; set; }

    }


    internal class Response
    {
        public string Text { get; set; }
        public Message FulfillmentMessages { get; set; }
        public string Event { get; set; }
        public EventInput FollowupEventInput { get; set; }
        public RepeatedField<Context> OutputContexts { get; set; }

        public Context OutputContexts1 { get; set; }
        public SessionEntityType SessionEntityType { get; set; }

        public List<List_OutputContext> List_Contexts { get; set; }

    }



    internal class Session
    {
        public string Id { get; set; }
        public string WhatsApp_No { get; set; }

        public string Session_ID { get; set; }
    }

    public class List_OutputContext
    {
        public string Name { get; set; }
        public int LifespanCount { get; set; }

        public Struct Parameters { get; set; }
    }

}

