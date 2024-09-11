using System.Collections.Generic;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;

namespace WaterCompany.Data
{
    public class MockRepository : IRepository
    {
        public void AddClient(Client client)
        {
            throw new System.NotImplementedException();
        }

        public bool ClientExists(int id)
        {
            throw new System.NotImplementedException();
        }

        public Client GetClient(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>();
            clients.Add(new Client {Id=1, Name="John", Email="John.com", PhoneNumber="123456789", Address="John Street"});
            clients.Add(new Client {Id=2, Name="Marie", Email= "Marie.com", PhoneNumber="012345678", Address= "Marie Street" });
            clients.Add(new Client {Id=3, Name="Judith", Email= "Judith.com", PhoneNumber="901234567", Address= "Judith Street" });
            clients.Add(new Client {Id=4, Name="Paul", Email= "Paul.com", PhoneNumber="890123456", Address= "Paul Street" });
            
            return clients;
        }

        public void RemoveClient(Client client)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateClient(Client client)
        {
            throw new System.NotImplementedException();
        }
    }
}
