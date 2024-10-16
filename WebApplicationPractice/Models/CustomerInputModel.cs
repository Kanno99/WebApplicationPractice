using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPractice.Models
{
    public class CustomerInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
