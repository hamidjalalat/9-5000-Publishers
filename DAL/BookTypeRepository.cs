using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
   public class BookTypeRepository: Repository<Models.BookType>, IBookTypeRepository
    {
        public BookTypeRepository(Models.DatabaseContext databaseContext)
        : base(databaseContext)
        {

        }
    }
}
