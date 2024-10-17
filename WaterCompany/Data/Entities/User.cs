using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WaterCompany.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(50, ErrorMessage = "The filed {0} can contain {1} characters lenght.")]
        public string FirstName { get; set; }


        [MaxLength(50, ErrorMessage = "The filed {0} can contain {1} characters lenght.")]
        public string LastName { get; set; }


        [MaxLength(100, ErrorMessage = "The filed {0} can contain {1} characters lenght.")]
        public string Address { get; set; }


        public int CityId { get; set; }


        public City City { get; set; }


        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Profile Image")]
        public string ImageUrl { get; set; }

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
