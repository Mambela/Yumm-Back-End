using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Drivers;
using Yumm_Back_End.Models.DTOs.PaginationDTO;

namespace Yumm_Back_End.Repository._DriverRepository
{
    public interface IDriverRepository
    {
        int TotalRecords();

        Task<bool> CreateDriver(Driver driver);

        Driver GetDriver(int id);

        List<Driver> GetDriversList(PaginationFilter filter);

        List<Driver> GetDriversList(PaginationFilter filter, string name);

        bool UpdateDriver(Driver driver);

        bool Save();

        bool SaveAsync();
    }
}
