using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheAuction.Models;
using TheAuction.Infrastructure;
using CodeFirst;

namespace TheAuction.Models
{
    public class ConditionModel
    {
        MyDbContext _dbContext;
        public ConditionModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Condition> getConditions()
        {
            List<Condition> Conditions = _dbContext.Conditions.ToList();
            return Conditions;
        }
        public Condition getConditionById(int _id)
        {
            List<Condition> Conditions = getConditions();
            Condition _Condition = Conditions.FirstOrDefault(item => item.Condition_id == _id);
            return _Condition;
        }
        public Condition getConditionByName(string _name)
        {
            List<Condition> Conditions = getConditions();
            Condition _Condition = Conditions.FirstOrDefault(item => item.Name == _name);
            return _Condition;
        }
        public Condition getConditionById(int _id, List<Condition> Conditions)
        {
            Condition _Condition = Conditions.FirstOrDefault(item => item.Condition_id == _id);
            return _Condition;
        }
        public Condition getConditionByName(string _name, List<Condition> Conditions)
        {
            Condition _Condition = Conditions.FirstOrDefault(item => item.Name == _name);
            return _Condition;
        }
        public Condition setCondition(Condition _Condition)
        {

            _dbContext.Conditions.Add(_Condition);
            _dbContext.SaveChanges();
            return _Condition;
        }
       
    }
}