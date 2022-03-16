using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsApp_Webhook.Models;
using WhatsApp_Webhook.Utilities;

namespace WhatsApp_Webhook.Intent
{
    public class Menu_Intent
    {
        internal static Bot_Agent Menu(Bot_Agent agent)
        {
            //agent.Response.Text = "Hello, Welcome to _Racold India Service!_ \n *Get started right away by select the option number*\n*1.* Do you want to register a new complaint.\n*2.* For a new installation.\n*3.* know the status of your existing complaint.\n\n*Please enter the option number*";

            // agent.Response.Text = "Hello, Welcome to _Racold India Service!_ \n *Get started right away by select the option number*\n*1.* Do you want to register a new complaint.\n*2.* For a new installation.\n\n*Please enter the option number*";

            //agent.Response.Text = "Here's what I can help you with 👇\n1. *Registering a new complaint*\n2. *New Installation*\n3. *Choose the Right Water Heater*\n4. *Find Racold Store Near Me*\n\n💡 Type *1*, *2*, *3* or *4* to make a selection the options.";
            if (agent.Session.Id == "projects/waba-demo-giwt/agent/sessions/" + agent.Session.Session_ID)
            {
                //agent.Response.Text = "Here's what I can help you with 👇\n1. *Registering a new complaint*\n2. *New Installation*\n3. *Choose the Right Water Heater*\n4. *Find Racold Store Near Me*\n\n💡 Type *1*, *2*, *3* or *4* to make a selection the options.";
                agent.Response.Text = "Here's what I can help you with 👇\n1. *Registering a new complaint*\n2. *New Installation*\n3. *Choose the Right Water Heater*\n4. *Find Racold Store Near Me*\n5. *Racold Range Catalogue*\n\n💡 Type *1*, *2*, *3*, *4* or *5* to make a selection from above options.";
            }
            else
            {
                //agent.Response.Text = "Here's what I can help you with 👇\n1. *Registering a new complaint*\n2. *New Installation*\n\n💡 Type *1* or *2* to make a selection the options.";
                agent.Response.Text = "Here's what I can help you with 👇\n1. *Registering a new complaint*\n2. *New Installation*\n3. *Choose the Right Water Heater*\n4. *Find Racold Store Near Me*\n5. *Racold Range Catalogue*\n\n💡 Type *1*, *2*, *3*, *4* or *5* to make a selection from above options.";
                //agent.Response.Text = "Hi, Welcome to our official WhatsApp Account!\nGet started right away by selecting the below option. 😃\n1. *Registering a new complaint*\n2. *New Installation*\n3. *Choose the Right Water Heater*\n4. *Find Racold Store Near Me*\n5. *Racold Range Catalogue*\n\n💡 Type *1*, *2*, *3*, *4* or *5* to make a selection the options.";
            }

            //agent.Response.Text = "Here's what I can help you with 👇\n1. *Registering a new complaint*\n2. *New Installation*\n\n💡 Type *1* or *2* to make a selection the options.";
            agent.Context_Name = "Awaiting_Welcome_Followup";
            agent.Response.List_Contexts = Response_Helper.ListOutputContext(Response_Helper.OutputContext(agent.Context + agent.Context_Name, 1));
            return agent;
        }
    }
}
