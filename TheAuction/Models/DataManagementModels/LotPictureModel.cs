//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using TheAuction.Models;
//using TheAuction.Infrastructure;
//using CodeFirst;

//namespace TheAuction.Models
//{
//    public class LotPictureModel
//    {
//        DbContext _dbContext;
//        public LotPictureModel(DbContext dbContext)
//        {
//            this._dbContext = dbContext;
//        }

//        public List<LotPicture> getLotPictures()
//        {
//            List<LotPicture> LotPictures = _dbContext.LotPictures.ToList();
//            return LotPictures;
//        }
//        public LotPicture getLotPictureById(int _id)
//        {
//            List<LotPicture> LotPictures = getLotPictures();
//            LotPicture _LotPicture = LotPictures.FirstOrDefault(item => item.LotPicture_id == _id);
//            return _LotPicture;
//        }
//        public LotPicture setLotPicture(LotPicture _LotPicture)
//        {

//            _dbContext.LotPictures.Add(_LotPicture);
//            _dbContext.SaveChanges();
//            return _LotPicture;
//        }
//    }
//}