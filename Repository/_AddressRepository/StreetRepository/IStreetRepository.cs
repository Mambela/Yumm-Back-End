using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.StreetRepository
{
    public interface IStreetRepository
    {
        ICollection<Street> GetStreets();

        Street GetStreet(int streetId);

        ICollection<Street> GetStreetsInNeighbourhoods(int streetId);

        bool StreetExists(string name);

        bool StreetExists(int streetId);

        Task<bool> CreateStreet(Street street);

        bool UpdateStreet(Street street);

        bool DeleteStreet(Street street);

        bool Save();
    }
}
