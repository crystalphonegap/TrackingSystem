using Google.Apis.Util;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.WellKnownTypes;
using NPS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsApp_Webhook.Models;
using NPS.Models;
using WhatsApp_Webhook.Utilities;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace WhatsApp_Webhook.Intent
{
    public class Products_Intent
    {
        private readonly IWABARepository repository;
        public IEnumerable<Context> contexts { get; }


        public Products_Intent(IWABARepository repository)
        {
            this.repository = repository;
        }
        internal Bot_Agent House_Type(Bot_Agent agent)
        {
            var house_type = agent.Request.Parameters.Fields["house_type"].StringValue;

            var got_house_type = house_type.Length > 0;
            if (!got_house_type)
            {
                agent.Response.Text = "Oops!🤭 It seems you have entered a wrong option number.\nYou can type *Menu* to see what I can do for you OR Please enter the proper *option number*.";
                agent.Context_Name = "Awaiting_House_Type";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }
            else
            {
                agent.Response.Text = "Select an option that describes the temperature during winters in your city. 🌞 🌡\n1. *Moderate (Above 15 Degree)*\n2. *Cold (6 Degree to 15 Degree)*\n3. *Extreme Cold (Below 5 Degree)*\n\n💡 Type *1*, *2* or *3* to make a selection from above options.";
                agent.Context_Name = "Awaiting_City_Weather";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }


            return agent;
        }


        internal Bot_Agent City_Weather(Bot_Agent agent)
        {
            var city_weather = agent.Request.Parameters.Fields["city_weather"].StringValue;

            var got_city_weather = city_weather.Length > 0;
            if (!got_city_weather)
            {
                agent.Response.Text = "Oops!🤭 It seems you have entered a wrong option number.\nYou can type *Menu* to see what I can do for you OR Please enter the proper *option number*.";
                agent.Context_Name = "Awaiting_City_Weather";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }
            else
            {
                agent.Response.Text = "How many people take back-to-back bath in your house? 🚿\n1. *1*\n2. *2*\n3. *3 and above*\n\n💡 Type *1*, *2* or *3* to make a selection from above options.";
                agent.Context_Name = "Awaiting_Member_Showers";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }


            return agent;
        }

        internal Bot_Agent Member_Showers(Bot_Agent agent)
        {
            var member_showers = agent.Request.Parameters.Fields["member_showers"].StringValue;

            var got_member_showers = member_showers.Length > 0;
            if (!got_member_showers)
            {
                agent.Response.Text = "Oops!🤭 It seems you have entered a wrong option number.\nYou can type *Menu* to see what I can do for you OR Please enter the proper *option number*.";
                agent.Context_Name = "Awaiting_Member_Showers";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }
            else
            {
                agent.Response.Text = "What is the length of the shower preferred by you / your family members? 🚿⏳\n1. *Quick Showers*\n2. *Long Showers*\n3. *Leisure Showers*\n\n💡 Type *1*, *2* or *3* to make a selection from above options.";
                //agent.Response.Text = "How long do you / your family members take shower? 🚿⏳\n1. *4-5 minutes*\n2. *5-10 minutes*\n3. *10-15 minutes*\n\n💡 Type *1*, *2* or *3* to make a selection from above options.";
                //agent.Response.Text = "How long do you / your family members take shower? 🚿⏳\n1. *4-5 minutes*\n2. *5-10 minutes*\n3. *10-15 minutes*\n4. *Above 15 minutes*\n\n💡 Type *1*, *2*, *3* or *4* to make a selection the options.";
                agent.Context_Name = "Awaiting_Bath_Timing";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }


            return agent;
        }

        internal Bot_Agent Bath_Timing(Bot_Agent agent)
        {
            var bath_timing = agent.Request.Parameters.Fields["bath_timing"].StringValue;

            var got_bath_timing = bath_timing.Length > 0;
            if (!got_bath_timing)
            {
                agent.Response.Text = "Oops!🤭 It seems you have entered a wrong option number.\nYou can type *Menu* to see what I can do for you OR Please enter the proper *option number*.";
                agent.Context_Name = "Awaiting_Bath_Timing";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }
            else
            {
                var house_type = agent.Request.OutputContexts.Fields["house_type"].StringValue;
                var city_weather = agent.Request.OutputContexts.Fields["city_weather"].StringValue;
                var member_showers = agent.Request.OutputContexts.Fields["member_showers"].StringValue;


                var Product_ID = repository.Get_ProductID(house_type, city_weather, member_showers, bath_timing);

                List<Pl_Product_Line> pl_line = new List<Pl_Product_Line>();

                pl_line = repository.Get_Product(Product_ID.Product_ID);

                var BathUsage = pl_line.Select(o => o.Bath_Usage).Distinct().ToList();


                if (BathUsage.Count > 1)
                {
                    StringBuilder strMessage = new StringBuilder();
                    agent.Response.SessionEntityType = Utilities.Response_Helper.GetEntityType("projects/whatsapp-bfnkwj/agent/sessions/" + agent.Session.Session_ID + "/entityTypes/showers_usage", BathUsage);
                    int index = 1;
                    strMessage.Append("How do you consume hot water in your house? 🛀").AppendLine();
                    foreach (string Value in BathUsage)
                    {
                        strMessage.Append(index + ". *" + Value + "*").AppendLine();
                        index++;
                    }
                    strMessage.Append("\n" + Utilities.Response_Helper.GetTypeOptions(BathUsage.Count));

                    agent.Response.Text = strMessage.ToString();


                    agent.Parameters_Set = new Struct() { Fields = { { "product_id", Value.ForString(Product_ID.Product_ID) } } };

                    List<List_OutputContext> List_OutputContext = new List<List_OutputContext>();

                    agent.Context_Name = "Awaiting_Bath_Usage";

                    var au = Response_Helper.OutputContext(agent.Context + agent.Context_Name, 2, agent.Parameters_Set);
                    List_OutputContext.Add(au);
                    agent.Response.List_Contexts = List_OutputContext;


                }
                else
                {
                    agent.Parameters_Set = new Struct() { Fields = { { "product_id", Value.ForString(Product_ID.Product_ID) }, { "showers_usage", Value.ForString(BathUsage[0]) } } };

                    List<List_OutputContext> List_OutputContext = new List<List_OutputContext>();

                    agent.Context_Name = "Awaiting_Bath_Usage";

                    var au = Response_Helper.OutputContext(agent.Context + agent.Context_Name, 2, agent.Parameters_Set);
                    List_OutputContext.Add(au);
                    agent.Response.List_Contexts = List_OutputContext;

                }


               

            }


            return agent;
        }

        internal Bot_Agent Shower_Usage(Bot_Agent agent)
        {
            var showers_usage = agent.Request.Parameters.Fields["showers_usage"].StringValue;

            var got_showers_usage = showers_usage.Length > 0;
            if (!got_showers_usage)
            {
                agent.Response.Text = "Oops!🤭 It seems you have entered a wrong option number.\nYou can type *Menu* to see what I can do for you OR Please enter the proper *option number*.";
                agent.Context_Name = "Awaiting_Bath_Usage";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }
            else
            {
                var product_id = agent.Request.OutputContexts.Fields["product_id"].StringValue;
                List<Pl_Product_Line> pl_line = new List<Pl_Product_Line>();
                pl_line = repository.Get_Product(product_id).Where(p => p.Bath_Usage == showers_usage).ToList();

                var product_type = pl_line.Select(o => o.Product_Type).Distinct().ToList();


                if (product_type.Count > 1)
                {
                    StringBuilder strMessage = new StringBuilder();
                    agent.Response.SessionEntityType = Utilities.Response_Helper.GetEntityType("projects/whatsapp-bfnkwj/agent/sessions/" + agent.Session.Session_ID + "/entityTypes/product_type", product_type);
                    int index = 1;
                    strMessage.Append("What is the preferred installation type? 🧰").AppendLine();
                    foreach (string Value in product_type)
                    {
                        strMessage.Append(index + ". *" + Value.Trim() + "*").AppendLine();
                        index++;
                    }
                    strMessage.Append("\n" + Utilities.Response_Helper.GetTypeOptions(product_type.Count));

                    agent.Response.Text = strMessage.ToString();

                    List<List_OutputContext> List_OutputContext = new List<List_OutputContext>();

                    agent.Context_Name = "Awaiting_Product_Type";

                    var au = Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1);
                    List_OutputContext.Add(au);
                    agent.Response.List_Contexts = List_OutputContext;
                }
                else
                {


                    //List<List_OutputContext> List_OutputContext = new List<List_OutputContext>();
                    //agent.Parameters_Set = new Struct() { Fields = { { "product_type", Value.ForString(product_type[0]) } } };
                    //agent.Context_Name = "Awaiting_Product_Type";

                    //var au = Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1, agent.Parameters_Set);
                    //List_OutputContext.Add(au);
                    //agent.Response.List_Contexts = List_OutputContext;



                    agent.Response.FollowupEventInput = new Google.Cloud.Dialogflow.V2.EventInput();
                    agent.Response.FollowupEventInput.Name = "direct_product_type";
                    agent.Response.FollowupEventInput.LanguageCode = "en";
                    agent.Response.FollowupEventInput.Parameters = new Struct() { Fields = { { "product_type", Value.ForString(product_type[0]) } } };

                    agent.Response.Text = "";

                }

            }


            return agent;
        }


        internal Bot_Agent Product_Type(Bot_Agent agent)
        {
            var product_type = agent.Request.Parameters.Fields["product_type"].StringValue;

            var got_showers_usage = product_type.Length > 0;
            if (!got_showers_usage)
            {
                agent.Response.Text = "Oops!🤭 It seems you have entered a wrong option number.\nYou can type *Menu* to see what I can do for you OR Please enter the proper *option number*.";
                agent.Context_Name = "Awaiting_Product_Type";
                agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            }
            else
            {
                var product_id = agent.Request.OutputContexts.Fields["product_id"].StringValue;
                var showers_usage = agent.Request.OutputContexts.Fields["showers_usage"].StringValue;
                List<Pl_Product_Line> pl_line = new List<Pl_Product_Line>();
                //pl_line = repository.Get_Product(product_id).Where(p => p.Product_Type == product_type).ToList();

                // var product_selection = pl_line.Select(o => o.Product_Type).Distinct().ToList();
                var product_selection = repository.Get_Product(product_id).Where(p => p.Bath_Usage == showers_usage && p.Product_Type == product_type).ToList();
                // var product_selection = repository.Get_Product(product_id).Where(p => p.Product_Type == product_type).ToList();
                if (product_selection.Count > 0)
                {
                    StringBuilder strMessage = new StringBuilder();

                    int index = 1;
                    strMessage.Append("Recommended products based on your inputs, click on the link for the product details: ").AppendLine().AppendLine();
                    foreach (Pl_Product_Line Value in product_selection)
                    {
                        strMessage.Append(index + ". *" + Value.Product.Trim() + "*").AppendLine();
                        strMessage.Append(Response_Helper.GetCategoryName(Value.Category.Trim())).AppendLine();
                        strMessage.Append(Value.Short_URL.Trim()).AppendLine().AppendLine();
                        index++;
                    }
                    strMessage.Append("🙂 Thank you for choosing Racold Water Heaters.");

                    agent.Response.Text = strMessage.ToString() + Response_Helper.EndMessage;

                }


            }


            return agent;
        }

     
    } 
}
