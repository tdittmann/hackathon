using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KIS.AI.Hackathon.Template.Models
{
    public class SearchResult
    {
        public SearchResult()
        {
            Answers = new List<Answer>();
            Related = new List<Answer>();
           
            SavedItemId = string.Empty;
            Question = string.Empty;
        }

        public List<Answer> Answers { get; set; }

        public List<Answer> Related { get; set; }

        public string SavedItemId { get; set; }

        public string Question { get; set; }
    }
}