using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Repository._ClientRepository
{
    public interface IClientRepository
    {

        ICollection<ApplicationUser> GetUsers();
        ICollection<Client> GetClients();

        Client GetClientByNumber(string phone);

        Client GetClient(int Id);

        Task<bool> CreateClient(Client client);

        bool UpdateClient(Client client);

        bool DeleteClient(Client client);

        bool ClientExists(int id);

        bool ClientExists(string Phone);

        bool Save();

    }
}
