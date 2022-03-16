using Microsoft.AspNetCore.DataProtection;
using NPS.Models;
using NPS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsApp_Webhook.Models;
using WhatsApp_Webhook.Utilities;

namespace WhatsApp_Webhook.Intent
{
    public class Customer_Intent 
    {
        static string Api_Key = "f1a98253e4f7d16a419fb258bc16daaaac82f";
       
        public readonly IWABARepository respository;
   
        List<Pl_Store_Locator> pl_Store_Locators = new List<Pl_Store_Locator>();
      
        public Customer_Intent(IWABARepository respository)
        {
            this.respository = respository;
        
        }
        internal Bot_Agent Customer_Mobile(Bot_Agent agent)
        {
           // var service = agent.Request.Parameters.Fields["welcome"].StringValue;
           

            var service = agent.Request.OutputContexts.Fields["welcome"].StringValue;

            var got_service = service.Length > 0;
            var customer_mobile = agent.Request.Parameters.Fields["customer_mobile"].StringValue;

            var got_customer_mobile = customer_mobile.Length > 0;

             if (!got_customer_mobile)
            {
                agent.Response.Text = "Oops! It seems you have entered a wrong mobile number.\nPlease enter your customer valid 10 digits mobile number";
                agent.Context_Name = "Awaiting_Customer_Mobile";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }


            else if (got_service && got_customer_mobile)
            {


                if (service == "complaint" || service == "installation")
                {
                    //string Base_Url = "https://aristonservice.in/Racold_Customer/complaint.aspx?";
                    //string LongURl = string.Format(Base_Url + @"mobile={0}&service={1}&timestamp={2}&utm={3}&dealer={4}", MyCrypto.EncryptString("91" + customer_mobile), MyCrypto.EncryptString(service), MyCrypto.EncryptString(DateTime.Now.ToString("yyyyMMddHHmm")), MyCrypto.EncryptString("WB"), MyCrypto.EncryptString(agent.Session.Session_ID));

                    //string LongUrl =  Utilities.Url_Shortner.GetComplaintURL(agent.Session.Session_ID, "WABA").Result;
                    //string LongURl = "mobile=9820155930&service=installation&timestamp=11255455&utm=whatsapp";

                    //string Base_Url = "https://www.racold.net/CallCenter/CustomerComplaintRegistration";
                    //string LongURl = $"{Base_Url}?Id={ protector.Protect(agent.Session.Session_ID)}?Source={ protector.Protect("WABA")}?Genrate={protector.Protect(DateTime.Now.ToString("yyyy-MM-dd HH:mm"))}";


                    string LongUrl = Utilities.Url_Shortner.GetComplaintURL(customer_mobile, "DLRWABA", agent.Session.Session_ID).Result;

                    var result = Utilities.Url_Shortner.ShortenUrl(Api_Key, LongUrl).Result;


                    agent.Response.Text = "You can book your complaint through below link\n" + result.url.shortLink + "\n\nFor any further assistance write to us at *customer.care@racold.com*" + Response_Helper.EndMessage;


                }
            }

            return agent;
        }

        

        internal Bot_Agent Customer_Pincode(Bot_Agent agent)
        {
            StringBuilder strMessage = new StringBuilder();
            var pincode = agent.Request.Parameters.Fields["pincode"].StringValue;

            var got_pincode = pincode.Length > 0;

            if (!got_pincode)
            {
                agent.Response.Text = "Oops!🤭 It seems you have entered a wrong pincode.\nPlease enter your valid 6 digits postal code";
                agent.Context_Name = "Awaiting_Pincode";
            }
            else 
            {
                pl_Store_Locators = respository.Get_Store_Locator(pincode).OrderBy(l => l.Business_Name).ToList();

                if (pl_Store_Locators.Count > 0)
                {
                    strMessage.Append("*Racold Partner Stores(" + pl_Store_Locators.Count + "*) near you, based on the information shared:").AppendLine();
                    int index = 1;
                    
                    foreach (Pl_Store_Locator value in pl_Store_Locators)
                    {
                        strMessage.Append("*" + index + ". " + value.Business_Name.Trim() + "*").AppendLine();
                        strMessage.Append(value.Address_1.Trim()).AppendLine();
                        strMessage.Append(value.Address_2.Trim() +" "+ value.Address_3.Trim()).AppendLine();
                        //if (value.Address_3 != " " || value.Address_3 != null)
                        //{
                        //    strMessage.Append(value.Address_3.Trim()).AppendLine();

                        //}

                        strMessage.Append(value.City.Trim() + ", "+ value.State.Trim() + " " + value.Postal_Code.Trim()).AppendLine();
                        strMessage.Append(value.Primary_Phone.Trim()).AppendLine();
                        strMessage.Append(value.Short_URL.Trim()).AppendLine().AppendLine();

                        index++;
                    }
                    strMessage.Append("*Thank you for choosing Racold Water Heaters* 😃").AppendLine();
                    
                    agent.Response.Text = strMessage.ToString() + Response_Helper.EndMessage;
                }
                else 
                {
                    //agent.Response.Text = "We are sorry!😕 Unable to process your request.\nKindly help us with any other pin code or area to help you better. \nVisit www.Racold.com for any further input.";
                    agent.Response.Text = "Kindly locate a store near you by visiting the store locator on our website - https://cutt.ly/QgzL7l1 \n\n*Thank you for choosing Racold Water Heaters* 😃\n\nWe value your time! Please type *0* or *menu* for the Home menu, if you need any further assistance!\n\nAlways here for you *24*7*. Just say *Hi* the next time you may need any assistance.\n\nYou may visit www.Racold.com for further inputs.";
                    //agent.Response.Text = "Oops! There is no retail shop against this pincode. We will available you soon";
                    //agent.Response.Text = "Oops! Currently no outlet is assign on input pin code, we have noted your request. Please try after same days, will update our database soon.";
                }
            }


            return agent;
        }
    }
}
