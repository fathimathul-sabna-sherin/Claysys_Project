using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudAsp.Models
{
    public class Signup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Second name")]
        public string LastName { get; set; }

        [Required]

        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

    }
}