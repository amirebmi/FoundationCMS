using FoundationCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Services
{
    public interface IDonorService
    {
        Donor GetDonor(int id);
        List<Donor> GetDonors();
        decimal GetDonorContibutions(Donor donor);  // Get all contributions for a specific donor
        void AddDonor(Donor donor);
        void RemoveDonor(Donor donor);

        void SaveChanges();
    }

    public class DonorService : IDonorService
    {
        private readonly AppDbContext _db;

        public DonorService(AppDbContext db)
        {
            _db = db;
        }

        public Donor GetDonor(int donorId)
        {
            return _db.Donors.Find(donorId);
        }

        public List<Donor> GetDonors()
        {
            return _db.Donors.ToList();
        }

        public decimal GetDonorContibutions(Donor donor)
        {
            var result = (from c1 in _db.Contributions
                          join c2 in _db.Donors on c1.DonorId equals c2.Id
                          where c2 == donor
                          select c1.Amount).Sum();
            return result;
        }

        public void AddDonor(Donor donor)
        {
            _db.Donors.Add(donor);
            _db.SaveChanges();
        }

        public void RemoveDonor(Donor donor)
        {
            _db.Donors.Remove(donor);
            _db.SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
