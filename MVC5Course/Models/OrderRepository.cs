using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class OrderRepository : EFRepository<Order>, IOrderRepository
	{
        public IQueryable<Product> GetAll()
        {
            
            return null;
        }
	}

	public  interface IOrderRepository : IRepository<Order>
	{

	}
}