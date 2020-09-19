using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class DatabaseContext:IdentityDbContext<User>
    {
        public DatabaseContext()
        {
           
        }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookType> BookTypes { get; set; }
        public virtual DbSet<FieldOfStudy> FieldOfStudys { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<LessonType> LessonTypes { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PageBase> PageBases { get; set; }
        public virtual DbSet<GoldenTip> GoldenTips { get; set; }
        public virtual DbSet<ConceptualPoint> ConceptualPoints { get; set; }
        public virtual DbSet<LetterChart> LetterCharts { get; set; }
        public virtual DbSet<LetterTable> LetterTables { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Paper> Paper { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=123;Persist Security Info=True;User ID=sa;Initial Catalog=DBA;Data Source=.");

        }
    }
}
