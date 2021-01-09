using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Repository._MenuRepository
{
    public interface IItemRepository
    {
        ICollection<Item> GetItems();

        ICollection<Item> GetItemsInCategory(int catgoryId);

        Item GetItem(int itemId);

        bool ItemExists(int itemId);

        bool ItemExists(string name,int restaurantId,int categoryId);

        Task<bool> CreateItem(Item item);

        bool UpdateItem(Item item);

        bool DeleteItem(Item item);

        bool Save();

        Task<MediaResponse> SaveFoodImage(ItemDTORegister objectFile);

        
    }
}
