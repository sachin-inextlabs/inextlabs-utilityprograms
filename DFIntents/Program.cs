using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Text;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Cloud.Dialogflow.V2;
using Google.Api.Gax;
using Google.LongRunning;
using Google.Protobuf.WellKnownTypes;
using Google.Apis.Requests.Parameters;
using Google.Protobuf.Collections;

namespace DFIntents
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectID = "retaildf-ldce-585aeaa00259.json";
            
            //System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "retaildf-ldce-585aeaa00259.json");
           //Authentication using service acccounts. It is better that this is stored in Key Vault 
           //Service Account must be created from GCP project and saved as JSON
            var credential = GoogleCredential.FromFile(projectID);

            IntentsClient intentsClient = IntentsClient.Create();
            //Attach the intent request to the project  
            ListIntentsRequest request = new ListIntentsRequest
            {
                ParentAsAgentName = AgentName.FromProject("retaildf-ldce"),
                LanguageCode = "",
                IntentView = IntentView.Full,
            };
            // Make the request
            PagedEnumerable<ListIntentsResponse, Intent> response = intentsClient.ListIntents(request);

                

            foreach (Intent item in response)
            {
                Console.WriteLine(item.DisplayName);
                Console.WriteLine("Total Count is {0}",item.TrainingPhrases.Count());
                /*foreach(Intent.Types.TrainingPhrase phrase in item.TrainingPhrases)
                {
                    Console.WriteLine(phrase.Name);
                }*/
            }

        }
    }
}
