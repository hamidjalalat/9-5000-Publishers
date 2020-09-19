using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class PageBaseRepository : Repository<Models.PageBase>, IPageBaseRepository
    {
        public PageBaseRepository(Models.DatabaseContext databaseContext)
     : base(databaseContext)
        {
        }
    }
}
