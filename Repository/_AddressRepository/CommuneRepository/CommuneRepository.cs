using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.CommuneRepository
{
    public class CommuneRepository : ICommuneRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public CommuneRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CommuneExists(string name)
        {
            throw new NotImplementedException();
        }

        public bool CommuneExists(int communeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCommune(Commune commune)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCommune(Commune commune)
        {
            throw new NotImplementedException();
        }

        public Commune GetCommune(int communeId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Commune> GetCommunes()
        {
            throw new NotImplementedException();
        }

        public ICollection<Commune> GetCommunesInCity(int communeId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateCommune(Commune commune)
        {
            throw new NotImplementedException();
        }
        //public bool CommuneExists(string name)
        //{
        //    var value = _dbContext.Communes.Any(c => c.Name == name);

        //    return value;
        //}

        //public bool CommuneExists(int communeId)
        //{

        //    var value = _dbContext.Communes.Any(c => c.Id == communeId);

        //    return value;
        //}

        //public async Task<bool> CreateCommune(Commune commune)
        //{
        //    await _dbContext.Communes.AddAsync(commune);

        //    return Save();
        //}

        //public bool DeleteCommune(Commune commune)
        //{
        //    _dbContext.Communes.Remove(commune);

        //    return Save();
        //}

        //public Commune GetCommune(int communeId)
        //{
        //    return _dbContext.Communes.FirstOrDefault(c => c.Id == communeId);
        //}

        //public ICollection<Commune> GetCommunes()
        //{
        //    return _dbContext.Communes.OrderBy(c => c.Name).ToList();
        //}

        //public ICollection<Commune> GetCommunesInCity(int cityId)
        //{

        //    return _dbContext.Communes.Include(c => c.City).Where(c => c.CityId == cityId).ToList();

        //}

        //public bool Save()
        //{
        //    return _dbContext.SaveChanges() >= 0;   
        //}

        //public bool UpdateCommune(Commune commune)
        //{

        //    _dbContext.Communes.Update(commune);

        //    return Save();

        //}
    }
}
