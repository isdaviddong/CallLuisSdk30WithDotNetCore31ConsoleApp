using System;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;

namespace testLuisSDK
{
    class Program
    {
        const string SubscriptionKey = "cefdc547a90c4ad19bd9bfd3f916b6fb";
        const string EndPoint = "https://luis20210604.cognitiveservices.azure.com/";
        const string ApplicationId = "8492e6ae-57ef-4015-adf7-1857e9b07e4a";
        static void Main(string[] args)
        {
            while (true)
            {

                Console.Write("please input utterance ('exit' for quit) : ");
                var utterance = Console.ReadLine();
                if (utterance == "exit") break;

                var ret = predict(utterance);
                foreach (var item in ret.Prediction.Entities)
                {
                    Console.WriteLine($"Entities : {item.Key} - {item.Value}");
                }
                foreach (var item in ret.Prediction.Intents)
                {
                    Console.WriteLine($"Intents : {item.Key} - {item.Value.Score}");
                }
                Console.WriteLine($"\nTopIntent : {ret.Prediction.TopIntent}");
            }
        }

        static PredictionResponse predict(string utterance)
        {
            var client = new LUISRuntimeClient(new ApiKeyServiceClientCredentials(SubscriptionKey));
            client.Endpoint = EndPoint;
            // Predict
            try
            {
                var predictionRequest = new PredictionRequest
                {
                    Query = utterance
                };

                var result = client.Prediction.GetSlotPredictionAsync(new Guid(ApplicationId), "production", predictionRequest).Result;
                return result;
            }
            catch (Exception)
            {
                Console.WriteLine("\nSomething went wrong. Please Make sure your app is published and try again.\n");
                return null;
            }
        }
    }
}
