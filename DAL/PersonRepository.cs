using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
   public class PersonRepository: Repository<Models.Person>, IPersonRepository
    {
        public PersonRepository(Models.DatabaseContext databaseContext)
        : base(databaseContext)
        {
        }
    }
}
