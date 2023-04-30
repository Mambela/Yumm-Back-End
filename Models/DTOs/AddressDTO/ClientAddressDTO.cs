using Yumm_Back_End.Models.Clients;

namespace Yumm_Back_End.Models.DTOs.AddressDTO
{
    public class ClientAddressDTO
    {
        public int Id { get; set; }
        public string Designation { get; set; }

        public int ClientId { get; set; }

        public int AddressId { get; set; }
    }
}
