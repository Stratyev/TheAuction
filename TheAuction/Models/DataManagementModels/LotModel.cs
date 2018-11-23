using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using TheAuction.Infrastructure;
using CodeFirst;
using System.Data.Entity;

namespace TheAuction.Models
{
    public class LotModel
    {
        MyDbContext _dbContext;
        public LotModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Lot> getLots()
        {
            //List<Lot> Lots = _dbContext.Lots.ToList();

            List<Lot> Lots = _dbContext.Lots.ToList();
            return Lots;
                           
        }
        public Lot setLot(Lot _lot/*, Condition _condition*/)
        {
            //_condition.Lots.Add(_lot);

            _dbContext.Lots.Add(_lot);
            _dbContext.SaveChanges();
            return _lot;
        }
        public Lot getLotById(int _id)
        {
            List<Lot> lots = getLots();
            Lot _lot = lots.FirstOrDefault(item => item.Lot_id == _id);
            return _lot;
        }
        public Lot getLotById(int _id, List<Lot> lots)
        {
            Lot _lot = lots.FirstOrDefault(item => item.Lot_id == _id);
            return _lot;
        }
        public Lot editLot(Lot updLot)
        {
            Lot extLot = getLotById(updLot.Lot_id);

            extLot = (Lot)UpdateValues.Update(extLot, updLot);
            _dbContext.SaveChanges();
            return extLot;
        }
    }
}