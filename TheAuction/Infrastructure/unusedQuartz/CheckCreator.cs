using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using TheAuction.Models;
using CodeFirst;


namespace TheAuction.Infrastructure.Quartz
{
    public class CheckCreator : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            Object dManager = dataMap.Get("_dManager");
            Object oLot = dataMap.Get("lot");
            DataManager _dManager = (DataManager)dManager;
            Lot lot = (Lot)oLot;

            Bet bet = _dManager.BetModel.getBets().LastOrDefault(b => b.Lot == lot);
            if (bet != null)
            {
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
        
    }
}