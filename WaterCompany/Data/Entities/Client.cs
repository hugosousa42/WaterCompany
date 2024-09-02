using System.ComponentModel.DataAnnotations;
using System;

namespace WaterCompany.Data.Entities
{
    public class Client
    {
        
        public int Id { get; set; }


        public string Name { get; set; }

        
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

   
        public string Address { get; set; }

        public DateTime RegistrationDate { get; set; }

    }
}
