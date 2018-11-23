using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirst;
using System.Data.Entity;
using System.Threading;

namespace TheAuction.Models
{
    public class LotChecksAddition 
    {
        DataManager _dManager;
        //Timer timer;
        public LotChecksAddition(DataManager dManager/*, Timer timer*/)
        {
            _dManager = dManager;
            //this.timer = timer;
        }
        public void AddCheck()
        {
            List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                    .Include(l => l.Location).Include(l => l.LotGroup)
                    .Where(l => l.Condition.Name == "active" && l.Auction_end <= DateTime.Now).ToList();
            //foreach (Lot lot in lots)
            //{
            //    // To avoid processing a particular lot more than once
            //    lot.Condition = _dManager.ConditionModel.getConditionByName("processing");
            //    _dManager.LotModel.editLot(lot);
            //}
            foreach (Lot lot in lots)
            {
                Bet bet = _dManager._dbContext.Bets.Include(b => b.Customer).Include(b => b.Lot).ToList().LastOrDefault(b => b.Lot == lot);
                if (bet == null)
                {
                    lot.Condition = _dManager.ConditionModel.getConditionByName("unclaimed");
                    _dManager.LotModel.editLot(lot);
                }
                else
                {
                    Customer customer = _dManager._dbContext.Customers.Include(c => c.Figure).ToList().FirstOrDefault(c => c == bet.Customer);
                    Seller seller = _dManager._dbContext.Sellers.Include(s => s.Figure).ToList().FirstOrDefault(s => s == lot.Seller);
                    //List<Figure> figures = _dManager.FigureModel.getFigures();
                    //List<Location> locations = _dManager.LocationModel.getLocations();
                    //List<Category> categories = _dManager.CategoryModel.getCategories();
                    List<ShipmentOption> shipOps = _dManager._dbContext.ShipmentOptions.Include(shOp => shOp.Source).Include(shOp => shOp.Destination).ToList();
                    ShipmentOption shipOp = _dManager.ShipmentOptionModel.getShipmentOptionBySD(seller.Figure.Location, customer.Figure.Location, shipOps);

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
                    Condition conditionComplete = _dManager.ConditionModel.getConditionByName("complete");
                    Condition conditionShipmentExpected = _dManager.ConditionModel.getConditionByName("shipment_expected");
                    Check check = new Check()
                    {
                        Cost = lot.Price + shipOp.Cost,
                        Lot = lot,
                        Bet = bet,
                        ShipmentOption = shipOp,
                        Condition = conditionComplete,
                        ShipmentLastDate = DateTime.Now
                    };
                    _dManager.CheckModel.setCheck(check);

                    lot.Winner = customer;
                    lot.Condition = conditionShipmentExpected;
                    _dManager.LotModel.editLot(lot);
                }
            }
        }
    }
}