using Microsoft.EntityFrameworkCore;
using OhNoAir.Models;

namespace OhNoAir.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Airport> Airport { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Account> Account { get; set; }

    }
}
