using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Repository._ClientRepository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public ClientRepository(ApplicationDbContext dbcontex)
        {
            _dbcontext = dbcontex;
        }
 
 

        public bool ClientExists(int id)
        {
            return _dbcontext.Clients.Any(c => c.Id == id);
        }

        public bool ClientExists(string Phone)
        {
            return _dbcontext.Users.Any(u => u.PhoneNumber == Phone);
        }
 
        public async Task<bool> CreateClient(Client client)
        {
            await _dbcontext.Clients.AddAsync(client);
            return Save();
        }

       
        public bool DeleteClient(Client client)
        {
            _dbcontext.Clients.Remove(client);

            return Save();
        }
 

        public Client GetClient(int Id)
        {
            return _dbcontext.Clients.Include(u => u.User).FirstOrDefault(c => c.Id == Id);
        }
 
        public Client GetClientByNumber(string phone)
        {
            var user = _dbcontext.Users.FirstOrDefault(u => u.PhoneNumber == phone);

            var client = _dbcontext.Clients.Include(u => u.User).FirstOrDefault(u => u.UserId == user.Id);

            return client;
        }
 

        public ICollection<Client> GetClients()
        {
            var clientlist = _dbcontext.Clients.Include(u => u.User).ToList();
 

            return clientlist;
        }
 
        public ICollection<ApplicationUser> GetUsers()
        {
            var userlist = _dbcontext.Users.ToList();
            return userlist;
        }

        public bool Save()
        {
            return _dbcontext.SaveChanges() >= 0;
        }
 

        public bool UpdateClient(Client client)
        {
            _dbcontext.Clients.Update(client);

            return Save();
        }
    }
}
