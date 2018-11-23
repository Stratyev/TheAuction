using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheAuction.Models;
using TheAuction.Infrastructure;
using CodeFirst;

namespace TheAuction.Models
{
    public class ShipmentModel
    {
        MyDbContext _dbContext;
        public ShipmentModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Shipment> getShipments()
        {
            List<Shipment> Shipments = _dbContext.Shipments.ToList();
            return Shipments;
        }
        public Shipment getShipmentById(int _id)
        {
            List<Shipment> Shipments = getShipments();
            Shipment _Shipment = Shipments.FirstOrDefault(item => item.Shipment_id == _id);
            return _Shipment;
        }
        public Shipment getShipmentById(int _id, List<Shipment> Shipments)
        {
            Shipment _Shipment = Shipments.FirstOrDefault(item => item.Shipment_id == _id);
            return _Shipment;
        }
        public Shipment setShipment(Shipment _Shipment)
        {

            _dbContext.Shipments.Add(_Shipment);
            _dbContext.SaveChanges();
            return _Shipment;
        }
        public Shipment editShipment(Shipment updShipment)
        {
            Shipment extShipment = getShipmentById(updShipment.Shipment_id);

            extShipment = (Shipment)UpdateValues.Update(extShipment, updShipment);
            _dbContext.SaveChanges();
            return extShipment;
        }
    }
}