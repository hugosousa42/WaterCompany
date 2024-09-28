using WaterCompany.Data.Entities;
using WaterCompany.Models;

namespace WaterCompany.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Client ToClient(ClientViewModel model, string path, bool IsNew)
        {
            return new Client
            {
                id = IsNew ? 0 : model.id,
                Name = model.Name,
                ImageUrl = path,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Birthdate = model.Birthdate,
                user = model.user
            };
        }

        public ClientViewModel ToClientViewModel(Client cliente)
        {
            return new ClientViewModel
            { 
                id = cliente.id,
                Name = cliente.Name,
                ImageUrl = cliente.ImageUrl,
                Email = cliente.Email,
                PhoneNumber = cliente.PhoneNumber,
                Birthdate = cliente.Birthdate,
                user = cliente.user
            };
        }
    }
}
