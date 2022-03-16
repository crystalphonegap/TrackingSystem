using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using Grpc.Auth;
using Grpc.Core;
using WhatsApp_Webhook.Models;
using RestSharp;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Google.Protobuf;

namespace WhatsApp_Webhook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   
    public class DialogflowApiController : ControllerBase
    {

        private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
  

        [HttpGet("{sessionId}/{text}")]
        public ContentResult SendTextMessageToDialogFlow(string sessionId, string text)
        {
            string languageCode = "en";
            var query = new QueryInput
            {
                Text = new TextInput
                {
                    Text = text,
                    LanguageCode = languageCode

                }
            };



            var creds = GoogleCredential.FromFile("C:\\whatsapp-bfnkwj-697107d540d0.json");

            Channel channel = new Channel(
            SessionsClient.DefaultEndpoint.Host, SessionsClient.DefaultEndpoint.Port, creds.ToChannelCredentials());
            var client = SessionsClient.Create(channel);

            DetectIntentRequest request = new DetectIntentRequest
            {
                SessionAsSessionName = new SessionName("whatsapp-bfnkwj", sessionId),
                QueryInput = query


            };

            DetectIntentResponse response = client.DetectIntent(request);
            //tWebhookResponse();
            return Content(response.QueryResult.ToString(), "application / json");
            //return JsonResult(response.QueryResult);
            //return Ok(response.QueryResult);
            //return response.QueryResult;
        }

        public  async Task<Url_Shortner_Property> ShortenUrl()
        {

            string apiKey = "f1a98253e4f7d16a419fb258bc16daaaac82f";
            string longURL = "https://cutt.ly/api/api.php?key=f1a98253e4f7d16a419fb258bc16daaaac82f&short=https://aristonservice.in/Racold_Customer/complaint.aspx?mobile=Q0cl6injV9qijmmmrSkxZw==&service=b448IKrc2yzNpBqSpxoWeA==&timestamp=2njXZlr6zVuYnJA8U4cd/BcPVBSzcCblwvFxiIWIKy4=&utm=lqOVrXLNtFWPau878IK32g==";
            //    string content = string.Empty;

            string output;
            Url_Shortner_Property link;
            // Build the domain string depending on the selected domain type
            //if (domain == Domain.BITLY)
            //    _domain = "bit.ly";
            //else
            //    _domain = "j.mp";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                string.Format(@"https://cutt.ly/api/api.php?key={0}&short={1}", apiKey, longURL));
            request.Proxy = null;
            using (WebResponse response = await request.GetResponseAsync())

            //var response = (HttpWebResponse)request.GetResponse();
            //using (HttpWebResponse response = await request.GetResponseAsync)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    output = await reader.ReadToEndAsync();

                    using (var sr = new StringReader(output))
                    using (var jr = new JsonTextReader(sr))
                    {
                        var serial = new JsonSerializer();
                        serial.Formatting = Formatting.Indented;
                        link = serial.Deserialize<Url_Shortner_Property>(jr);

                        //var reserializedJSON = JsonConvert.SerializeObject(link, Formatting.Indented);

                    }
                }
            }
            return await Task.FromResult<Url_Shortner_Property>(link);
            //return await link;
        }


        [HttpGet("WABADemo/{sessionId}/{text}")]
        public ContentResult SendTextMessageToDialogFlowWABADemo(string sessionId, string text)
        {
            string languageCode = "en";
            var query = new QueryInput
            {
                Text = new TextInput
                {
                    Text = text,
                    LanguageCode = languageCode

                }
            };



            var creds = GoogleCredential.FromFile("C:\\waba-demo-giwt-5404490a5d9b.json");

            Channel channel = new Channel(
            SessionsClient.DefaultEndpoint.Host, SessionsClient.DefaultEndpoint.Port, creds.ToChannelCredentials());
            var client = SessionsClient.Create(channel);

            DetectIntentRequest request = new DetectIntentRequest
            {
                SessionAsSessionName = new SessionName("waba-demo-giwt", sessionId),
                QueryInput = query


            };

            DetectIntentResponse response = client.DetectIntent(request);
            //tWebhookResponse();
            return Content(response.QueryResult.ToString(), "application / json");
            //return JsonResult(response.QueryResult);
            //return Ok(response.QueryResult);
            //return response.QueryResult;
        }


    }
}