using Yumm_Back_End.Models;

namespace Yumm_Back_End.Helper
{
    public class RoleIdentifier
    {


        public string ChooseRole(string role)
        {
 

            if(UserRoles.ADMIN.Equals(role))
                return UserRoles.ADMIN;


            if (UserRoles.RESTAURANTADMIN.Equals(role.ToUpper()))
                return UserRoles.RESTAURANTADMIN;


            if (UserRoles.RESTAURANTMANAGER.Equals(role.ToUpper()))
                return UserRoles.RESTAURANTMANAGER;

            if (UserRoles.RESTAURANTSTAFF.Equals(role.ToUpper()))
                return UserRoles.RESTAURANTSTAFF;

          

            if (UserRoles.CLIENT.Equals(role.ToUpper()))
                return UserRoles.CLIENT;

            if (UserRoles.DRIVER.Equals(role.ToUpper()))
                return UserRoles.DRIVER;

            if (UserRoles.SUPPORTTEAM.Equals(role.ToUpper()))
                return UserRoles.SUPPORTTEAM;

            if (UserRoles.SUPPORTTEAMMANAGER.Equals(role.ToUpper()))
                return UserRoles.SUPPORTTEAMMANAGER;


            return null;
        }
    }
}
