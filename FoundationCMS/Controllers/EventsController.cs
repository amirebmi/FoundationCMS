using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundationCMS.Models;
using FoundationCMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

namespace FoundationCMS.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IDonorService _donorService;

        public EventsController(IEventService eventService, IDonorService donorService)
        {
            _eventService = eventService;
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
        public IActionResult Add(Event e)
        {
            var newEvent = new Event
            {
                EventName = e.EventName,
                EventDate = e.EventDate,
                StartAt = e.StartAt,
                EndAt = e.EndAt,
                Location = e.Location
            };

            _eventService.AddEvent(newEvent);
            _eventService.SaveChanges();

           return RedirectToAction("Invite", "Events" , new { id = newEvent.Id});
        }

        [HttpGet]
        public IActionResult Invite(int id)
        {
            ViewBag.Donors = _donorService.GetDonors(); //Display list of donors for invitation
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public IActionResult Invite(int id, int[] selectedDonors)
        {
            foreach(var item in selectedDonors)
            {
                var newObj = new EventDonor
                {
                    EventId = id,
                    DonorId = item
                };
                _eventService.InviteDonor(newObj);
                _eventService.SaveChanges();
            }

            return View();
        }


    }
}
