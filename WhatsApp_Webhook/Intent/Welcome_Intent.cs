using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using NPS.Models;
using NPS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsApp_Webhook.Models;
using WhatsApp_Webhook.Utilities;


namespace WhatsApp_Webhook.Intent
{
    public class Welcome_Intent
    {
        static string Api_Key = "f1a98253e4f7d16a419fb258bc16daaaac82f";
     
        private readonly IWABARepository respository;

        public Welcome_Intent(IWABARepository respository)
        {
            this.respository = respository;


        }

        //string Base_Url = "https://aristonservice.in/Racold_Customer/complaint.aspx?";
        internal  Bot_Agent Process(Bot_Agent agent)
        {
           

            //agent.Response.Text = "Hi, Welcome to our official WhatsApp Account!\nGet started right away by selecting the below option. 😃\n1. *Registering a new complaint* \n2. *New Installation* \n\n💡 Type *1* or *2* to make a selection the options.";
            //// agent.Response.Text = "Hi, Welcome to our official WhatsApp Account!\nGet started right away by selecting the below option. 😃\n1. *Registering a new complaint*\n2. *New Installation*\n3. *Choose the Right Water Heater*\n4. *Find Racold Store Near Me*\n\n💡 Type *1*, *2*, *3* or *4* to make a selection the options.";


            if (agent.Session.Id == "projects/waba-demo-giwt/agent/sessions/" + agent.Session.Session_ID)
            {
                agent.Response.Text = "Hi, Welcome to our official WhatsApp Account!\nGet started right away by selecting below option. 😃\n1. *Registering a new complaint*\n2. *New Installation*\n3. *Choose the Right Water Heater*\n4. *Find Racold Store Near Me*\n5. *Racold Range Catalogue*\n\n💡 Type *1*, *2*, *3*, *4* or *5* to make a selection from above options.";
            }
            else
            {
                //agent.Response.Text = "Hi, Welcome to our official WhatsApp Account!\nGet started right away by selecting the below option. 😃\n1. *Registering a new complaint*\n2. *New Installation*\n\n💡 Type *1* or *2* to make a selection the options.";
                agent.Response.Text = "Hi, Welcome to our official WhatsApp Account!\nGet started right away by selecting below option. 😃\n1. *Registering a new complaint*\n2. *New Installation*\n3. *Choose the Right Water Heater*\n4. *Find Racold Store Near Me*\n5. *Racold Range Catalogue*\n\n💡 Type *1*, *2*, *3*, *4* or *5* to make a selection from above options.";
            }


           
            agent.Context_Name = "Awaiting_Welcome_Followup";
            agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            return agent;
        }



        internal  Bot_Agent Welcome_Followup(Bot_Agent agent)
        {
            
            var service = agent.Request.Parameters.Fields["service"].StringValue;

            var got_service = service.Length > 0;

          


            if (!got_service)
            {
                agent.Response.Text = "I apologize but I may not have an answer to your query at this moment.\n💡 You can type *Menu* to see what I can do for you OR Please enter the proper *option number*.";
                agent.Context_Name = "Awaiting_Welcome_Followup";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));

            }
            else
            {


                if (service == "complaint" || service == "installation")
                {


                    //string Base_Url = "https://aristonservice.in/Racold_Customer/complaint.aspx?";
                    //string LongURl = string.Format(Base_Url + @"mobile={0}&service={1}&timestamp={2}&utm={3}", MyCrypto.EncryptString(agent.Session.Session_ID), MyCrypto.EncryptString(service), MyCrypto.EncryptString(DateTime.Now.ToString("yyyyMMddHHmm")), MyCrypto.EncryptString("WB"));

                    //string LongURl = "mobile=9820155930&service=installation&timestamp=11255455&utm=whatsapp";

                    //string Base_Url = "https://www.racold.net/CallCenter/CustomerComplaintRegistration";
                    //string LongURl = $"{Base_Url}?Id={ protector.Protect(agent.Session.Session_ID)}?Source={ protector.Protect("WABA")}?Genrate={protector.Protect(DateTime.Now.ToString("yyyy-MM-dd HH:mm"))}";
                    string LongUrl = Utilities.Url_Shortner.GetComplaintURL(agent.Session.Session_ID, "WABA", agent.Session.Session_ID).Result;

                    var result = Utilities.Url_Shortner.ShortenUrl(Api_Key, LongUrl).Result;

                    

                    agent.Response.Text = "You can book your complaint through below link\n" + result.url.shortLink + "\n\nFor any further assistance write to us at *customer.care@racold.com*" + Response_Helper.EndMessage;

                    //agent.Response.FollowupEventInput = new Google.Cloud.Dialogflow.V2.EventInput();
                    //agent.Response.FollowupEventInput.Name = "event_end";
                    //agent.Response.FollowupEventInput.LanguageCode = "en";
                    //agent.Response.FollowupEventInput.Parameters = new Struct() { Fields = { { "message", Value.ForString(agent.Response.Text) } } };
                    //agent.Context_Name = "end_message";
                    //agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));



                }
                else if (service == "product")
                {
                    // agent.Response.Text = "The feature you requested is currently unavailable. Please try again later. 😔\n\nFor any further assistance write to us at *customer.care@racold.com* ";
                    agent.Response.Text = "What kind of a house do you live in? 🏠\n1. *Apartment / Flat*\n2. *Individual House / Bungalow*\n\n💡 Type *1* or *2* to make a selection the options.";
                    agent.Context_Name = "Awaiting_House_Type";
                    agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));





                }
                else if (service == "store")
                {
                    //agent.Response.Text = "Please enter your valid 6 digits pincode\n\nE.g. *411006*";
                    agent.Response.Text = "Please enter your valid 6 digits pincode";
                    agent.Context_Name = "Awaiting_Pincode";
                    agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
                }
                else if (service == "catalogue")
                {
                    //agent.Response.Text = "Please enter your valid 6 digits pincode\n\nE.g. *411006*";
                    agent.Response.Text = "🙂 Thank you for choosing Racold Water Heaters, you can access our Range Catalogue by clicking on this link : https://cutt.ly/WE5aP5o We value your time!\n\nPlease type *0* or *menu* for the Home menu, if you need any further assistance!\n\nAlways here for you *24 * 7*. Just say *Hi* the next time you may need any assistance.\n\nYou may visit www.Racold.com for further inputs.";
                    agent.Context_Name = "Awaiting_Welcome_Followup";
                    agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
                }


                //string LongURl = string.Format(Base_Url + @"mobile={0}&service={1}&timestamp={2}&utm={3}", MyCrypto.EncryptString (agent.Session.Session_ID), MyCrypto.EncryptString(service), MyCrypto.EncryptString(DateTime.Now.ToString("yyyy-MM-dd HH:mm")) , MyCrypto.EncryptString("WB"));

                ////string LongURl = "mobile=9820155930&service=installation&timestamp=11255455&utm=whatsapp";

                //var result = Utilities.Url_Shortner.ShortenUrl(Api_Key, LongURl);


                //agent.Response.Text = "You can book your complaint through below link " + result.Result.url.shortLink + "\n\n For any assistance call at *18604252288* ";

            }

            return agent;
        }



        //----------------------------------------------- Dealer--------------------------------------

        internal  Bot_Agent Dealer_Process(Bot_Agent agent)
        {
            agent.Response.Text = "Hi, Welcome to our official WhatsApp Account!\nGet started right away by selecting the below option. 😃\n1. *Registering a new complaint* \n2. *New Installation* \n\n💡 Type *1* or *2* to make a selection from above options.";

            return agent;
        }


        internal  Bot_Agent Dealer_Welcome_Followup(Bot_Agent agent)
        {
            var service = agent.Request.Parameters.Fields["welcome"].StringValue;

            var got_service = service.Length > 0;


            //var customer_mobile = agent.Request.Parameters.Fields["customer_mobile"].StringValue;

            //var got_customer_mobile = customer_mobile.Length > 0;

            if (!got_service)
            {
                agent.Response.Text = "I apologize but I may not have an answer to your query at this moment.\n💡 You can type *Menu* to see what I can do for you OR Please enter the proper *option number*.";
                //agent.Context_Name = "Menu";
                //agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            
            }
            else 
            {
                agent.Response.Text = "Enter your customer 10 digits mobile number";
                agent.Context_Name = "Awaiting_Customer_Mobile";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }


        

            return agent;
        }
    }
}
