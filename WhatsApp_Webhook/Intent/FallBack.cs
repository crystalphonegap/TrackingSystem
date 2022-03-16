using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsApp_Webhook.Models;
using WhatsApp_Webhook.Utilities;

namespace WhatsApp_Webhook.Intent
{
    public class FallBack
    {
        internal static Bot_Agent Fall_Back(Bot_Agent agent)
        {
            switch (agent.Request.Intent)
            {
                //case "FallBack.Welcome":
                //    agent.Response.Text = "Please type *Hi* for start your conversation.";
                //    agent.Context_Name = "waiting_family_type";

                //    break;
                case "FallBack.Welcome.Followup":
                    agent.Response.Text = "Please choose the valid option number.";
                    agent.Context_Name = "Awaiting_Welcome_Followup";
                    agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));

                    break;
                
                default:
                    agent.Response.Text = "I'm Racold Virtual Assistant.\nI apologize but I may not have an answer to your query at this moment.\n\n💡 You can type  *Hi* to see what I can do for you";
                    agent.Context_Name = "Menu";
                    agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
                    break;
            }


            return agent;
        }
    }
}
