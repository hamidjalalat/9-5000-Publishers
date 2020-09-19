using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
   public class FieldOfStudyRepository: Repository<Models.FieldOfStudy>, IFieldOfStudyRepository
    {
        public FieldOfStudyRepository(Models.DatabaseContext databaseContext)
       : base(databaseContext)
        {

        }
    }
}
