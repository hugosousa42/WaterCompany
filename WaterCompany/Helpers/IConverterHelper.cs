using WaterCompany.Models;
using WaterCompany.Data.Entities;

namespace WaterCompany.Helpers
{
    public interface IConverterHelper
    {
        Client ToClient(ClientViewModel model, string path, bool IsNew);

        ClientViewModel ToClientViewModel (Client cliente);
    }
}
