using AutoMapper;
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
        public async Task<bool> CreateLocation(Location location)
        {

            await _dbContext.Locations.AddAsync(location);

            return Save();

        }

        public bool DeleteLocation(int locationId)
        {
            var location = GetLocation(locationId);

            _dbContext.Locations.Remove(location);

            return Save();
        }

        public Location GetLocation(int locationId)
        {
            return _dbContext.Locations.FirstOrDefault(l => l.Id == locationId);
        }

        public ICollection<Location> GetLocations()
        {
            return _dbContext.Locations.ToList();
        }



        public ICollection<Location> GetLocationsInNeighbourhood(string hood)
        {
            return _dbContext.Locations.Where(l => l.Neighbourhood.Trim().ToLower() == hood.Trim().ToLower()).ToList();

        }

        public bool LocationExists(Location location)
        {

            //Check the Hood
            return _dbContext.Locations.Any(
                l => l.Neighbourhood.Trim().ToLower() == location.Neighbourhood.Trim().ToLower() &&
                l.StreetName.Trim().ToLower() == location.StreetName.Trim().ToLower() &&
                (l.Lat == location.Lat && l.Lng == location.Lng));

        }

        public bool LocationExists(int locationId)
        {
            return _dbContext.Locations.Any(l => l.Id == locationId);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool UpdateLocation(Location location)
        {
            _dbContext.Locations.Update(location);

            return Save();
        }

    }
}
