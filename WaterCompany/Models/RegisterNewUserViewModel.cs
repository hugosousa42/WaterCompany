using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WaterCompany.Models
{
    public class RegisterNewUserViewModel
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        public IEnumerable<SelectListItem> Roles { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "You must select a Role.")]
        public string RoleId { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} can contain {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(100, ErrorMessage = "The field {0} can contain {1} characters.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "City")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a city.")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country.")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}
