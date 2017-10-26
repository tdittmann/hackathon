using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KIS.AI.Hackathon.Template.Services;
using KIS.AI.Hackathon.Template.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;
using System.Collections.Generic;
using System.Dynamic;

namespace KIS.AI.Hackathon.Template.Services
{
    public class RecognitionService
    {
        public string filepath = "";
        public async Task<SearchResult> GetRecipeName(string searchQuery)
        {

            CustomVisionClient cVC = new CustomVisionClient();
            ImageRecognitionModel iRM = new ImageRecognitionModel();
            SearchResult sR = new SearchResult();
            Answer answer = new Answer();

            //1.Custom Vision Recognition Service nutzen

            filepath = searchQuery.ToString();

            iRM = await cVC.Request(filepath);

            //2.Azure Search mit dem Output von Custom Vision befüllen (Bsp. Seelachsfilet)
            IAzureSearchClient azSrchClient = new AzureSearchClient();
            var sr = await azSrchClient.GetSearchResults(iRM.Input, "");

            if (sr.Results == null || sr.Results.Count == 0)
            {
                return null;
            }


            //3. Dokumente von Cosmos DB abholen

            var getKbDocsOperation = azSrchClient.GetKbDocIds(sr);
            var dbClient = new CosmosDBClient();
            var kbResults = dbClient.GetDocsByIds(getKbDocsOperation.Item1);

                     

            if (String.IsNullOrEmpty(kbResults.ToString()))
            {
                answer.Title = "No Match";
                answer.Link = "No Match";
                answer.DocumentId = "No Match";
                answer.Description = "No Match";              
                answer.Content = "No Match";
                
            }
            else {
            foreach(var r in kbResults)
            {
                answer.Title = r.RecipeName;
                answer.Link = r.ImageUrl;
                answer.DocumentId = r.Id;
                answer.Description = r.RecipeId;
                foreach(var c in r.Cooking)
                {
                    answer.Content += c + " ";
                }
          


                sR.Answers.Add(answer);
            }
            }

            return sR;

        }
    }
}