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
        private readonly ApplicationDbContext _dbContext;

        public ClientRepository(ApplicationDbContext dbContex)
        {
            _dbContext = dbContex;
        }

        public bool ClientExists(int id)
        {
            return _dbContext.Clients.Any(c => c.Id == id);
        }

        public bool ClientExists(string Phone)
        {
            return _dbContext.Users.Any(u => u.PhoneNumber == Phone);
        }

        public async Task<bool> CreateClient(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
            return Save();
        }

        public bool DeleteClient(Client client)
        { 
            _dbContext.Clients.Remove(client);

           return Save();
        }

        public Client GetClient(int Id)
        {
            return _dbContext.Clients.Include(u => u.User).FirstOrDefault(c => c.Id == Id);
        }

        public Client GetClientByNumber(string phone)
        {

            var user = _dbContext.Users.FirstOrDefault(u => u.PhoneNumber == phone);

            var client=  _dbContext.Clients.Include(u=> u.User).FirstOrDefault(u => u.UserId == user.Id);

            return client;
        }

        public ICollection<Client> GetClients()
        {
            var clientList = _dbContext.Clients.Include(u => u.User).ToList();

            Console.WriteLine();

            foreach (var client in clientList)
            {
                Console.WriteLine(client);
            }

            return clientList;
                
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            var userList = _dbContext.Users.ToList();
            return userList;
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool UpdateClient(Client client)
        {

            _dbContext.Clients.Update(client);

            return Save();
            
        }
    }
}
