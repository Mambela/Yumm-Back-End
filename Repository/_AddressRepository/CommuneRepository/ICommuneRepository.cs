using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.CommuneRepository
{
    public interface ICommuneRepository
    {
        ICollection<Commune> GetCommunes();

        Commune GetCommune(int communeId);

        ICollection<Commune> GetCommunesInCity(int communeId);

        bool CommuneExists(string name);

        bool CommuneExists(int communeId);

        Task<bool> CreateCommune(Commune commune);

        bool UpdateCommune(Commune commune);

        bool DeleteCommune(Commune commune);

        bool Save();
    }
}
