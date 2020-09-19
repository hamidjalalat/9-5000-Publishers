using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
  public  class BookType:BaseEntity
    {
        public BookType() : base()
        {

        }
        public string Name { get; set; }

    }
}
