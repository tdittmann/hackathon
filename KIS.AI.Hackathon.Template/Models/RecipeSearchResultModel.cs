using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using KIS.AI.Hackathon.Template.Models;

namespace KIS.AI.Hackathon.Template.Models
{
    [SerializePropertyNamesAsCamelCase]
    public class RecipeSearchResultModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("category")]
        public string[] Category { get; set; }

        [JsonProperty("cooking")]
        public string[] Cooking { get; set; }

        [JsonProperty("copyrightPhoto")]
        public string CopyrightPhoto { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("ingredients")]
        public List<dynamic> Ingredients { get; set; }

        [JsonProperty("portionCount")]
        public string PortionCount { get; set; }

        [JsonProperty("portionUnit")]
        public string PortionUnit { get; set; }

        [JsonProperty("recipeId")]
        public string RecipeId { get; set; }

        [JsonProperty("recipeName")]
        public string RecipeName { get; set; }

        [JsonProperty("searchKeywords")]
        public string SearchKeywords { get; set; }

        [JsonProperty("timeline")]
        public string Timeline { get; set; }

        [JsonProperty("tip")]
        public string Tip { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("videoUrl")]
        public string VideoUrl { get; set; }

        [JsonProperty("rid")]
        public string Rid { get; set; }

        }
}