using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class ProductType:BaseEntity
    {
        public ProductType() : base()
        {

        }
        public string Name { get; set; }
    }
}
