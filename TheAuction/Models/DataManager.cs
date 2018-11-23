using TheAuction.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using CodeFirst;
using System.Collections.Generic;
//using System.Data.Entity;

namespace TheAuction.Models
{
    public class DataManager
    {
        private IOwinContext _owinContext;
        private MyDbContext _context;
        public DataManager(IOwinContext context) //получаем овин-контекст
        {
            this._owinContext = context;
        }
        public DataManager(MyDbContext context) //или сразу дб-контекст
        {
            this._context = context;
        }

        public MyDbContext _dbContext //получаем дб-контекст из овин-контекста
        {
            get
            {
                if (_owinContext != null)
                {
                    return _owinContext.Get<MyDbContext>();
                }
                else
                {
                    return _context;
                }   
            }
        }

        //public void Inserts<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        //{
        //    // Отключаем отслеживание и проверку изменений для оптимизации вставки множества полей
        //    _dbContext.Configuration.AutoDetectChangesEnabled = false;
        //    _dbContext.Configuration.ValidateOnSaveEnabled = false;

        //    _dbContext.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));


        //    foreach (TEntity entity in entities)
        //        _dbContext.Entry(entity).State = EntityState.Added;
        //    _dbContext.SaveChanges();

        //    _dbContext.Configuration.AutoDetectChangesEnabled = true;
        //    _dbContext.Configuration.ValidateOnSaveEnabled = true;
        //}

        private FigureModel _FigureModel;
        public FigureModel FigureModel
        {
            get
            {
                if(_FigureModel == null)
                {
                    return _FigureModel = new FigureModel(_dbContext); 
                }
                else
                {
                    return _FigureModel;
                }
            }
        }

        private CustomerModel _CustomerModel;
        public CustomerModel CustomerModel
        {
            get
            {
                if (_CustomerModel == null)
                {
                    return _CustomerModel = new CustomerModel(_dbContext);
                }
                else
                {
                    return _CustomerModel;
                }
            }
        }

        private SellerModel _SellerModel;
        public SellerModel SellerModel
        {
            get
            {
                if (_SellerModel == null)
                {
                    return _SellerModel = new SellerModel(_dbContext);
                }
                else
                {
                    return _SellerModel;
                }
            }
        }

        private LotModel _LotModel;
        public LotModel LotModel
        {
            get
            {
                if (_LotModel == null)
                {
                    return _LotModel = new LotModel(_dbContext);
                }
                else
                {
                    return _LotModel;
                }
            }
        }

        private LotGroupModel _LotGroupModel;
        public LotGroupModel LotGroupModel
        {
            get
            {
                if (_LotGroupModel == null)
                {
                    return _LotGroupModel = new LotGroupModel(_dbContext);
                }
                else
                {
                    return _LotGroupModel;
                }
            }
        }

        private LocationModel _LocationModel;
        public LocationModel LocationModel
        {
            get
            {
                if (_LocationModel == null)
                {
                    return _LocationModel = new LocationModel(_dbContext);
                }
                else
                {
                    return _LocationModel;
                }
            }
        }

        private CategoryModel _CategoryModel;
        public CategoryModel CategoryModel
        {
            get
            {
                if (_CategoryModel == null)
                {
                    return _CategoryModel = new CategoryModel(_dbContext);
                }
                else
                {
                    return _CategoryModel;
                }
            }
        }

        private BetModel _BetModel;
        public BetModel BetModel
        {
            get
            {
                if (_BetModel == null)
                {
                    return _BetModel = new BetModel(_dbContext);
                }
                else
                {
                    return _BetModel;
                }
            }
        }

        private ShipmentOptionModel _ShipmentOptionModel;
        public ShipmentOptionModel ShipmentOptionModel
        {
            get
            {
                if (_ShipmentOptionModel == null)
                {
                    return _ShipmentOptionModel = new ShipmentOptionModel(_dbContext);
                }
                else
                {
                    return _ShipmentOptionModel;
                }
            }
        }

        private ShipmentModel _ShipmentModel;
        public ShipmentModel ShipmentModel
        {
            get
            {
                if (_ShipmentModel == null)
                {
                    return _ShipmentModel = new ShipmentModel(_dbContext);
                }
                else
                {
                    return _ShipmentModel;
                }
            }
        }

        private CheckModel _CheckModel;
        public CheckModel CheckModel
        {
            get
            {
                if (_CheckModel == null)
                {
                    return _CheckModel = new CheckModel(_dbContext);
                }
                else
                {
                    return _CheckModel;
                }
            }
        }

        private ConditionModel _ConditionModel;
        public ConditionModel ConditionModel
        {
            get
            {
                if (_ConditionModel == null)
                {
                    return _ConditionModel = new ConditionModel(_dbContext);
                }
                else
                {
                    return _ConditionModel;
                }
            }
        }

        //private LotPictureModel _LotPictureModel;
        //public LotPictureModel LotPictureModel
        //{
        //    get
        //    {
        //        if (_LotPictureModel == null)
        //        {
        //            return _LotPictureModel = new LotPictureModel(_dbContext);
        //        }
        //        else
        //        {
        //            return _LotPictureModel;
        //        }
        //    }
        //}
    }
}