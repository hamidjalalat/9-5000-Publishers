using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class LessonType:BaseEntity
    {
        public LessonType() : base()
        {

        }
        public string Name { get; set; }
    }
}
