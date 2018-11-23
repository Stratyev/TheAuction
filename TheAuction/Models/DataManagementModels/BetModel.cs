using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheAuction.Models;
using TheAuction.Infrastructure;
using CodeFirst;

namespace TheAuction.Models
{
    public class BetModel
    {
        MyDbContext _dbContext;
        public BetModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Bet> getBets()
        {
            List<Bet> bets = _dbContext.Bets.ToList();
            return bets;
        }
        public Bet getBetById(int _id)
        {
            List<Bet> bets = getBets();
            Bet _bet = bets.FirstOrDefault(item => item.Bet_id == _id);
            return _bet;
        }
        public Bet getBetById(int _id, List<Bet> bets)
        {
            Bet _bet = bets.FirstOrDefault(item => item.Bet_id == _id);
            return _bet;
        }
        public Bet setBet(Bet _bet)
        {

            _dbContext.Bets.Add(_bet);
            _dbContext.SaveChanges();
            return _bet;
        }
    }
}