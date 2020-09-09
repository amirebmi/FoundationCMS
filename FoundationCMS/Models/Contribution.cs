using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        public int EventId { get; set; }
        public Event Event { get; set; }


        
        //public Contribution(decimal amount)
        //{
        //    Amount = amount;
        //}
    }
}
