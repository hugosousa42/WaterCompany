using System.Collections.Generic;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;

namespace WaterCompany.Data
{
    public interface IRepository
    {
        void AddClient(Client client);
        bool ClientExists(int id);
        Client GetClient(int id);
        IEnumerable<Client> GetClients();
        void RemoveClient(Client client);
        Task<bool> SaveAllAsync();
        void UpdateClient(Client client);
    }
}