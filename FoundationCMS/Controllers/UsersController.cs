using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundationCMS.Models;
using FoundationCMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoundationCMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        // Signup a user
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            // Hash the password using BCrypt algorithm
            var password = BCrypt.Net.BCrypt.HashPassword(user.Hash);
            var newUser = new User
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Hash = password,
                Title = user.Title,
                Gender = "male",
                Street = user.Street,
                City = user.City,
                Zip = user.Zip,
                CellPhone = user.CellPhone,
                HomePhone = user.HomePhone,
                Email = user.Email
            };


            _userService.AddUser(newUser);


            return View();
        }

        public IActionResult Profile(int id)
        {
            return View();  // Display the user's profile
        }


    }
}
