using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
  public  class SectionRepository: Repository<Models.Section>, ISectionRepository
    {
        public SectionRepository(Models.DatabaseContext databaseContext)
     : base(databaseContext)
        {

        }
    }
}
