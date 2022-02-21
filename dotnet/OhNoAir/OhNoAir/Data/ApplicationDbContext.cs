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
        public DbSet<Destination> Destination { get; set; }
    }
}
