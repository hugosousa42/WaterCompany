using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WaterCompany.Data.Entities;
using WaterCompany.Helpers;

namespace WaterCompany.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("hugosb9@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Hugo",
                    LastName = "Sousa",
                    Email = "hugosb9@gmail.com",
                    UserName = "hugosb9@gmail.com",
                    PhoneNumber = "123456789"
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!_context.Clients.Any())
            {
                AddClient("John", user);
                AddClient("Marie", user);
                AddClient("Sarah", user);
                AddClient("Paul", user);
                AddClient("Andrew", user);

                await _context.SaveChangesAsync();
            }
        }


        private void AddClient(string name, User user)
        {
            _context.Clients.Add(new Client
            {
                Name = name,
                ImageUrl = string.Empty,
                Email = name + "@email.com",
                PhoneNumber = Convert.ToString(_random.Next(1000000000)),
                Address = name + " Street",
                RegistrationDate = DateTime.Now,
                user = user
            });
        }
    }
}
