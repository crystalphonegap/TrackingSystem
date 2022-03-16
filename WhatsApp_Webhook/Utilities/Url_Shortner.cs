using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WhatsApp_Webhook.Models;

namespace WhatsApp_Webhook.Utilities
{
    public class Url_Shortner
    {
        internal static async Task<Url_Shortner_Property>  ShortenUrl(string apiKey, string longURL)
        {
           
            string output;
            Url_Shortner_Property link;
            // Build the domain string depending on the selected domain type
            //if (domain == Domain.BITLY)
            //    _domain = "bit.ly";
            //else
            //    _domain = "j.mp";

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
            //    string.Format(@"https://cutt.ly/api/api.php?key={0}&short={1}",apiKey, HttpUtility.UrlEncode(longURL)));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
               string.Format(@"https://cutt.ly/api/api.php?key={0}&short={1}", apiKey, HttpUtility.UrlEncode(longURL)));
            request.Proxy = null;
            using (WebResponse response = await request.GetResponseAsync())

            //var response = (HttpWebResponse)request.GetResponse();
            //using (HttpWebResponse response = await request.GetResponseAsync)
            {
                using (StreamReader reader =  new StreamReader(response.GetResponseStream()))
                {
                    output = await reader.ReadToEndAsync();

                    using (var sr = new StringReader(output))
                    using (var jr = new JsonTextReader(sr))
                    {
                        var serial = new JsonSerializer();
                        serial.Formatting = Formatting.Indented;
                        link =  serial.Deserialize<Url_Shortner_Property>(jr);

                        //var reserializedJSON = JsonConvert.SerializeObject(link, Formatting.Indented);

                    }
                }
            }
            return await Task.FromResult<Url_Shortner_Property>(link);
            //return await link;
        }


        internal static async Task<string> GetComplaintURL(string MobileNo, string Source, string Created)
        {
            string result = null;

            // Build the domain string depending on the selected domain type
            //if (domain == Domain.BITLY)
            //    _domain = "bit.ly";
            //else
            //    _domain = "j.mp";
            //string.Format(@"https://www.racold.net/api/CallCenter/GetUrlWABAComplaint?Id={0}&Source={1}", MobileNo, Source);
            string Url = "https://www.racold.in/api/CallCenter/GetUrlWABAComplaint";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Url}/{MobileNo}/{Source}/{Created}");
            if (response != null)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return await Task.FromResult(JsonConvert.DeserializeObject<string>(result));
            
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
            //    string.Format(@"https://www.racold.net/api/CallCenter/GetUrlWABAComplaint?Id={0}&Source={1}", MobileNo, Source));
            //request.Proxy = null;
            //using (WebResponse response = await request.GetResponseAsync())

            ////var response = (HttpWebResponse)request.GetResponse();
            ////using (HttpWebResponse response = await request.GetResponseAsync)
            //{
            //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //    {
            //        output = await reader.ReadToEndAsync();

            //        using (var sr = new StringReader(output))
            //        using (var jr = new JsonTextReader(sr))
            //        {
            //            var serial = new JsonSerializer();
            //            serial.Formatting = Formatting.Indented;
            //            link = serial.Deserialize<Url_Shortner_Property>(jr);

            //            //var reserializedJSON = JsonConvert.SerializeObject(link, Formatting.Indented);

            //        }
            //    }
            //}
            //return await Task.FromResult<resu>(link);
            //return await link;
        }
    }
}
