using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;

namespace KIS.AI.Hackathon.Template.Models
{
    [SerializePropertyNamesAsCamelCase]
    public class Ingredient
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }

        [JsonProperty("quantityTo")]
        public string QuantityTo { get; set; }

        [JsonProperty("seperator")]
        public bool Seperator { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

    }
}