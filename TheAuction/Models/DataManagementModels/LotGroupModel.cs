using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirst;

namespace TheAuction.Models
{
    public class LotGroupModel
    {
        MyDbContext _dbContext;
        public LotGroupModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<LotGroup> getLotGroups()
        {
            List<LotGroup> LotGroups = _dbContext.LotGroups.OrderBy(lg => lg.LotGroup_id).ToList();
            return LotGroups;
        }
        public LotGroup getLotGroupById(int _id)
        {
            List<LotGroup> LotGroups = getLotGroups();
            LotGroup _LotGroup = LotGroups.FirstOrDefault(item => item.LotGroup_id == _id);
            return _LotGroup;
        }
        public LotGroup getLotGroupById(int _id, List<LotGroup> LotGroups)
        {
            LotGroup _LotGroup = LotGroups.FirstOrDefault(item => item.LotGroup_id == _id);
            return _LotGroup;
        }
        public LotGroup setLotGroup(LotGroup _LotGroup)
        {

            _dbContext.LotGroups.Add(_LotGroup);
            _dbContext.SaveChanges();
            return _LotGroup;
        }
        public LotGroup editLotGroup(LotGroup updLotGroup)
        {
            LotGroup extLotGroup = getLotGroupById(updLotGroup.LotGroup_id);

            extLotGroup = (LotGroup)UpdateValues.Update(extLotGroup, updLotGroup);
            _dbContext.SaveChanges();
            return extLotGroup;
        }
    }
}