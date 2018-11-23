using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using TheAuction.Infrastructure;
using CodeFirst;

namespace TheAuction.Models
{
    public class CustomerModel
    {
        protected MyDbContext _dbContext;
        public CustomerModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public List<Customer> getCustomers()
        {
            return _dbContext.Customers.ToList();
        }
        public Customer setCustomer(Customer _customer)
        {
            _dbContext.Customers.Add(_customer);
            _dbContext.SaveChanges();
            return _customer;
        }
        public void deleteCustomer(Customer _customer)
        {
            _dbContext.Customers.Remove(_customer);
            _dbContext.SaveChanges();
        }
    }
}