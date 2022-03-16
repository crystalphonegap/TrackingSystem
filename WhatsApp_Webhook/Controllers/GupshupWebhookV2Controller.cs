using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatsApp_Webhook.Models;
using static WhatsApp_Webhook.Models.Gupshup_Agent;
using Google.Cloud.Dialogflow.V2;
using WhatsApp_Webhook.Utilities;

namespace WhatsApp_Webhook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GupshupWebhookV2Controller : ControllerBase
    {
        public ContentResult ReadStringDataManual(Gupshup_Agent_V2 Bot_V2)
        {


            string ReturnMessageToGupshup = string.Empty;
            //return Content(response.QueryResult.FulfillmentText.ToString(), "application / json");

            DetectIntentResponse response = new DetectIntentResponse();
            if (Bot_V2.Payload.Payload != null)
            {
                if (Bot_V2.Payload.Payload.Text != null)
                {

                    response = Utilities.Send_To_Dialogflow.SendTextMessageToDialogFlow(Bot_V2.Payload.Sender.Phone, Bot_V2.Payload.Payload.Text);

                    //    
                    ReturnMessageToGupshup = response.QueryResult.FulfillmentText;

                }
            }
            else
            {

                ReturnMessageToGupshup = "Testing";
            }
            return Content(ReturnMessageToGupshup, "application / json");




        }
    }
}