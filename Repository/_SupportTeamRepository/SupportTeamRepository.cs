using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Authentication;
using Yumm_Back_End.Models.DTOs.PaginationDTO;

namespace Yumm_Back_End.Repository._SupportTeamRepository
{
    public class SupportTeamRepository : ISupportTeamRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SupportTeamRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Count()
        {
            return _dbContext.SupportTeams.Count();
        }

        public async Task<bool> CreateSupportTeam(SupportTeam SupportTeam)
        {
             await _dbContext.SupportTeams.AddAsync(SupportTeam);

            return Save();
        }

        public bool DeleteSupportTeam(SupportTeam SupportTeam)
        {
            throw new System.NotImplementedException();
        }

        public List<SupportTeam> GetAllSupportTeamByName(PaginationFilter filter, string reference)
        {
            return _dbContext.SupportTeams
                .Include(u => u.User)
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .Where(s =>s.Name.Contains(reference) || s.LastName.Contains(reference))
               .ToList();
        }

        public SupportTeam GetSupportTeam(int SupportTeamId)
        {
            return _dbContext.SupportTeams
                .Include(u => u.User).FirstOrDefault(s => s.Id == SupportTeamId);
        }

        public ICollection<SupportTeam> GetSupportTeams(PaginationFilter filter)
        {
            return _dbContext.SupportTeams
                .Include(u => u.User)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToList();
           
        }

        public ICollection<SupportTeam> GetSupportTeamsInType(int typeId)
        {
            throw new System.NotImplementedException();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool SupportTeamExist(string name)
        {

            return _dbContext.SupportTeams.Any(s => s.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool SupportTeamExist(int SupportTeamId)
        {
            return _dbContext.SupportTeams.Any(s => s.Id == SupportTeamId);
        }

        public bool UpdateResturant(SupportTeam SupportTeam)
        {
            _dbContext.SupportTeams.Update(SupportTeam);

            return Save();
        }
    }
}
