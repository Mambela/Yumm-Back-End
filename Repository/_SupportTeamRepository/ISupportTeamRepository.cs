using System.Collections.Generic;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Authentication;

namespace Yumm_Back_End.Repository._SupportTeamRepository
{
    public interface ISupportTeamRepository
    {
        ICollection<SupportTeam> GetSupportTeams();

        ICollection<SupportTeam> GetSupportTeamsInType(int typeId);
        SupportTeam GetSupportTeam(int SupportTeamId);

        bool SupportTeamExist(string name);

        bool SupportTeamExist(int SupportTeamId);

        Task<bool> CreateSupportTeam(SupportTeam SupportTeam);

        bool UpdateResturant(SupportTeam SupportTeam);

        bool DeleteSupportTeam(SupportTeam SupportTeam);

        bool Save();
    }
}
