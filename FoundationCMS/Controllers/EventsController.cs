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

        [Route("Events/Manage/Invite/{id}")]
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

        [Route("Events/Manage/Invite/{eventId}")]
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
            return RedirectToAction("Invite", "Events");
        }

        [Route("Events/Manage/Uninvite/{eventId}/{donorId}")]
        public IActionResult Uninvite(int eventId, int donorId)
        {
            _eventService.Uninvite(eventId, donorId);
            return RedirectToAction("Invite", "Events", new { id = eventId });
        }



        [Route("Events/Manage/{eventId}")]
        [Route("Events/Manage/RollCall/{eventId}")]
        [HttpGet]
        public IActionResult RollCall(int eventId)
        {
            var eventObject = _eventService.GetEvent(eventId);

            //bool onTime;
            //if ((DateTime.Now.ToString("d") == eventObject.EventDate.ToString("d")) &&
            //    (DateTime.Now.Hour == eventObject.StartAt.Hour) &&
            //    (DateTime.Now.Minute == eventObject.StartAt.Minute))
            //{
            //    onTime = true;
            //}
            //else
            //{
            //    onTime = false;
            //}

            ViewBag.StartRoll = true;

            var presentDonors = _eventService.GetPresentDonors(eventId);
            ViewBag.PresentDonors = presentDonors;  // Get a list of present donors

            var invitedDonors = (from d in _donorService.GetDonors()
                     join e in _eventService.GetEventDonors() on d.Id equals e.DonorId
                     where e.EventId == eventId
                     select d).ToList();
            ViewBag.InvitedDonors = (invitedDonors.Except(presentDonors)).ToList(); // Get list of invited donors except who are present

            return View(_eventService.GetEvent(eventId)); // Return event to get the event information on the Invite page
        }

        // Present a user at an event
        [Route("Events/Manage/RollCall/Present/{eventId}/{donorId}")]
        public IActionResult Present(int eventId, int donorId)
        {
            _eventService.Present(eventId, donorId);    // Present an invited donor

            return RedirectToAction("Manage", "Events", new { id = eventId });
        }

        // Absent a user at an event
        [Route("Events/Manage/RollCall/Absent/{eventId}/{donorId}")]
        public IActionResult Absent(int eventId, int donorId)
        {
            _eventService.Absent(eventId, donorId);    // Absent an invited donor

            return RedirectToAction("Manage", "Events", new { id = eventId });
        }

        // Display event details
        [Route("Events/Details/{eventId}")]
        [HttpGet]
        public IActionResult Details(int eventId)
        {
            // Number of invited guests
            ViewBag.InvitedNumber = (from e in _eventService.GetEventDonors()
                                     where e.EventId == eventId
                                     select e.DonorId).Count();

            // Number of present guests
            ViewBag.PresentNumber = (from e in _eventService.GetEventDonors()
                                     where e.EventId == eventId && e.IsPresent == true
                                     select e.IsPresent).Count();
            // Number of absent guests
            ViewBag.AbsentNumber = (from e in _eventService.GetEventDonors()
                                     where e.EventId == eventId && e.IsPresent == false
                                     select e.IsPresent).Count();


            return View(_eventService.GetEvent(eventId));
            
        }
    }
}
