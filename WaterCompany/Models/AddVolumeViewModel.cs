using System.ComponentModel.DataAnnotations;

namespace WaterCompany.Models
{
    public class AddVolumeViewModel
    {
        public string UserId { get; set; }


        [Range(0.0001, double.MaxValue, ErrorMessage = "The volume must be a positive number.")]
        public double Volume { get; set; }
    }
}
