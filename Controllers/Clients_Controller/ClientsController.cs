using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.DTOs.ClientDTO;
using Yumm_Back_End.Models.User;
using Yumm_Back_End.Repository._ClientRepository;

namespace Yumm_Back_End.Controllers.Clients_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IMapper _mapper;

        private readonly IClientRepository _clientRepo;

        public ClientsController(IMapper mapper, IClientRepository clientRepo)
        {
            _mapper = mapper;
            _clientRepo = clientRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientDTO clientDto)
        {
            if (clientDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var clientObj = _mapper.Map<Client>(clientDto);

            if (!await _clientRepo.CreateClient(clientObj))
            {
                ModelState.AddModelError("", $"An error occured while creating the restaurant {clientObj.FirstName + " " + clientObj.LastName}");

                return StatusCode(500, ModelState);
            }

            return Ok(clientObj);

        }

        [HttpGet]
        public IActionResult GetClients()
        {
            var clientList = _clientRepo.GetClients();

            var clientDTo = new List<UserDTO>();

            

            foreach (var client in clientList)
            {
                UserDTO UserDTo = new UserDTO
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    PhoneNumber = client.User.PhoneNumber
                    
                };

                clientDTo.Add(UserDTo);
            }

            return Ok(clientDTo);

        }


        [HttpGet("{clientId:int}", Name = "GetClient")]
        public IActionResult GetClient(int clientId)
        {
            var clientObj = _clientRepo.GetClient(clientId);


            if (clientObj == null)
            {
                return NotFound();
            }


            UserDTO UserDTo = new UserDTO
            {
                Id = clientObj.Id,
                FirstName = clientObj.FirstName,
                LastName = clientObj.LastName,
                Email = clientObj.User.Email,
                PhoneNumber = clientObj.User.PhoneNumber

            };

            //var clientDTO = _mapper.Map<ClientDTO>(clientObj);

            return Ok(UserDTo);

        }


        [HttpGet("{clientPhone}", Name = "GetClientsByPhone")]
        public IActionResult GetClientsByPhone(string clientPhone)
        {
            var clientObj = _clientRepo.GetClientByNumber(clientPhone);


            if (clientObj == null)
            {
                return NotFound();
            }


            UserDTO clientDTO = new UserDTO
            {
                Id = clientObj.Id,
                FirstName = clientObj.FirstName,
                LastName = clientObj.LastName,
                Email = clientObj.User.Email,
                PhoneNumber = clientObj.User.PhoneNumber

            };
           // var clientDTO = _mapper.Map<ClientDTO>(clientObj);

            return Ok(clientDTO);

        }

        [HttpDelete]
        public IActionResult DeleteClient(int clientId)
        {
            if (!_clientRepo.ClientExists(clientId))
            {
                return NotFound();
            }

            var clientObj = _clientRepo.GetClient(clientId);


            if (!_clientRepo.DeleteClient(clientObj))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting te record  {clientObj.FirstName + " " + clientObj.LastName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpPatch]
        public IActionResult UpdateClient([FromBody] ClientDTO clientDTO, int clientId)
        {
            if (clientDTO == null || clientDTO.Id == clientId)
            {
                return BadRequest(ModelState);
            }

            var clientObj = _mapper.Map<Client>(clientDTO);

            if (!_clientRepo.UpdateClient(clientObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the details {clientObj.FirstName}");

                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
