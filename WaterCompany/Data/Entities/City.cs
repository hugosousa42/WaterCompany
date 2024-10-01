using System.ComponentModel.DataAnnotations;

namespace WaterCompany.Data.Entities
{
    public class City
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "City")]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters.")]
        public string Name { get; set; }
    }
}
