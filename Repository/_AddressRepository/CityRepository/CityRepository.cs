using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.CityRepository
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CityRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CityExists(string name)
        {
            throw new NotImplementedException();
        }

        public bool CityExists(int cityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCity(City city)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCity(City city)
        {
            throw new NotImplementedException();
        }

        public ICollection<City> GetCities()
        {
            throw new NotImplementedException();
        }

        public ICollection<City> GetCitiesInProvices(int provinceId)
        {
            throw new NotImplementedException();
        }

        public City GetCity(int cityId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateCity(City city)
        {
            throw new NotImplementedException();
        }

        //public bool CityExists(string name)
        //{
        //    bool value = _dbContext.Cities.Any(c => c.Name.Trim().ToLower() == name.ToLower().Trim());

        //    return value;
        //}

        //public bool CityExists(int cityId)
        //{
        //    bool value = _dbContext.Cities.Any(c => c.Id == cityId);

        //    return value;
        //}

        //public async Task<bool> CreateCity(City city)
        //{
        //    await _dbContext.AddAsync(city);

        //    return Save();
        //}

        //public bool DeleteCity(City city)
        //{
        //    _dbContext.Cities.Remove(city);

        //    return Save();
        //}

        //public ICollection<City> GetCities()
        //{
        //    return _dbContext.Cities.OrderBy(a => a.Name).ToList();
        //}

        //public ICollection<City> GetCitiesInProvices(int provinceId)
        //{

        //    return _dbContext.Cities.Include(c => c.Province).Where(c => c.ProviceId == provinceId).ToList();

        //}

        //public City GetCity(int cityId)
        //{
        //    return _dbContext.Cities.FirstOrDefault(c => c.Id == cityId);

        //}

        //public bool Save()
        //{
        //    return _dbContext.SaveChanges() >= 0;
        //}

        //public bool UpdateCity(City city)
        //{
        //    _dbContext.Cities.Update(city);

        //    return Save();
        //}
    }
}
