using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheAuction.Models;
using TheAuction.Infrastructure;
using CodeFirst;


namespace TheAuction.Models
{
    public class LocationModel
    {
        MyDbContext _dbContext;
        public LocationModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Location> getLocations()
        {
            List<Location> Locations = _dbContext.Locations.ToList();
            return Locations;
        }
        public Location getLocationById(int _id)
        {
            List<Location> Locations = getLocations();
            Location _Location = Locations.FirstOrDefault(item => item.Location_id == _id);
            return _Location;
        }
        public Location getLocationByName(string _name)
        {
            List<Location> Locations = getLocations();
            Location _Location = Locations.FirstOrDefault(item => item.Name == _name);
            return _Location;
        }
        public Location getLocationById(int _id, List<Location> Locations)
        {
            Location _Location = Locations.FirstOrDefault(item => item.Location_id == _id);
            return _Location;
        }
        public Location getLocationByName(string _name, List<Location> Locations)
        {
            Location _Location = Locations.FirstOrDefault(item => item.Name == _name);
            return _Location;
        }
        public Location setLocation(Location _Location)
        {

            _dbContext.Locations.Add(_Location);
            _dbContext.SaveChanges();
            return _Location;
        }
        public Location editLocation(Location updLocation)
        {
            Location extLocation = getLocationById(updLocation.Location_id);

            extLocation = (Location)UpdateValues.Update(extLocation, updLocation);
            _dbContext.SaveChanges();
            return extLocation;
        }
    }
}