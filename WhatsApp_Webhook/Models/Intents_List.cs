using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsApp_Webhook.Models
{
    public class Intents_List : List<KeyValuePair<string, Func<Bot_Agent, Bot_Agent>>>
    {
        public void Add(string intentName, Func<Bot_Agent, Bot_Agent> function)
        {
            var intent =   this.FirstOrDefault( i =>  i.Key.ToLower() == intentName.ToLower());
            if (string.IsNullOrWhiteSpace(intent.Key))
            {
                Add(new KeyValuePair<string,  Func<Bot_Agent, Bot_Agent>>(intentName, function));
            }
           // await Task.CompletedTask;
        }


    }
}
