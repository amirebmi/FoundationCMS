using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundationCMS.Models;
using FoundationCMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoundationCMS.Controllers
{
    public class ContributionsController : Controller
    {
        private readonly IContributionService _contributionService;
        private readonly IEventService _eventService;
        private readonly IDonorService _donorService;

        public ContributionsController(IContributionService contributionService, IEventService eventService, IDonorService donorService)
        {
            _contributionService = contributionService;
            _eventService = eventService;
            _donorService = donorService;
        }

        [Route("Contributions/View/{eventId}")]
        public IActionResult View(int eventId)   // Add a contribution
        {
            ViewBag.PresentDonors = _eventService.GetPresentDonors(eventId);    // List of present donors -- used in contributions page


            return View(_eventService.GetEvent(eventId));
        }


        public void Add(Contribution c)
        {
            _contributionService.AddContribution(c.EventId, c.DonorId, c.Amount, c.PaymentMethod);
        }

        // For contributions detailed report
        [Route("Contributions/Details/{eventId}")]
        public IActionResult Details(int eventId)
        {
            var contributionObject = _contributionService.GetListOfDonations(eventId);

            var dict = new Dictionary<Donor, List<Contribution>>();

            foreach(var item in contributionObject)
            {
                List<Contribution> contributions;
                if (dict.TryGetValue(item.Donor, out contributions))
                {
                    var newContribution = new Contribution
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        ContributionDate = item.ContributionDate,
                        PaymentMethod = item.PaymentMethod,
                        EventId = item.EventId
                    };
                    contributions.Add(newContribution);
                }
                else
                {
                    contributions = new List<Contribution>();
                    var newContribution = new Contribution
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        ContributionDate = item.ContributionDate,
                        PaymentMethod = item.PaymentMethod,
                        EventId = item.EventId
                    };

                    contributions.Add(newContribution);


                    dict.Add(item.Donor, contributions);
                }
            }
            ViewBag.Dict = dict;
            return View();
        }

        [Route("Contributions/Remove/{eventId}/{contributionId}")]
        public IActionResult Remove(int eventId, int contributionId)
        {

            _contributionService.RemoveContribution(contributionId);
            return RedirectToAction("Details", "Contributions", new { id = eventId });
        }


        [HttpGet]
        [Route("Contributions/Edit/{eventId}/{contributionId}/{donorId}")]
        public IActionResult Edit(int eventId, int contributionId, int donorId)
        {
            ViewBag.Donor = _donorService.GetDonor(donorId);

            ViewBag.Event = _eventService.GetEvent(eventId);

            ViewBag.Contribution = _contributionService.GetContribution(contributionId);

            return View();
        }

        [Route("Contributions/Edit")]
        [HttpPost]
        public IActionResult Edit(Contribution c)
        {

            var contribution = _contributionService.GetContribution(c.Id);

            contribution.Amount = c.Amount;
            contribution.ContributionDate = DateTime.Now;
            contribution.PaymentMethod = c.PaymentMethod;

            _contributionService.SaveChanges();

            return RedirectToAction("Details", "Contributions", new { id = contribution.EventId });
        }


        // Use it for html to pdf convertion
        public IActionResult ReportPrint(int eventId)
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }



    }
}
