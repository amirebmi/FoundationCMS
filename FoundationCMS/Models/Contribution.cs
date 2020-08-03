using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public DateTime ContributionDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }


        public int DonorId { get; set; }
        public Donor Donor { get; set; }
    }
}
