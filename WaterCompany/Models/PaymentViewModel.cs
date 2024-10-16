using System;
using System.ComponentModel.DataAnnotations;

namespace WaterCompany.Models
{
    public class PaymentViewModel
    {
        public int id { get; set; }

        [Display(Name = "Payment Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime PaymentDate { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
