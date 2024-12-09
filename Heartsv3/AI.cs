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
        
        string apiKey = "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";
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
            //Console.WriteLine("Response: " + content);
            return int.Parse(content);
        
    }
}
