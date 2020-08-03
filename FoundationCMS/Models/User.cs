using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string UserName { get; set; }
        [Required]
        public string Hash { get; set; }
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }

        [Column(TypeName = "char(5)")]
        public string Zip { get; set; }

        [Required]
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }

        [Required]
        public string Email { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsAdmin { get; set; } = false;

        [MaxLength(1000)]
        public string UserNotes { get; set; }




        public List<EventManager> EventManagers { get; set; }
    }
}
