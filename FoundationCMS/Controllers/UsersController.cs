using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Profile(int id)
        {
            return View(_userService.GetUser(id));  // Display the user's profile
        }


    }
}
