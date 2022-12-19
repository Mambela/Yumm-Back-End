using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Authentication;

namespace Yumm_Back_End.Repository._SupportTeamRepository
{
    public class SupportTeamRepository : ISupportTeamRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SupportTeamRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public SupportTeam GetSupportTeam(int SupportTeamId)
        {
            return _dbContext.SupportTeams.FirstOrDefault(s => s.Id == SupportTeamId);
        }

        public ICollection<SupportTeam> GetSupportTeams()
        {
            return _dbContext.SupportTeams.ToList();
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

            return _dbContext.SupportTeams.Any(s => s.Name == name);
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
