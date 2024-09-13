﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WaterCompany.Data.Entities
{
    public class Client : IEntity
    {
        
        public int id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The filed {0} can contain {1} characters lenght.")]
        public string Name { get; set; }


        [Display(Name = "Image")]
        public string ImageUrl { get; set; }


        public string Email { get; set; }


        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

   
        public string Address { get; set; }
        
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        public User user { get; set; }

    }
}
