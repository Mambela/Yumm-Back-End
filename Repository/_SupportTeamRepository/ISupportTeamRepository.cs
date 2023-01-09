using System.Collections.Generic;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Authentication;
using Yumm_Back_End.Models.DTOs.PaginationDTO;

namespace Yumm_Back_End.Repository._SupportTeamRepository
{
    public interface ISupportTeamRepository
    {
        ICollection<SupportTeam> GetSupportTeams(PaginationFilter filter);

        List<SupportTeam> GetAllSupportTeamByName(PaginationFilter filter,string reference);

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
