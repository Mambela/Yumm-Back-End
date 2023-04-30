using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.LocationRepository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public LocationRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<bool> CreateLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLocation(int locationId)
        {
            throw new NotImplementedException();
        }

        public Location GetLocation(int locationId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Location> GetLocations()
        {
            throw new NotImplementedException();
        }

        public ICollection<Location> GetLocationsInNeighbourhood(int hoodId)
        {
            throw new NotImplementedException();
        }

        public bool LocationExists(Location location)
        {
            throw new NotImplementedException();
        }

        public bool LocationExists(int locationId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public Location UserCheckLocationExist(Location location)
        {
            throw new NotImplementedException();
        }

        public bool UserCheckLocationExist(int locationId)
        {
            throw new NotImplementedException();
        }
        //public async Task<bool> CreateLocation(Location location)
        //{

        //    await _dbContext.Locations.AddAsync(location);

        //    return Save();

        //}

        //public bool DeleteLocation(int locationId)
        //{
        //    var location = GetLocation(locationId);

        //    _dbContext.Locations.Remove(location);

        //    return Save();
        //}

        //public Location GetLocation(int locationId)
        //{
        //    return _dbContext.Locations.Include(s => s.Street)
        //           .FirstOrDefault(l => l.Id == locationId);
        //}

        //public ICollection<Location> GetLocations()
        //{
        //    return _dbContext.Locations.Include(s => s.Street)
        //        .ToList();
        //}

        //public ICollection<Location> GetLocationsInNeighbourhood(int hoodId)
        //{

        //    return _dbContext.Locations.Include(n => n.Street.Neighbourhoods).Where(n => n.Street.NeighbourhoodId == hoodId)
        //        .Include(s => s.Street)
        //        .ToList();
        //}

        //public bool LocationExists(Location location)
        //{
        //    return _dbContext.Locations.Any(l => l.Lat == location.Lat && l.Lng == location.Lng && l.StreetId == location.StreetId);
        //}

        //public Location UserCheckLocationExist(Location location)
        //{
        //    return _dbContext.Locations.FirstOrDefault(l => l.Lat == location.Lat && l.Lng == l.Lng);
        //}

        //public bool LocationExists(int locationId)
        //{
        //    return _dbContext.Locations.Any(l => l.Id == locationId);
        //}


        //public bool Save()
        //{
        //    return _dbContext.SaveChanges() >= 0;
        //}

        //public bool UpdateLocation(Location location)
        //{
        //    _dbContext.Locations.Update(location);

        //    return Save();
        //}

        //public bool UserCheckLocationExist(int locationId)
        //{
        //    return _dbContext.Locations.Any(l => l.Id == locationId);
        //}
    }
}
