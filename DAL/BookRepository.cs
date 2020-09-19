using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BookRepository : Repository<Models.Book>, IBookRepository
    {
        public BookRepository(Models.DatabaseContext databaseContext)
          : base(databaseContext)
        {

        }

    }
}
