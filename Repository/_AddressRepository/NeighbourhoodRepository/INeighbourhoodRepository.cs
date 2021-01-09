using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.NeighbourhoodRepository
{
    public interface INeighbourhoodRepository
    {
        ICollection<Neighbourhood> GetNeighbourhoods();

        Neighbourhood GetNeighbourhood(int neighbourhoodId);

        ICollection<Neighbourhood> GetNeighbourhoodsInCommune(int communeId);

        bool NeighbourhoodExists(string name);

        bool NeighbourhoodExists(int neighbourhoodId);

        Task<bool> CreateNeighbourhood(Neighbourhood neighbourhood);

        bool UpdateNeighbourhood(Neighbourhood neighbourhood);

        bool DeleteNeighbourhood(Neighbourhood neighbourhood);

        bool Save();
    }
}
