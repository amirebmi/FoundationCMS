using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators;
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
        
        public IActionResult List()
        {
            return View(_eventService.GetEvents());
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
            var x = (from d in _donorService.GetDonors()
                              join e in _eventService.GetEventDonors() on d.Id equals e.DonorId
                              where d.Id == e.DonorId && id == e.EventId
                              select d).ToList();
            ViewBag.Donors = _donorService.GetDonors().Except(x);

            ViewBag.EventInfo = _eventService.GetEvent(id);     // Get the event information to be shown on invitation page


            ViewBag.InvitedDonor = (from d in _donorService.GetDonors()
                            join e in _eventService.GetEventDonors() on d.Id equals e.DonorId
                            where d.Id == e.DonorId && id == e.EventId
                            select d).ToList();






            return View(_eventService.GetEvent(id));    // Return event to get the event information on the Invite page
        }

        [HttpPost]
        public  IActionResult Invite(int eventId, List<int> checkedIdList)
        {


            foreach (var item in checkedIdList)
            {
                var newGuest = new EventDonor
                {
                    EventId = eventId,
                    DonorId = item
                };

                    _eventService.InviteDonor(newGuest);
                    _eventService.SaveChanges();
                
            }
            return RedirectToAction("Invite", "Events", new { id = eventId });
        }

        [Route("Events/Uninvite/{eventId}/{donorId}")]
        public IActionResult Uninvite(int eventId, int donorId)
        {

            _eventService.Uninvite(eventId, donorId);

            //var invitedDonor = _eventService.GetInvitedDonor(donorId, eventId);
            //EventDonor eventDonor = new EventDonor();
            //eventDonor.Donor= invitedDonor; 
            //_eventService.Uninvite(eventDonor);
            return RedirectToAction("Invite", "Events", new { id = eventId });

           // return $"DonorID: {donorId} and EventID: {eventId}";
        }


    }
}
