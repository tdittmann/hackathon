using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using KIS.AI.Hackathon.Template.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System.Globalization;
using System.Collections.Generic;

namespace KIS.AI.Hackathon.Template.Services
{
    [Serializable]
    public class AzureSearchClient : IAzureSearchClient
    {
        private readonly string searchServiceName = ConfigurationManager.AppSettings["Search:ServiceName"];
        private readonly string queryApiKey = ConfigurationManager.AppSettings["Search:ApiKey"];
        private readonly string searchIndexName = ConfigurationManager.AppSettings["Search:IndexName"];
        private readonly string scoringProfileName = ConfigurationManager.AppSettings["Search:ScoringProfileName"];

        private static readonly string stringMinimumScore = ConfigurationManager.AppSettings["Search:MinimumScore"];
        private static readonly string stringConfidenceLevelFactor = ConfigurationManager.AppSettings["Search:ConfidenceLevelFactor"];
        private static readonly string stringTopNResults = ConfigurationManager.AppSettings["Search:TopNResults"];

        private static SearchIndexClient _indexClient = null;

        public async Task<DocumentSearchResult<RecipeSearchResultModel>> GetSearchResults(string q, string searchFields = "")
        {
            if (_indexClient == null)
            {
                _indexClient = new SearchIndexClient(searchServiceName, searchIndexName, new SearchCredentials(queryApiKey));
            }

            SearchParameters searchParams = new SearchParameters();
            if (!string.IsNullOrEmpty(scoringProfileName))
                searchParams.ScoringProfile = scoringProfileName;

            if (!string.IsNullOrEmpty(searchFields))
            {
                searchParams.SearchFields = searchFields?.Split(',')?.ToArray();

            }


            try
            {
                var searchResult = await _indexClient?.Documents?.SearchAsync<RecipeSearchResultModel>(q, searchParams);

                return searchResult;
            }
            catch (Exception e)
            {
                return null;
            }


        }

        public Tuple<string[], bool> GetKbDocIds(DocumentSearchResult<RecipeSearchResultModel> srResult)
        {

            var highConfidenceResult = false;

            double minimumScore = (string.IsNullOrEmpty(stringMinimumScore)) ? 0.00 : double.Parse(stringMinimumScore);

            double confidenceLevelFactor = (string.IsNullOrEmpty(stringConfidenceLevelFactor)) ? 0 : double.Parse(stringConfidenceLevelFactor);

            int topNResults = (string.IsNullOrEmpty(stringTopNResults)) ? 1 : int.Parse(stringTopNResults);

            var kbTopNSearchResults = srResult.Results.Where(s => s.Score >= minimumScore).Take(topNResults).ToList();

            var confidenceLevel = CalculateConfidenceLevel(srResult, confidenceLevelFactor);

            if (confidenceLevel >= confidenceLevelFactor)
            {
                highConfidenceResult = true;
            }

            var docsIds = kbTopNSearchResults.Select(d => d.Document.Id).ToArray();

            return new Tuple<string[], bool>(docsIds, highConfidenceResult);

        }

        private static double CalculateConfidenceLevel(DocumentSearchResult<RecipeSearchResultModel> srResult, double confidenceLevelFactor)
        {
            if (srResult.Results.Count > 1 && srResult.Results[1].Score != 0)
            {
                return srResult.Results[0].Score / srResult.Results[1].Score;
            }
            return confidenceLevelFactor;
        }

    }

    public interface IAzureSearchClient
    {
        Task<DocumentSearchResult<RecipeSearchResultModel>> GetSearchResults(string q, string searchFields = "");

        Tuple<string[], bool> GetKbDocIds(DocumentSearchResult<RecipeSearchResultModel> srResult);
    }
}
