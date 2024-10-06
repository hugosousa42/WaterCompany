using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WaterCompany.Data.Entities;

namespace WaterCompany.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<BillDetail> BillDetails { get; set; }

        public DbSet<BillDetailTemp> BillDetailsTemp { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne(c => c.user)
                .WithOne()
                .HasForeignKey<Client>(c => c.UserId);

            modelBuilder.Entity<Country>()
                .HasIndex(c => c.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
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

