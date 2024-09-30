using System.Collections.Generic;
using WaterCompany.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

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

        //Habilitar a regra de apagar em cascata (cascade delete rule)
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    var cascadeFKs = modelBuilder.Model
        //        .GetEntityTypes()
        //        .SelectMany(t => t.GetForeignKeys())
        //        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        //    foreach (var fk in cascadeFKs)
        //    {
        //        fk.DeleteBehavior = DeleteBehavior.Restrict;
        //    }

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}




