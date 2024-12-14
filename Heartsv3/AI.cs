using System.Text.Json;

namespace Heartsv3;

using System;
using System.IO;
using System.Net;
using System.Text;

public class AI
{
    string resp;
    public AI(List<int> hand, List<int> pile)
    {
        //must replace with own key!
        //Write own key here
        string apiKey =
            "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";
        
        string endpoint = "https://api.openai.com/v1/chat/completions";

        // Create the HTTP request
        var request = (HttpWebRequest)WebRequest.Create(endpoint);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        // Prepare the JSON payload
        CardGameAI p = new CardGameAI(hand, pile);
        string pInp = p.ConstructPayload();
        string payload = pInp;

        byte[] byteArray = Encoding.UTF8.GetBytes(payload);
        request.ContentLength = byteArray.Length;

        // Write payload to the request stream
        using (Stream dataStream = request.GetRequestStream())
        {
            dataStream.Write(byteArray, 0, byteArray.Length);
        }

        // Get the response
        string responseText;
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }
            }

            resp = responseText;
            
    }
    
    public int GetCard()
    {
        
            var jsonResponse = JsonDocument.Parse(resp);
            //Console.WriteLine("Response got");
            string content = jsonResponse
                .RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
            Console.WriteLine("Response: " + content);
            return int.Parse(content);
        
    }
}
