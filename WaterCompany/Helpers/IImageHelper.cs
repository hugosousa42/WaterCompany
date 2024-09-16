using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WaterCompany.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile ImageFile, string folder);
    }
}
