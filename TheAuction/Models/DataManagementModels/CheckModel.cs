using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheAuction.Models;
using TheAuction.Infrastructure;
using CodeFirst;

namespace TheAuction.Models
{
    public class CheckModel
    {
        MyDbContext _dbContext;
        public CheckModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Check> getChecks()
        {
            List<Check> checks = _dbContext.Checks.ToList();
            return checks;
        }
        public Check getCheckById(int _id)
        {
            List<Check> checks = getChecks();
            Check _check = checks.FirstOrDefault(item => item.Check_id == _id);
            return _check;
        }
        public Check getCheckById(int _id, List<Check> checks)
        {
            Check _check = checks.FirstOrDefault(item => item.Check_id == _id);
            return _check;
        }
        public Check setCheck(Check _check)
        {

            _dbContext.Checks.Add(_check);
            _dbContext.SaveChanges();
            return _check;
        }
        public Check editCheck(Check updCheck)
        {
            Check extCheck = getCheckById(updCheck.Check_id);

            extCheck = (Check)UpdateValues.Update(extCheck, updCheck);
            _dbContext.SaveChanges();
            return extCheck;
        }
    }
}