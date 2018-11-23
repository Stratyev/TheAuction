using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheAuction.Models;
using TheAuction.Infrastructure;
using CodeFirst;

namespace TheAuction.Models
{
    public class ShipmentOptionModel
    {
        MyDbContext _dbContext;
        public ShipmentOptionModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<ShipmentOption> getShipmentOptions()
        {
            List<ShipmentOption> shipmentOptions = _dbContext.ShipmentOptions.ToList();
            return shipmentOptions;
        }
        public ShipmentOption getShipmentOptionById(int _id)
        {
            List<ShipmentOption> shipmentOptions = getShipmentOptions();
            ShipmentOption _shipmentOption = shipmentOptions.FirstOrDefault(item => item.ShipmentOption_id == _id);
            return _shipmentOption;
        }
        public ShipmentOption getShipmentOptionById(int _id, List<ShipmentOption> shipmentOptions)
        {
            ShipmentOption _shipmentOption = shipmentOptions.FirstOrDefault(item => item.ShipmentOption_id == _id);
            return _shipmentOption;
        }
        public ShipmentOption setShipmentOption(ShipmentOption _shipmentOption)
        {

            _dbContext.ShipmentOptions.Add(_shipmentOption);
            _dbContext.SaveChanges();
            return _shipmentOption;
        }
        public ShipmentOption getShipmentOptionBySD(Location source, Location destination)
        {
            ShipmentOption shipOp = getShipmentOptions().FirstOrDefault(d => d.Source == source && d.Destination == destination);
            return shipOp;
        }
        public ShipmentOption getShipmentOptionBySD(Location source, Location destination, List<ShipmentOption> shipmentOption)
        {
            ShipmentOption shipOp = shipmentOption.FirstOrDefault(d => d.Source == source && d.Destination == destination);
            return shipOp;
        }
    }
}