using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KIS.AI.Hackathon.Template.Models
{
    public class Answer
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public int AnswerNumber { get; set; }

        public string DocumentId { get; set; }

    }
}