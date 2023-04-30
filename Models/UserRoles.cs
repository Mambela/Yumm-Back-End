using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models
{
    public class UserRoles
    {

        public const string ADMIN  = "ADMIN";
        public const string DRIVER = "DRIVER";
        public const string CLIENT = "CLIENT";
        public const string RESTAURANT = "RESTAURANT";
        public const string SUPPORTTEAM = "SUPPORTTEAM";
        public const string SUPPORTTEAMMANAGER = "TEAMMANAGER";
        public const string RESTAURANTMANAGER = "RESTAURANTMANAGER";
        public const string RESTAURANTSTAFF = "RESTAURANTSTAFF";
        public const string RESTAURANTADMIN = "RESTAURANTADMIN";



    }

    public class Status
    {

        public const string NEW = "NEW";
        public const string ACCEPTED = "ACCEPTED";
        public const string REJECTED = "REJECTED";
        public const string COLLECTED = "COLLECTED";
        public const string DELIVERED = "DELIVERED";
        public const string AWAITINGPAYMENT = "AWAITING PAYMENT";
        public const string READY = "READY FOR COLLECTION";   
        public const string PAID = "PAID";

       public static string GetStatus(string status)
        {
            switch (status.ToUpper())
            {
                case Status.NEW: return NEW;
                case Status.READY: return READY;
                case Status.ACCEPTED: return ACCEPTED; 
                case Status.REJECTED: return REJECTED;
                case Status.COLLECTED: return COLLECTED;
                case Status.DELIVERED:return DELIVERED;
                case Status.PAID:return PAID;
                case Status.AWAITINGPAYMENT:return AWAITINGPAYMENT;
                default:
                    break;
            }

            return null;

        }

    }
}
