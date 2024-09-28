using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WaterCompany.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
