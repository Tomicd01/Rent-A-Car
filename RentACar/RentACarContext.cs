using Microsoft.EntityFrameworkCore;
using RentACar.Models;

namespace RentACar
{
    public class RentACarContext : DbContext
    {

        public RentACarContext(DbContextOptions<RentACarContext> options) : base(options) 
        {

        }
        public DbSet<Car> Car { get; set; }

        public DbSet<Rental> Rental { get; set; }

        public DbSet<User>  User { get; set; }

        public DbSet<Payment> Payment { get; set; }

        public DbSet<Review> Review { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                .Ignore(r => r.Car)
                .Ignore(r => r.User);

            modelBuilder.Entity<Payment>()
                .Ignore(p => p.Rental);
        }


    }
}
