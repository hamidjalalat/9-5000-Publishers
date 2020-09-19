using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Lesson:BaseEntity
    {
        public Lesson() : base()
        {

        }
        public string Name { get; set; }

        [Display(Name = "نوع رشته تحصیلی")]
        public int FieldOfStudyId { get; set; }

        [Display(Name = "نوع رشته تحصیلی")]
        public int SectionId { get; set; }

        [Display(Name = "نوع درس")]
        public int LessonTypeId { get; set; }

        public bool HasGoldenTips { get; set; }

        public bool HasConceptualPoints { get; set; }

        public bool HasLetterCharts { get; set; }

        public bool HasLetterTables { get; set; }

        public bool HasMovies { get; set; }


    }
}
