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

namespace WhatsApp_Webhook.Utilities
{
    public class Send_To_Dialogflow
    {
        public static DetectIntentResponse SendTextMessageToDialogFlow(string sessionId, string text, string languageCode = "en")
        {
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
            return response;
            //return response.QueryResult;
        }
    }
}
