using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsApp_Webhook.Models;

namespace WhatsApp_Webhook.Utilities
{
    public class Intent_Router
    {
        public static Bot_Agent Process(Bot_Agent agent)
        {
            var intentsList = WhatsApp_Webhook.Controllers.DialogflowWebhookController.IntentHandlers;

            var intent = intentsList.FirstOrDefault(i => i.Key.ToLower() == agent.Request.Intent.ToLower());
            if (!string.IsNullOrWhiteSpace(intent.Key))
            {
                return intent.Value(agent);
            }
            if (string.IsNullOrWhiteSpace(agent.Response.Text))
            {
                agent.Response.Text = "Sorry, I do not understand. please try again.";
            }
            return agent;
        }
    }
}
