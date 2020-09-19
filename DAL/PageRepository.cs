using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class PageRepository : Repository<Models.Page>, IPageRepository
    {
        public PageRepository(Models.DatabaseContext databaseContext)
     : base(databaseContext)
        {
        }
    }
}
