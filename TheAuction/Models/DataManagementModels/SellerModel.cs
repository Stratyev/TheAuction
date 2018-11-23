using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using TheAuction.Infrastructure;
using CodeFirst;

namespace TheAuction.Models
{
    public class SellerModel
    {
        protected MyDbContext _dbContext;
        public SellerModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public List<Seller> getSellers()
        {
            return _dbContext.Sellers.ToList();
        }
        public Seller setSeller(Seller _seller)
        {
            _dbContext.Sellers.Add(_seller);
            _dbContext.SaveChanges();
            return _seller;
        }
        public void deleteSeller(Seller _seller)
        {
            _dbContext.Sellers.Remove(_seller);
            _dbContext.SaveChanges();
        }
    }
}