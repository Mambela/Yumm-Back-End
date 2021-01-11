using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models;
using Yumm_Back_End.Models.Authentication;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Repository._Auth;

namespace Yumm_Back_End.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller 
    {
        private readonly IAuthRepository _authRepo;
     

        public AuthenticationController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;

        }

        #region Restaurant Auth Methods
        [HttpPost]
        [Route("Restaurant/Register")]
        public async Task<IActionResult> RegisterRestaurant([FromBody] RegisterModelRestaurant model)
        {
            var register = await _authRepo.RegisterRestaurantAsync(model.Email, model.Password, model.PhoneNumber, model.Name, model.Description);

            return Ok(model);
        }


        [HttpPost]
        [Route("Restaurant/Login")]

        public async Task<IActionResult> ActionResult([FromBody] AuthModel model)
        {

            var authResult = await _authRepo.AuthenticateRestaurant(model.Password,model.UserName, null);

            return Ok(authResult);
        }
        #endregion

        #region  Client Auth Methods

        [HttpPost]
        [Route("Client/Register")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientModel model)
        {

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", $"Password and Password Confirmation do not match");

                return StatusCode(500, ModelState);
            }


            if (!ModelState.IsValid)
            {
                return StatusCode(500, ModelState);
            }

            var register = await _authRepo.RegisterClientAsync(model);

            return Ok(model);
        }


        [HttpPost]
        [Route("Client/Login")]
        public async Task<IActionResult> LoginClient([FromBody] AuthModel model)
        {

            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }



            var authResult = await _authRepo.AuthenticatedClient(model.Password, model.UserName);

            return Ok(authResult);
        }
        #endregion

    }
}
