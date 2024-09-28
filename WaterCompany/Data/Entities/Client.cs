using System;
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
        
       
        public DateTime Birthdate { get; set; }

        public User user { get; set; }

        public string ImageFullPath
        {

            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                {
                    return null;
                }

                return $"https://localhost:44382{ImageUrl.Substring(1)}";
            }

        }

    }
}
