using System;
using System.Configuration;
using System.Linq;
using Microsoft.Azure.Documents.Client;
using System.Collections.Generic;
using KIS.AI.Hackathon.Template.Models;
using KIS.AI.Hackathon.Template.Services;

namespace KIS.AI.Hackathon.Template.Services
{
    public class CosmosDBClient
    {
        private readonly string docDbEndpointUri = ConfigurationManager.AppSettings["CosmosDb:EndpointUri"];
        private readonly string docDbPrimaryKey = ConfigurationManager.AppSettings["CosmosDb:AuthKey"];
        private readonly string databaseName = ConfigurationManager.AppSettings["CosmosDb:Databasename"];
        private readonly string collectionName = ConfigurationManager.AppSettings["CosmosDb:CollectionName"];

        private static readonly string stringMinimumScore = ConfigurationManager.AppSettings["Search:MinimumScore"];
        private static readonly string stringConfidenceLevelFactor = ConfigurationManager.AppSettings["Search:ConfidenceLevelFactor"];
        private static readonly string stringTopNResults = ConfigurationManager.AppSettings["Search:TopNResults"];


        private static DocumentClient _client;

        public RecipeSearchResultModel GetDocById(string id)
        {
            if (_client == null)
            {
                _client = new DocumentClient(new Uri(docDbEndpointUri), docDbPrimaryKey);
            }

            var collectionLink = UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);

            RecipeSearchResultModel kbQueryItem = _client.CreateDocumentQuery<RecipeSearchResultModel>(collectionLink)
                                            .Where(so => so.Id == id)
                                            .AsEnumerable()
                                            .FirstOrDefault();

            return kbQueryItem;
        }

        public List<RecipeSearchResultModel> GetDocsByIds(IList<string> docsIds)
        {
            List<RecipeSearchResultModel> kbQueryItems = null;
            if (_client == null)
            {
                _client = new DocumentClient(new Uri(docDbEndpointUri), docDbPrimaryKey);
            }

            var collectionLink = UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);

            try
            {
                List<RecipeSearchResultModel> UnsortedkbQueryItems = _client.CreateDocumentQuery<RecipeSearchResultModel>(collectionLink)
                                         .Where(so => docsIds.Contains(so.Id))
                                         .AsEnumerable()
                                         .ToList();

                kbQueryItems = UnsortedkbQueryItems.OrderBy(d => docsIds.IndexOf(d.Id)).ToList();
            }
            catch (Exception ex)
            {

            }

            return kbQueryItems;
        }

    
}
}