using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Address
{
    public class ClientAddress:BaseEntity
    {
        public int Id { get; set; }
        public string Designation { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }

        //Address Id Coming from Larks Service
        public int AddressId { get; set; }

    }
}
