using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;

namespace KIS.AI.Hackathon.Template.Models
{ 

    public class ArticleSearchResultModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("rid")]
        public string Rid { get; set; }
    }
}