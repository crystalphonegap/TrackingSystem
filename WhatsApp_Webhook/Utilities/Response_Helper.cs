using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WhatsApp_Webhook.Models;

namespace WhatsApp_Webhook.Utilities
{
    public class Response_Helper
    {
        public static string EndMessage = "\n\nWe value your time! Please type *0* or *menu* for the Home menu, if you need further assistance!\n\nAlways here for you *24*7*. Just say *Hi* the next time you may need any assistance! Thank you.🙂\n\nYou may visit www.Racold.com for further inputs.";
        public static SessionEntityType GetEntityType(string Name, List<string> lsstring)
        {
            SessionEntityType sessionEntityType = new SessionEntityType();
           
            if (lsstring.Count > 0)
            {
                int Index = 1;
                foreach (string value in lsstring)
                {
                    EntityType.Types.Entity entity = new EntityType.Types.Entity();
                    entity.Value = value;
                    entity.Synonyms.Add(value);
                    entity.Synonyms.Add( Convert.ToString(Index));

                    sessionEntityType.Entities.Add(entity);
                    Index++;

                }
            }

            sessionEntityType.Name = Name;
            sessionEntityType.EntityOverrideMode = SessionEntityType.Types.EntityOverrideMode.Override;

            return sessionEntityType;
        }


        public static string GetTypeOptions(int Count)
        {
            string Message = string.Empty;

            switch (Count)
            {
                //case "FallBack.Welcome":
                //    agent.Response.Text = "Please type *Hi* for start your conversation.";
                //    agent.Context_Name = "waiting_family_type";

                //    break;
                case 2:
                    Message = "💡 Type *1* or *2* to make a selection the options.";

                    break;
                case 3:
                    Message = "💡 Type *1*, *2* or *3* to make a selection the options.";

                    break;
                case 4:
                    Message = "💡 Type *1*, *2*, *3* or *4* to make a selection the options.";

                    break;

                case 5:
                    Message = "💡 Type *1*, *2*, *3*, *4* or *5* to make a selection the options.";

                    break;

                default:
                    Message = "💡 Type option number to make a selection the options.";
                    break;
            }


            return Message;

        }

        public static string GetCategoryName(string Category)
        {
            string Message = string.Empty;

            switch (Category)
            {
                //case "FallBack.Welcome":
                //    agent.Response.Text = "Please type *Hi* for start your conversation.";
                //    agent.Context_Name = "waiting_family_type";

                //    break;
                case "ESWH":
                    Message = "Electric Storage Water Heater";

                    break;
                case "EIWH":
                    Message = "Electric Instant Water Heater";

                    break;
                case "HPWH":
                    Message = "Heat Pump Water Heater";

                    break;

                case "OIWH":
                    Message = "Online Instantaneous Water Heater";

                    break;
                case "GWH":
                    Message = "Gas Water Heater";

                    break;

                case "SWH":
                    Message = "Solar Water Heater";

                    break;

                default:
                    Message = "";
                    break;
            }


            return Message;

        }


        public static Context UpdateListContext(string Name, int LifespanCount = 1, Struct Parameters_Set = null) 
        {

            Context lct = new Context();
            lct.Name = Name;
            lct.LifespanCount = LifespanCount;
            lct.Parameters = Parameters_Set;


            return lct;
        }

        public static List_OutputContext OutputContext(string Name, int LifespanCount = 1, Struct Parameters_Set = null)
        {

            List_OutputContext lct = new List_OutputContext();
            lct.Name = Name;
            lct.LifespanCount = LifespanCount;
            lct.Parameters = Parameters_Set;


            return lct;
        }

        public static List<List_OutputContext> ListOutputContext(List_OutputContext list_OutputContext)
        {

            List<List_OutputContext> lct = new List<List_OutputContext>();

            lct.Add(list_OutputContext);

            return lct;
        }


    }
}
