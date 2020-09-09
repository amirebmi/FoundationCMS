using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundationCMS.Models;
using FoundationCMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoundationCMS.Controllers
{
    [ApiController]
    public class WebApiController : ControllerBase
    {
        private readonly IContributionService _contributionService;
        private readonly IEventService _eventService;
        private readonly IDonorService _donorService;

        public WebApiController(IContributionService contributionService, IEventService eventService, IDonorService donorService)
        {
            _contributionService = contributionService;
            _eventService = eventService;
            _donorService = donorService;
        }

        [HttpGet("api/contributions/isdonated/{eventId}/{donorId}")]
        public bool IsDonated(int eventId, int donorId)
        {
            return _contributionService.IsDonated(eventId, donorId);
        }

        [HttpGet("api/contributions/numberofdonation/{eventId}")]
        public int NumberOfDonations(int eventId)
        {
           return _contributionService.GetNumberOfDonations(eventId);

        }

        // This method is used in contribution page for JavaScript
        [HttpGet("api/contributions/numberofnotdonated/{eventId}")]
        public int NumOfNotDonated(int eventId)
        {
            var presentDonors = _eventService.GetPresentDonors(eventId);

            var distinctDonations = _contributionService.GetDistNumOfDonations(eventId);

            return presentDonors.Count - distinctDonations;
        }


    }
}
