using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class Paper:BaseEntity
    {
        public Paper() : base()
        {

        }
        public int BookId { get; set; }
        public string Paper1 { get; set; }
        public string Paper2 { get; set; }
        public string Paper3 { get; set; }
        public string Paper4 { get; set; }
        public string Paper5 { get; set; }
        public string Paper6 { get; set; }
        public string Paper7 { get; set; }
    }
}
