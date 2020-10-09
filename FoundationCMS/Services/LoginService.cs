using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoundationCMS.Services
{
    public class LoginService
    {
        private readonly IUserService _userService;

        public LoginService(IUserService userService)
        {
            _userService = userService;
        }

        public ClaimsIdentity Login(string username, string password)
        {
            var user = _userService.GetUser(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Hash))
            {
                return null;
            }

            // Otherwise, create a list of required claims
            // claim needs to be registered at Startup
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim("IsAdmin", user.IsAdmin.ToString())
            };
            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
