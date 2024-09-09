using System;
using System.Linq;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;

namespace WaterCompany.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Clients.Any())
            {
                AddClient("John");
                AddClient("Marie");
                AddClient("Sarah");
                AddClient("Paul");
                AddClient("Andrew");

                await _context.SaveChangesAsync();
            }
        }


        private void AddClient(string name)
        {
            _context.Clients.Add(new Client
            {
                Name = name,
                Email = name + "@email.com",
                PhoneNumber = Convert.ToString(_random.Next(1000000000)),
                Address = name + " Street",
                RegistrationDate = DateTime.Now
            });
        }
    }
}
