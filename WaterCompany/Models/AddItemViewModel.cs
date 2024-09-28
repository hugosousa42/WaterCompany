using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WaterCompany.Models
{
    public class AddItemViewModel
    {
        [Display(Name = "Client")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a client.")]
        public int ClienttId { get; set; }


        [Range(0.0001, double.MaxValue, ErrorMessage = "The volume must be a positive number.")]
        public double Volume { get; set; }


        public IEnumerable<SelectListItem> Clients { get; set; }
    }
}
