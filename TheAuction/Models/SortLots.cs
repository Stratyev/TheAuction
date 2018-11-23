using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using CodeFirst;
using TheAuction.Models;

namespace TheAuction.Models
{
    public static class SortLots
    {
        public static List<Lot> Sort(string sortBy, int pageSize, int currPage, out int pagesCount, DataManager _dManager)
        {
            if(sortBy != null)
            {
                List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                    .Include(l => l.Location).Where(l => l.Condition.Name == "active").OrderBy(sortBy).ToList();
                pagesCount = (lots.Count + pageSize - 1) / pageSize;
                lots = lots.Skip((currPage - 1) * pageSize).Take(pageSize).ToList();
                return lots;
            }
            else
            {
                List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                   .Include(l => l.Location).Where(l => l.Condition.Name == "active").ToList();
                pagesCount = (lots.Count + pageSize - 1) / pageSize;
                lots.Skip((currPage - 1) * pageSize).Take(pageSize);
                return lots;
            }
        }
        public static List<Lot> Sort(string sortBy, string categoryName, int pageSize, int currPage, out int pagesCount, DataManager _dManager)
        {
            if (sortBy != null)
            {
                List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                   .Include(l => l.Location).Where(l => l.Category.Name == categoryName
                   && l.Condition.Name == "active").OrderBy(sortBy).ToList();
                pagesCount = (lots.Count + pageSize - 1) / pageSize;
                lots.Skip((currPage - 1) * pageSize).Take(pageSize);
                return lots;
            }
            else
            {
                List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                   .Include(l => l.Location).Where(l => l.Category.Name == categoryName
                   && l.Condition.Name == "active").ToList();
                pagesCount = (lots.Count + pageSize - 1) / pageSize;
                lots.Skip((currPage - 1) * pageSize).Take(pageSize);
                return lots;
            }
        }
    }
}