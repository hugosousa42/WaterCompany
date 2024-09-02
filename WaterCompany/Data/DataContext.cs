using System.Collections.Generic;
using WaterCompany.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WaterCompany.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}

