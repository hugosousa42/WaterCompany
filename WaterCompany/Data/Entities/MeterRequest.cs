using System.ComponentModel.DataAnnotations;

namespace WaterCompany.Data.Entities
{
    public class MeterRequest : IEntity
    {
        public int id { get; set; }


        [MaxLength(50, ErrorMessage = "The filed {0} can contain {1} characters lenght.")]
        public string FirstName { get; set; }


        [MaxLength(50, ErrorMessage = "The filed {0} can contain {1} characters lenght.")]
        public string LastName { get; set; }


        [MaxLength(100, ErrorMessage = "The filed {0} can contain {1} characters lenght.")]
        public string Address { get; set; }

        
        public string Email { get; set; }


        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        public int CityId { get; set; }


        public City City { get; set; }


        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";


        public bool IsProcessed { get; set; } = false;

        public bool IsDelivered { get; set; } = false;
    }
}
