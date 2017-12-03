using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.IsDeleted == false).OrderByDescending(p=>p.ProductId);
        }

        public IQueryable<Product> GetTop10()
        {
            return All().Take(10);
        }

        public Product Find(int id)
        {
            return All().FirstOrDefault(p => p.ProductId == id);
        }
    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}