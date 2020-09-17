using FoundationCMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Services
{
    public interface IContributionService
    {
        void AddContribution(int eventId, int donorId, decimal amount, string paymentMethod); // Contribute through an event
        void AddContribution(int donorId, decimal amount, string paymentMethod);  // Contribute individually; outside of an event
        void RemoveContribution(int contributionId);
        decimal GetDonorContribution(int donorId);  // Get a donor contribution -- Return amount of contribution
        decimal GetEventContributions(int eventId); // Get an event total contribution -- Return the total (sum) of contributions
        int GetNumberOfDonations(int eventId); // Get number of submitted donation
        int GetDistNumOfDonations(int eventId); // Get distinct number of donations
        Contribution GetContribution(int contributionId);    // Get an amount of contribution using contribution Id
        bool IsDonated(int eventId, int donorId); // Temporarily
        List<Contribution> GetListOfDonations(int eventId); // Get a list of donors who donated at a specific event including the amount of the donation
        
        void SaveChanges();
    }

    public class ContributionService : IContributionService
    {
        private readonly AppDbContext _db;

        public ContributionService(AppDbContext db)
        {
            _db = db;
        }

        // Contribute through an event -- Invited donor in an event
        public void AddContribution(int eventId, int donorId, decimal amount, string paymentMethod)
        {
            var newContribution = new Contribution
            {
                ContributionDate = DateTime.Now,
                Amount = amount,
                PaymentMethod = paymentMethod,
                EventId = eventId,
                DonorId = donorId
            };

            _db.Contributions.Add(newContribution);
            _db.SaveChanges();
        }

        // Contribute outside of an event -- Individually
        public void AddContribution(int donorId, decimal amount, string paymentMethod)
        {
            var newContribution = new Contribution
            {
                ContributionDate = DateTime.Now,
                Amount = amount,
                PaymentMethod = paymentMethod,
                DonorId = donorId
            };

            _db.Contributions.Add(newContribution);
            _db.SaveChanges();
        }

        // Remove a donation
        public void RemoveContribution(int contributionId)
        {
            var donationObject = _db.Contributions.Where(c => c.Id == contributionId).SingleOrDefault();
            _db.Contributions.Remove(donationObject);
            _db.SaveChanges();
        }

        public decimal GetDonorContribution(int donorId)
        {
            return _db.Contributions.Where(e => e.DonorId == donorId).Select(e => e.Amount).SingleOrDefault();
        }

        public decimal GetEventContributions(int eventId)
        {
            return _db.Contributions.Where(e => e.EventId == eventId).Select(e => e.Amount).Sum();
        }

        public bool IsDonated(int eventId, int donorId)
        {
            var donor = _db.Contributions.Where(e => e.EventId == eventId).Where(d => d.DonorId == donorId).Count();

            if (donor == 0)
                return false;

            return true;
        }

        public int GetNumberOfDonations(int eventId)
        {
            return _db.Contributions.Where(e => e.EventId == eventId).Count();
        }

        public Contribution GetContribution(int contributionId)
        {
            return _db.Contributions.Where(c => c.Id == contributionId).SingleOrDefault();
        }

        public int GetDistNumOfDonations(int eventId)
        {
            return _db.Contributions.Where(e => e.EventId == eventId).Select(a => a.DonorId).Distinct().Count();
        }

        public List<Contribution> GetListOfDonations(int eventId)
        {
            return _db.Contributions.Where(c => c.EventId == eventId && c.Amount > 0).Include(c => c.Event).Include(c => c.Donor).ToList();
        }


        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
