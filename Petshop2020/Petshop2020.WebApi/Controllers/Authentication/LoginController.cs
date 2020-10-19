using Microsoft.AspNetCore.Mvc;
using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity.Authentication;
using Petshop2020.Infrastructure.SQLite.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop2020.WebApi.Controllers.Authentication
{
    [Route("/login")]
    public class LoginController : Controller
    {
        private IUserRepository userRepository;
        private IAuthenticationHelper authenticationHelper;

        public LoginController(IUserRepository repos, IAuthenticationHelper authHelper)
        {
            userRepository = repos;
            authenticationHelper = authHelper;
        }


        [HttpPost]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            var user = userRepository.GetAllUsers().FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!authenticationHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = authenticationHelper.GenerateToken(user)
            });
        }
    }
}
