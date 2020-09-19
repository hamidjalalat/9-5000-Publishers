using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class LessonRepository : Repository<Models.Lesson>, ILessonRepository
    {
        public LessonRepository(Models.DatabaseContext databaseContext)
        : base(databaseContext)
        {

        }
    }
}
