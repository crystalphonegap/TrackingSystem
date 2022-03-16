using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPS.Models;
using NPS.Services;
using WhatsApp_Webhook.Intent;
using WhatsApp_Webhook.Models;
using WhatsApp_Webhook.Utilities;

namespace WhatsApp_Webhook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class DialogflowWebhookController : ControllerBase
    {
        public DialogflowWebhookController(IWABARepository respository)
        {
            this.respository = respository;
          

        }
        // Define objects
        private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        private readonly IWABARepository respository;
      

        public static Intents_List IntentHandlers { get; private set; }



        [HttpPost]
        public ContentResult GetWebhookResponse()
        {
            IntentHandlers = Register();


            WebhookRequest request;
            using (var reader = new StreamReader(Request.Body))
            {
                request = jsonParser.Parse<WebhookRequest>(reader);
            }

            var bot_agent = Utilities.Model_Mapper.DialogFlow_To_Model(request);
            if (bot_agent == null)
            {
                return null;
            }
            bot_agent = Intent_Router.Process(bot_agent);


            return Content(Model_Mapper.Module_To_DialogFlow(bot_agent).ToString(), "application / json");
        }



        // Register list of intents
        public  Intents_List Register()
        {
            Intents_List IntentHandlers = new Intents_List();
            Customer_Intent ci = new Customer_Intent(respository);
            Products_Intent pi = new Products_Intent(respository);
            Welcome_Intent wi = new Welcome_Intent(respository);
            IntentHandlers.Add("Welcome", (mo) => wi.Process(mo));
            IntentHandlers.Add("Welcome.Followup", (mo) => wi.Welcome_Followup(mo));
            
            IntentHandlers.Add("Customer.Mobile", (mo) => ci.Customer_Mobile(mo));
            IntentHandlers.Add("Pincode", (mo) => ci.Customer_Pincode(mo));


            IntentHandlers.Add("Product.House.Type", (mo) => pi.House_Type(mo));
            IntentHandlers.Add("Product.City.Weather", (mo) => pi.City_Weather(mo));
            IntentHandlers.Add("Product.Member.Showers", (mo) => pi.Member_Showers(mo));
            IntentHandlers.Add("Product.Bath.Timing", (mo) => pi.Bath_Timing(mo));
            IntentHandlers.Add("Product.Shower.Usage", (mo) => pi.Shower_Usage(mo));
            IntentHandlers.Add("Product.Product.Type", (mo) => pi.Product_Type(mo));


            IntentHandlers.Add("Menu", (mo) => Intent.Menu_Intent.Menu(mo));
            IntentHandlers.Add("Fall_Back", (mo) => Intent.FallBack.Fall_Back(mo));



            //------------------- Dealer--------------------------------------
            IntentHandlers.Add("Welcome.Dealer", (mo) => wi.Dealer_Process(mo));
            IntentHandlers.Add("Welcome.Followup.Dealer", (mo) => wi.Dealer_Welcome_Followup(mo));
            IntentHandlers.Add("Customer.Mobile", (mo) => wi.Dealer_Welcome_Followup(mo));
            IntentHandlers.Add("Fall_Back", (mo) => Intent.FallBack.Fall_Back(mo));

            return IntentHandlers;
        }




    }
}