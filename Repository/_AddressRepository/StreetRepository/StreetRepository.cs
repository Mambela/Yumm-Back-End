using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.StreetRepository
{
    public class StreetRepository : IStreetRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StreetRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> CreateStreet(Street street)
        {
            throw new NotImplementedException();
        }

        public bool DeleteStreet(Street street)
        {
            throw new NotImplementedException();
        }

        public Street GetStreet(int streetId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Street> GetStreets()
        {
            throw new NotImplementedException();
        }

        public ICollection<Street> GetStreetsInNeighbourhoods(int streetId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool StreetExists(string name)
        {
            throw new NotImplementedException();
        }

        public bool StreetExists(int streetId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStreet(Street street)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> CreateStreet(Street street)
        //{
        //    await _dbContext.Streets.AddAsync(street);

        //    return Save();

        //}

        //public bool DeleteStreet(Street street)
        //{
        //    _dbContext.Streets.Remove(street);

        //    return Save();
        //}

        //public ICollection<Street> GetStreets()
        //{
        //    return _dbContext.Streets.OrderBy(s => s.Name).ToList();

        //}

        //public ICollection<Street> GetStreetsInNeighbourhoods(int neighbourhoodId)
        //{

        //    return _dbContext.Streets.Include(s => s.Neighbourhoods).Where(n => n.NeighbourhoodId == neighbourhoodId).OrderBy(n => n.Name).ToList();

        //}

        //public Street GetStreet(int streetId)
        //{

        //    return _dbContext.Streets.FirstOrDefault(s => s.Id == streetId);

        //}

        //public bool UpdateStreet(Street street)
        //{

        //     _dbContext.Streets.Update(street);

        //    return Save();

        //}
        //public bool Save()
        //{
        //    return _dbContext.SaveChanges() >= 0;
        //}

        //public bool StreetExists(string name)
        //{

        //    return _dbContext.Streets.Any(s => s.Name.Trim().ToLower() == name.Trim().ToLower());
        //}

        //public bool StreetExists(int streetId)
        //{
        //    return _dbContext.Streets.Any(s => s.Id == streetId);
        //}


    }
}
