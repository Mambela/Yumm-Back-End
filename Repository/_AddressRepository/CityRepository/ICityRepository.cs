using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.CityRepository
{
    public interface ICityRepository
    {
        ICollection<City> GetCities();

        City GetCity(int cityId);

        ICollection<City> GetCitiesInProvices(int provinceId);

        bool CityExists(string name);

        bool CityExists(int cityId);

        Task<bool> CreateCity(City city);

        bool UpdateCity(City city);

        bool DeleteCity(City city);

        bool Save();
    }
}
