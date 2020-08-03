using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Models
{
    public class Donor
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        
        public string LastName { get; set; }
        
        public string Gender { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        
        public string Email1 { get; set; }
        public string Email2 { get; set; }

        public string DonorNotes { get; set; }
        public bool IsActive { get; set; }


        public DateTime MemberSince { get; set; } = DateTime.Now;


        public List<Contribution> Contributions { get; set; }
    }
}
