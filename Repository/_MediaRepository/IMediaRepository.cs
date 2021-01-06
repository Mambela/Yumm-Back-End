using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;

namespace Yumm_Back_End.Repository._MediaRepository
{
    public interface IMediaRepository
    {
         Task<bool> SaveFoodImage(ItemDTORegister objectFile);
    }
}
