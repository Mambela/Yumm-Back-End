using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.ProvinceRepository
{
    public class ProvinceRepository : IProvinceRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public ProvinceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public Task<bool> CreateProvince(Province province)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProvince(Province province)
        {
            throw new NotImplementedException();
        }

        public Province GetProvince(int provinceId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Province> GetProvinces()
        {
            throw new NotImplementedException();
        }

        public bool ProvinceExists(string name)
        {
            throw new NotImplementedException();
        }

        public bool ProvinceExists(int provinceId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateProvice(Province province)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> CreateProvince(Province province)
        //{
        //    await _dbContext.Provinces.AddAsync(province);

        //    if (Save())
        //        return true;


        //    return false;
        //}

        //public Province GetProvince(int provinceId)
        //{
        //    return  _dbContext.Provinces.FirstOrDefault(p => p.Id == provinceId);
        //}


        //public ICollection<Province> GetProvinces()
        //{
        //    return _dbContext.Provinces.OrderBy(p => p.Name).ToList();
        //}

        //public bool ProvinceExists(string name)
        //{
        //    bool value = _dbContext.Provinces.Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim());

        //    return value;
        //}


        //public bool DeleteProvince(Province province)
        //{
        //    _dbContext.Provinces.Remove(province);

        //    return Save();

        //}

        //public bool UpdateProvice(Province province)
        //{
        //    _dbContext.Provinces.Update(province);

        //    return Save();
        //}

        //public bool Save()
        //{
        //    return _dbContext.SaveChanges() >= 0;
        //}

        //public bool ProvinceExists(int provinceId)
        //{
        //    var value = _dbContext.Provinces.Any(p => p.Id == provinceId);

        //    return value;
        //}
    }
}
