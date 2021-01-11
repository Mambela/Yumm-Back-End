using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.LocationRepository
{
    public interface ILocationRepository
    {

        ICollection<Location> GetLocations();

        Location GetLocation(int locationId);


        bool LocationExists(Location location);

        bool LocationExists(int locationId);

        Task<bool> CreateLocation(Location location);
        ICollection<Location> GetLocationsInNeighbourhood(string hood);


        bool UpdateLocation(Location location);

        bool DeleteLocation(int locationId);

        bool Save();


    }
}
