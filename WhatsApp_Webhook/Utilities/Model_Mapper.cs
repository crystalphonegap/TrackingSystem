using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsApp_Webhook.Models;
using static WhatsApp_Webhook.Models.Gupshup_Agent;

namespace WhatsApp_Webhook.Utilities
{
    public class Model_Mapper
    {
      
        internal static Bot_Agent DialogFlow_To_Model(WebhookRequest request)
        {

            Bot_Agent bot_agent = new Bot_Agent();
            bot_agent.Id = request.ResponseId;
            bot_agent.Session.Id = request.Session;
            request.QueryResult.LanguageCode = "en";
            if (bot_agent.Session.Id != "")
            {
                bot_agent.Session.Session_ID = request.Session.Split("/")[4];

                //bot_agent.Context = "projects/whatsapp-bfnkwj/agent/sessions/" + bot_agent.Session.Session_ID + "/contexts/";
                bot_agent.Context = bot_agent.Session.Id + "/contexts/";
            }
            //else
            //{
            //    bot_agent.Session.WhatsApp_No = "+919673991488".Substring(3, 10);
            //    bot_agent.Context = "projects/whatsapp-bfnkwj/agent/sessions/" + request.QueryResult.OutputContexts.FirstOrDefault(c => c.LifespanCount > 20).Name.Split("/")[4] + "/contexts/";
            //}

            bot_agent.Request.Intent = request.QueryResult.Intent.DisplayName;
     

            bot_agent.Request.Parameters = request.QueryResult.Parameters;

            if(request.QueryResult.OutputContexts.FirstOrDefault(c => c.LifespanCount > 20) != null)
            {

                bot_agent.Request.OutputContexts = request.QueryResult.OutputContexts.FirstOrDefault(c => c.LifespanCount > 20).Parameters;
            }
   

            return bot_agent;
        }

        internal static WebhookResponse Module_To_DialogFlow(Bot_Agent bot_agent)
        {
            WebhookResponse response = new WebhookResponse();

            if (bot_agent.Request.Intent == "Welcome" || bot_agent.Request.Intent == "Menu" || bot_agent.Request.Intent == "Welcome.Dealer")
            {
                response.OutputContexts.Clear();
            }

          



            response.FulfillmentText = bot_agent.Response.Text;

            response.FollowupEventInput = bot_agent.Response.FollowupEventInput;

            if (bot_agent.Response.OutputContexts != null)
            {
                response.OutputContexts.Add(bot_agent.Response.OutputContexts);
            }

          
            //if (bot_agent.Context_Name != null)
            //{
            //    response.OutputContexts.Add(new Context
            //    {
            //        Name = bot_agent.Context + bot_agent.Context_Name,
            //        LifespanCount = 1,
            //        Parameters = bot_agent.Parameters_Set

            //    });
            //}
            if (bot_agent.Context_Name_Session == "session-var")
            {
                response.OutputContexts.Add(new Context
                {
                    Name = bot_agent.Context + bot_agent.Context_Name_Session,
                    LifespanCount = 49,
                    Parameters = bot_agent.Parameters_Set

                });
            }

            //response.OutputContexts.Clear();
            if (bot_agent.Response.List_Contexts != null)
            {
                foreach (List_OutputContext Value in bot_agent.Response.List_Contexts)
                {
                    response.OutputContexts.Add(new Context
                    {
                        Name = Value.Name,
                        LifespanCount = Value.LifespanCount,
                        Parameters = Value.Parameters

                    });

                }
            }



            if (bot_agent.Response.SessionEntityType != null)
            {
                response.SessionEntityTypes.Add(bot_agent.Response.SessionEntityType);
            }
           

            response.Source = "WhatsApp Bot";






            return response;
        }

        internal static Gupshup_Agent Gupshup_To_Model(Gupshup_Agent_Request gupshup_bot_agent)
        {

            Gupshup_Agent bot_agent = new Gupshup_Agent();
            bot_agent.botname = gupshup_bot_agent.botname;
            bot_agent.channel = gupshup_bot_agent.channel;
            if (gupshup_bot_agent.contextobj != null)
            {
                bot_agent.contextobj = JsonConvert.DeserializeObject<Contextobj>(gupshup_bot_agent.contextobj);
            }
            if (gupshup_bot_agent.senderobj != null)
            {
                bot_agent.senderobj = JsonConvert.DeserializeObject<Senderobj>(gupshup_bot_agent.senderobj);
            }
            if (gupshup_bot_agent.messageobj != null)
            {
                bot_agent.messageobj = JsonConvert.DeserializeObject<Messageobj>(gupshup_bot_agent.messageobj);
            }
           
           
            return bot_agent;
        }

    }
}
