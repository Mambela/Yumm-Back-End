using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.NeighbourhoodRepository
{
    public class NeighbourhoodRepository : INeighbourhoodRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NeighbourhoodRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateNeighbourhood(Neighbourhood neighbourhood)
        {
            await _dbContext.AddAsync(neighbourhood);

            return Save();
        }
        public bool UpdateNeighbourhood(Neighbourhood neighbourhood)
        {

            _dbContext.Neighbourhoods.Update(neighbourhood);

            return Save();
        }
        public bool DeleteNeighbourhood(Neighbourhood neighbourhood)
        {
            _dbContext.Neighbourhoods.Remove(neighbourhood);

            return Save();
        }

        public Neighbourhood GetNeighbourhood(int neighbourhoodId)
        {

            return _dbContext.Neighbourhoods.FirstOrDefault(n => n.Id == neighbourhoodId);
        }

        public ICollection<Neighbourhood> GetNeighbourhoods()
        {
            return _dbContext.Neighbourhoods.OrderBy(n => n.Name).ToList();
        }

        public ICollection<Neighbourhood> GetNeighbourhoodsInCommune(int communeId)
        {
            return _dbContext.Neighbourhoods.Include(n => n.Commune).Where(n => n.CommuneId == communeId).ToList();   
        }

        public bool NeighbourhoodExists(string name)
        {
            var value = _dbContext.Neighbourhoods.Any(n => n.Name == name);

            return value;
        }

        public bool NeighbourhoodExists(int neighbourhoodId)
        {

            var value = _dbContext.Neighbourhoods.Any(n => n.Id == neighbourhoodId);

            return value;
            
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

       
    }
}
