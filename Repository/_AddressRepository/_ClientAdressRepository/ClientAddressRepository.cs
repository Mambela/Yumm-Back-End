using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository._ClientAdressRepository
{
    public class ClientAddressRepository : IClientAddressRepository
    {
        public readonly ApplicationDbContext dbContext;
        public ClientAddressRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<bool> CreateClientAddress(ClientAddress clientAddress)
        {
           await dbContext.ClientAddresses.AddAsync(clientAddress);

            return Save();
        }

        public bool DeleteClientAddress(ClientAddress clientAddress)
        {
            
            dbContext.ClientAddresses.Remove(clientAddress);
            
            return Save();
        }

        public List<ClientAddress> GetAllClientAddresses(int clientId)
        {
            return dbContext.ClientAddresses.Where(c =>c.ClientId == clientId).ToList();
        }

        public ClientAddress GetClientAddress(int clientId)
        {
            return dbContext.ClientAddresses.FirstOrDefault(c => c.Id == clientId);
        }

        public int NumberofAddresses(int clientId)
        {
            return dbContext.ClientAddresses.Where(c => c.ClientId == clientId).Count();
        }

        public bool Save()
        {
            return dbContext.SaveChanges() >= 0;
        }

        public bool UpdateClientAddress(ClientAddress clientAddress)
        {

            dbContext.ClientAddresses.Update(clientAddress);
            return Save();
        }
    }
}
