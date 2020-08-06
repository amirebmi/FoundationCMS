using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Models
{
    public class EventDonor
    {
        public int Id { get; set; }
        public DateTime InvitationDate { get; set; } = DateTime.Now;
        
        public bool IsPresent { get; set; }
        

        public int EventId { get; set; }
        public Event Event { get; set; }


        public int DonorId { get; set; }
        public Donor Donor { get; set; }

        public bool Selected { get; set; }


        [NotMapped] // Recently added 08/04/2020
        public List<EventDonor> CheckBoxItems { get; set; }

    }
}
