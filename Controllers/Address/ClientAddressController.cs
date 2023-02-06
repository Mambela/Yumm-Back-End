using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.DTOs.AddressDTO;
using Yumm_Back_End.Repository._AddressRepository._ClientAdressRepository;
using Yumm_Back_End.Services.Logger;

namespace Yumm_Back_End.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAddressController : ControllerBase
    {
        private readonly IClientAddressRepository clientAddressRepo;
        private readonly ILoggerService loggerService;
        private readonly IMapper mapper;
        public ClientAddressController(IClientAddressRepository _clientAddressRepository,ILoggerService _loggerService,IMapper _mapper)
        {
            clientAddressRepo = _clientAddressRepository;
            loggerService = _loggerService;
            mapper = _mapper;
        }

        [HttpPost]

        public async Task<IActionResult> CreateAddress(ClientAddressDTO newAddressDTO)
        {
            try
            {

                if (newAddressDTO == null)
                {
                    return BadRequest();
                }


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var numberOfAddresess = clientAddressRepo.NumberofAddresses(newAddressDTO.ClientId);

                if (numberOfAddresess > 1)
                {

                    ModelState.AddModelError("error", "Address limit reached!");
                    return BadRequest(ModelState);
                }

                var clientAddreObj = mapper.Map<ClientAddress>(newAddressDTO);



                if (!await clientAddressRepo.CreateClientAddress(clientAddreObj))
                {

                    ModelState.AddModelError("error", "error failed to crea new address for the client.");
                    return BadRequest(ModelState);
                }


                newAddressDTO.Id = clientAddreObj.Id;
                


                return StatusCode(StatusCodes.Status201Created,newAddressDTO);
            }
            catch (Exception ex)
            {
                loggerService.LogError("error create cliente address " + ex.Message +" "+ex.InnerException);
                ModelState.AddModelError("error", "error occurred while trying to create a client address. Please try again");
                return BadRequest(ModelState);
            }
        }


        [HttpGet]

        public IActionResult GetClientAddress(int addressId)
        {
            try
            {
                var clientAddressObj = clientAddressRepo.GetClientAddress(addressId);

                var clientAddressDTO = mapper.Map<ClientAddressDTO>(clientAddressObj);

                return Ok(clientAddressDTO);
            }
            catch (Exception ex)
            {
                loggerService.LogError("error to get cliente address " + ex.Message);
                ModelState.AddModelError("error", "error occurred while trying to retrieve a client address. Please try again");
                return BadRequest(ModelState);
            }
        }


        [HttpGet("ClientsAddress/{clientId:int}")]
        public IActionResult GetAllClientAddresses(int clientId)
        {
            try
            {

                var clientAddressObjList = clientAddressRepo.GetAllClientAddresses(clientId);

                

                var clientAddressDTOList = mapper.Map<List<ClientAddressDTO>>(clientAddressObjList);

                return Ok(clientAddressDTOList);
            }
            catch (Exception ex)
            {
                loggerService.LogError("error to get all client's address " + ex.Message);
                ModelState.AddModelError("error", "error occurred while trying to retrieve a client address. Please try again");
                return BadRequest(ModelState);
            }
        }

        [HttpPatch]
        public IActionResult PatchClientAddress(ClientAddressDTO clientAddress)
        {
            try
            {


                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.LogError("error to update client address " + ex.Message);
                ModelState.AddModelError("error", "error occurred while trying to update a client address. Please try again");
                return BadRequest(ModelState);
            }
        }


        [HttpPut]
        public IActionResult UpdateClientaddress(ClientAddressDTO clientAddressDTO, int clientAddressId)
        {
            try
            {

                if ( clientAddressDTO == null || clientAddressId != clientAddressDTO.Id)
                {
                    return BadRequest();
                }

                var clientAddress = mapper.Map<ClientAddress>(clientAddressDTO);

                if (!clientAddressRepo.UpdateClientAddress(clientAddress))
                {
                    ModelState.AddModelError("error", "failed to update client data no records changed!");
                    return BadRequest(ModelState);
                }


                return NoContent();
            }
            catch (Exception ex)
            {

                loggerService.LogError("error to get cliente address " + ex.Message);
                ModelState.AddModelError("error", "error occurred while trying to retrieve a client address. Please try again");
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public IActionResult DeleteClientAddress(int clientAddressId)
        {
            try
            {
                var clientAddress = clientAddressRepo.GetClientAddress(clientAddressId);


                if (!clientAddressRepo.DeleteClientAddress(clientAddress))
                {
                    ModelState.AddModelError("error", "failed to remove record client address!");
                    return BadRequest(ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.LogError("error to get cliente address " + ex.Message);
                ModelState.AddModelError("error", "error occurred while trying to remove a client address. Please try again");
                return BadRequest(ModelState);
            }
        }

    }
}
