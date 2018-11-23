using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Microsoft.AspNet.Identity;
using CodeFirst;
using TheAuction.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace TheAuction.Infrastructure
{
    public class WinLot : IHttpModule
    {
        public MyDbContext _context = new MyDbContext();
        static Timer timer;
        long interval = 5000; 
        static object synclock = new object();
        public List<Lot> Lots
        {
            get
            {
                return _dManager.LotModel.getLots();
            }
        }
        public void Init(HttpApplication app)
        {
            timer = new Timer(new TimerCallback(Win), null, 0, interval);
        }
        private void Win(object obj)
        {
            lock (synclock)
            {
                Lot lot = _dManager.LotModel.getLotById(1);
                Bet bet = _dManager.BetModel.getBets().LastOrDefault(b => b.Lot == lot);
                Customer customer = _dManager.CustomerModel.getCustomers().FirstOrDefault(c => c == bet.Customer);
                Seller seller = _dManager.SellerModel.getSellers().FirstOrDefault(s => s == lot.Seller);
                List<Figure> figures = _dManager.FigureModel.getFigures();

                ShipmentOption shipOp = _dManager.ShipmentOptionModel.getShipmentOptionBySD(seller.Figure.Location, customer.Figure.Location);

                if (shipOp == null)
                {
                    shipOp = new ShipmentOption()
                    {
                        Cost = 500,
                        Source = lot.Seller.Figure.Location,
                        Destination = customer.Figure.Location
                    };
                    _dManager.ShipmentOptionModel.setShipmentOption(shipOp);
                }

                Check check = new Check()
                {
                    Cost = lot.Price + shipOp.Cost,
                    Lot = lot,
                    ShipmentOption = shipOp,
                    //Status = "Incomplete"
                };
                _dManager.CheckModel.setCheck(check);
            }
            
        }
        public void Dispose()
        { }
        public DataManager _dManager
        {
            get
            {
                return new DataManager(_context);
            }
        }
    }
    
}