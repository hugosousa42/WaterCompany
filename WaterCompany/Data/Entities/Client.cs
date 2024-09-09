using System.ComponentModel.DataAnnotations;
using System;

namespace WaterCompany.Data.Entities
{
    public class Client
    {
        
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The filed {0} can contain {1} characters lenght.")]
        public string Name { get; set; }

        
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

   
        public string Address { get; set; }
        
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

    }
}
