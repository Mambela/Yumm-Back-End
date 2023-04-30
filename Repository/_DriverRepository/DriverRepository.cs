using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Drivers;
using Yumm_Back_End.Models.DTOs.PaginationDTO;

namespace Yumm_Back_End.Repository._DriverRepository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ApplicationDbContext dbContext;
        public DriverRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public int TotalRecords()
        {
           return dbContext.Drivers.Count();
        }

        public async Task<bool> CreateDriver(Driver driver)
        {
           await   dbContext.Drivers.AddAsync(driver);

            return Save();
        }

        public Driver GetDriver(int id)
        {
            return dbContext.Drivers.Include(u =>u.User).FirstOrDefault(d => d.Id == id);   
        }

        public List<Driver> GetDriversList(PaginationFilter filter)
        {
            return dbContext.Drivers.Include(u => u.User)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToList();
        }

        public List<Driver> GetDriversList(PaginationFilter filter, string name)
        {
            return dbContext.Drivers
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Where(d => d.FirstName.Contains(name) || d.LastName.Contains(name))
                .ToList();
            throw new System.NotImplementedException();
        }

        public bool Save()
        {
            return dbContext.SaveChanges() >= 0;
        }

        public bool UpdateDriver(Driver driver)
        {
            //dbContext.Entry(driver).State = EntityState.Modified;
            dbContext.Drivers.Update(driver);

            return Save();
        }

        public bool SaveAsync()
        {
            return SaveAsync();
        }
    }
}
