using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WaterCompany.Data.Entities
{
    public class Bill : IEntity
    {
        public int id { get; set; }


        [Required]
        [Display(Name = "Date of issue")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime DateOfIssue { get; set; }

        [Display(Name = "Payment Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? PaymentDate { get; set; }

        [Required]
        public User User { get; set; }


        public IEnumerable<BillDetail> Items { get; set; }


        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines => Items == null ? 0 : Items.Count();


        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Volume => Items == null ? 0 : Items.Sum(i => i.Volume);


        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Value => Items == null ? 0 : Items.Sum(i => i.Value);


        [Display(Name = "Date of issue")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? DateOfIssueLocal => this.DateOfIssue == null ? null : this.DateOfIssue.ToLocalTime();
    }
}
