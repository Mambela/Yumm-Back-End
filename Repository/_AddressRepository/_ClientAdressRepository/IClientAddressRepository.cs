using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository._ClientAdressRepository
{
    public interface IClientAddressRepository
    {

        Task<bool> CreateClientAddress(ClientAddress clientAddress);

        List<ClientAddress> GetAllClientAddresses(int clientId);

        ClientAddress GetClientAddress(int clientId);

        int NumberofAddresses(int clientId);


        bool UpdateClientAddress(ClientAddress clientAddress);  

        bool DeleteClientAddress(ClientAddress clientAddress);
        

        
        bool Save();
    }
}
