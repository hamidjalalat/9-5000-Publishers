using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class PageBaseFullList
    {
        public PageBaseFullList() : base()
        {

        }
        public Guid Id { get; set; }

        public int PageNumber { get; set; }

        public string ServerPath { get; set; }

        public int SessionNumber { get; set; }

        public string Content { get; set; }

        public bool Enable { get; set; }

        public int BookId { get; set; }

        public List<GoldenTip> GoldenTips { get; set; }

        public List<ConceptualPoint> ConceptualPoints { get; set; }

        public List<LetterChart> LetterCharts { get; set; }

        public List<LetterTable> LetterTables { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
