using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FoundationCMS.Models;
using FoundationCMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoundationCMS.Controllers
{
    public class DonorsController : Controller
    {
        private readonly IDonorService _donorService;

        public DonorsController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Donor d)
        {
            _donorService.AddDonor(d);
            _donorService.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Import(IFormFile file)
        {
            string firstName;
            string lastName;
            string email;

            // Read all the lines from file
            using var reader = new StreamReader(file.OpenReadStream());
            List<string> lines = new List<string>();
            string line;

            while((line = reader.ReadLine()) != null)
            {
                string[] column = line.Split(",");
                firstName = column[0];
                lastName = column[1];
                email = column[2];

                var obj = (from d in _donorService.GetDonors()
                          where d.FirstName == firstName && d.LastName == lastName && d.Email1 == email
                          select d).FirstOrDefault();
                if (obj == null)    // If the donor DOES NOT exist in the database
                {
                    var newDonor = new Donor
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email1 = email,
                        MemberSince = DateTime.Now
                    };

                    _donorService.AddDonor(newDonor);
                    _donorService.SaveChanges();
                }
                else
                {
                    continue;
                }


            }

            return RedirectToAction("Index", "Home");
        }
    }
}
