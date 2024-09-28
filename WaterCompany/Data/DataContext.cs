using System.Collections.Generic;
using WaterCompany.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WaterCompany.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Bill> Bills { get; set; }
        
        public DbSet<BillDetail> BillDetails { get; set; }
        
        public DbSet<BillDetailTemp> BillDetailsTemp { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}




