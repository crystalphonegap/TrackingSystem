using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WhatsApp_Webhook.Models;
using static WhatsApp_Webhook.Models.Gupshup_Agent;
using Google.Cloud.Dialogflow.V2;
using WhatsApp_Webhook.Utilities;

namespace WhatsApp_Webhook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GupshupWebhookController : ControllerBase
    {
        
        public ContentResult ReadStringDataManual(Gupshup_Agent_V2 Bot_V2)
        {


            //DetectIntentResponse response = new DetectIntentResponse();

            //if (bot_gupshup.messageobj.from != null && V2.Payload.Payload.Text != null)
            //{
            //    response = Utilities.Send_To_Dialogflow.SendTextMessageToDialogFlow(bot_gupshup.messageobj.from, bot_gupshup.messageobj.text);
            //}
            //else
            //{
            //    WebhookResponse response1 = new WebhookResponse();

            //    response1.FulfillmentText = "test";
            //    return Content(response1.FulfillmentText.ToString(), "application / json");
            //}






            //return Content(response.QueryResult.FulfillmentText.ToString(), "application / json");





            /// Direct Gupshup to whatsapp
            string message_response = string.Empty;
            if (Bot_V2.Payload.Payload != null)
            {
                if (Bot_V2.Payload.Payload.Text != null)
                {
                    // Welcome

                    if (Bot_V2.Payload.Payload.Text.ToLower() == "hi" || Bot_V2.Payload.Payload.Text.ToLower() == "hello")
                    {

                        message_response = "Hello, Welcome to _Racold India Service!_ \n*Get started right away by selecting the option number*\n*1.* Do you want to register a new complaint.\n*2.* For a new installation.\n\n*Please enter the option number*";
                    }
                    else if (Bot_V2.Payload.Payload.Text.ToLower() == "1" || Bot_V2.Payload.Payload.Text.ToLower() == "2" || Bot_V2.Payload.Payload.Text.ToLower() == "installation" || Bot_V2.Payload.Payload.Text.ToLower() == "complaint")
                    {

                        string service = string.Empty;
                        if (Bot_V2.Payload.Payload.Text.ToLower() == "1" || Bot_V2.Payload.Payload.Text.ToLower() == "complaint") { service = "repair"; } else if (Bot_V2.Payload.Payload.Text.ToLower() == "2" || Bot_V2.Payload.Payload.Text.ToLower() == "installation") { service = "installation"; }
                        string Api_Key = "f1a98253e4f7d16a419fb258bc16daaaac82f";
                        string Base_Url = "https://aristonservice.in/Racold_Customer/complaint.aspx?";
                        string LongURl = string.Format(Base_Url + @"mobile={0}&service={1}&timestamp={2}&utm={3}", MyCrypto.EncryptString(Bot_V2.Payload.Sender.Phone), MyCrypto.EncryptString(service), MyCrypto.EncryptString(DateTime.Now.ToString("yyyyMMddHHmm")), MyCrypto.EncryptString("WB"));

                        //string LongURl = "mobile=9820155930&service=installation&timestamp=11255455&utm=whatsapp";

                        var result = Utilities.Url_Shortner.ShortenUrl(Api_Key, LongURl);


                        message_response = "You can book your complaint through below link\n" + result.Result.url.shortLink + "\n\nFor any further assistance write to us at *customer.care@racold.com* ";

                    }
                    else
                    {
                        message_response = "Start interacting with Racold customer care, text *Hi* to the same WhatsApp number.";
                    }

                }
            }
            else
            { }

            return Content(message_response.ToString(), "application / json");
        }
    }
}