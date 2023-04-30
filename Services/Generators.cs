using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Services.Logger;

namespace Yumm_Back_End.Services
{
    public class Generators
    {
        public string GenerateUniqueNumber()
        {
            string unique = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss").Trim();
            unique = unique.Replace("/", "").Replace(":", "").Replace(" ", "");
            unique = unique + "_";

            return unique;
        }

        public string GenereateRestaurantReference(string RestaurantName)
        {
            LoggerService loggerService = new LoggerService();
            try
            {
                    int part1Ref = 0;
                    string part2Ref = null;

                Random random = new Random();
                part1Ref = random.Next(2000000, 9999999);
                part2Ref = DateTime.Now.Year  + DateTime.Now.Month.ToString();
                    //random.Next(1000, 9999);



                return part1Ref +$"{RestaurantName.Substring(0,2)}" +part2Ref;
             }
            catch (Exception ex)
            {
                loggerService.LogError("Error "+ ex.Message);
                throw;
            }
        }
    }
}
