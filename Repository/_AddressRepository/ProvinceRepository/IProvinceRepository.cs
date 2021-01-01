using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.ProvinceRepository
{
    public interface IProvinceRepository
    {

        //Task<ICollection<Province>> GetProvinces();
        
        ICollection<Province> GetProvinces();

        Province GetProvince(int provinceId);

        bool ProvinceExists(string name);

        bool ProvinceExists(int provinceId);

        Task<bool> CreateProvince(Province province);

        bool UpdateProvice(Province province);

        bool DeleteProvince(Province province);

        bool Save();

    }
}
