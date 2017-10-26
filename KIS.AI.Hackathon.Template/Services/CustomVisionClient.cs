using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Dynamic;
using System.Collections.Generic;
using KIS.AI.Hackathon.Template.Models;
using System.Configuration;

namespace KIS.AI.Hackathon.Template.Services
{
    public class CustomVisionClient
    {
        private readonly string _predictionKey = ConfigurationManager.AppSettings["CustomVision:PredictionKey"];
        private readonly string _predictionEndpointUrl = ConfigurationManager.AppSettings["CustomVision:PredictionEndpoint"];
        
        public async Task<ImageRecognitionModel> Request(string request)
        {
            ImageRecognitionModel visionRequest = new ImageRecognitionModel();


            //include filepath

            string filePath = request;
            try
            {
                byte[] byteData = GetFileAsByteArray(filePath);
                visionRequest = await MakeRequest(byteData);
            }
          catch (Exception e)
            {
                visionRequest.Input = e.ToString();
                return visionRequest;
            }

            return visionRequest;
        }
        public async Task<ImageRecognitionModel> MakeRequest(byte[] byteData)
        {
            ImageRecognitionModel iRM = new ImageRecognitionModel();
            var recognitionResult = string.Empty;

            iRM.Input = string.Empty;

            using (var content = new ByteArrayContent(byteData))
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Prediction-Key", _predictionKey);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                HttpResponseMessage response = await client.PostAsync(_predictionEndpointUrl, content);
                recognitionResult = await response.Content.ReadAsStringAsync();
            }
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(recognitionResult);
            var highscore = 0.9;

            foreach(var c in obj.Predictions)
            {
               if(c.Probability >= highscore)
                {
                    iRM.Input += c.Tag + " ";
                }
             
            }
            
            return iRM;
        }

        static byte[] GetFileAsByteArray(string filePath)
        {
            try
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
            catch (Exception e)
            {
                return null;
            }
                
         
           
        }
    }
}