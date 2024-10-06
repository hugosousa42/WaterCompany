using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            await _context.Database.MigrateAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Employee");
            await _userHelper.CheckRoleAsync("Client");


            if (!_context.Countries.Any())
            {
                var portugalCities = new List<City>()
                {
                    new City { Name = "Lisboa" },
                    new City { Name = "Porto" },
                    new City { Name = "Faro" }
                };
                _context.Countries.Add(new Country
                {
                    Cities = portugalCities,
                    Name = "Portugal"
                });
                var spainCities = new List<City>()
                {
                    new City { Name = "Barcelona" },
                    new City { Name = "Salamanca" },
                    new City { Name = "Madrid" }
                };
                _context.Countries.Add(new Country
                {
                    Cities = spainCities,
                    Name = "Spain"
                });

                await _context.SaveChangesAsync();
            }

            var userAdmin = await _userHelper.GetUserByEmailAsync("hugosb9@gmail.com");
            if (userAdmin == null)
            {
                userAdmin = new User
                {
                    FirstName = "Hugo",
                    LastName = "Sousa",
                    Email = "hugosb9@gmail.com",
                    UserName = "hugosb9@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "Coimbra Street",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(userAdmin, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(userAdmin, "Admin");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(userAdmin);
                await _userHelper.ConfirmEmailAsync(userAdmin, token);
            }

            var IsAdminInRole = await _userHelper.IsUserInRoleAsyc(userAdmin, "Admin");
            if (!IsAdminInRole)
            {
                await _userHelper.AddUserToRoleAsync(userAdmin, "Admin");
            }

            var userEmployee = await _userHelper.GetUserByEmailAsync("John@gmail.com");
            if (userEmployee == null)
            {
                userEmployee = new User
                {
                    FirstName = "John",
                    LastName = "Lennon",
                    Email = "john@gmail.com",
                    UserName = "john@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "Lisbon Street",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(userEmployee, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(userEmployee, "Employee");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(userEmployee);
                await _userHelper.ConfirmEmailAsync(userEmployee, token);
            }

            var IsEmployeeInRole = await _userHelper.IsUserInRoleAsyc(userEmployee, "Employee");
            if (!IsEmployeeInRole)
            {
                await _userHelper.AddUserToRoleAsync(userEmployee, "Employee");
            }

            var userClient = await _userHelper.GetUserByEmailAsync("marie@gmail.com");
            if (userClient == null)
            {
                userClient = new User
                {
                    FirstName = "Marie",
                    LastName = "Curie",
                    Email = "marie@gmail.com",
                    UserName = "marie@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "Porto Street",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(userClient, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(userClient, "Client");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(userClient);
                await _userHelper.ConfirmEmailAsync(userClient, token);
            }

            var IsClientInRole = await _userHelper.IsUserInRoleAsyc(userClient, "Client");
            if (!IsClientInRole)
            {
                await _userHelper.AddUserToRoleAsync(userClient, "Client");
            }

            if (!_context.Clients.Any())
            {
                AddClient("Marie", userClient);

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
                Birthdate = DateTime.Now,
                user = user
            });
        }
    }
}
