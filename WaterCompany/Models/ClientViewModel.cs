using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WaterCompany.Data.Entities;

namespace WaterCompany.Models
{
    public class ClientViewModel : Client
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
