using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KIS.AI.Hackathon.Template.Models
{
    public class VisionResultModel
    {
        public VisionResultModel()
        {
            RecipeName = new List<string>();
            RecipeContent = new List<dynamic>();
            PictureInput = string.Empty;
        }

        public List<string> RecipeName { get; set; }
        
        public List<dynamic> RecipeContent { get; set; }

        public string PictureInput { get; set; }
    }
}