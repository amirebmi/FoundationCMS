using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string EventName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime EventDate { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        [Required]
        public string Location { get; set; }



        public List<EventManager> EventManagers { get; set; }
        public List<EventDonor> Donors { get; set; }    
    }
}
