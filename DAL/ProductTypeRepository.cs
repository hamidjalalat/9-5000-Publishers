using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ProductTypeRepository: Repository<Models.ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(Models.DatabaseContext databaseContext)
    : base(databaseContext)
        {
        }
    }
}
