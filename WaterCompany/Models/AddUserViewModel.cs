using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WaterCompany.Models
{
    public class AddUserViewModel
    {
        [Display(Name = "Client")]
        [Required(ErrorMessage = "You must select a client.")]
        public string UserId { get; set; }

        [Range(0.0001, double.MaxValue, ErrorMessage = "The volume must be a positive number.")]
        public double Volume { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
