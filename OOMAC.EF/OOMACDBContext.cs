using Microsoft.EntityFrameworkCore;
using OOMAC.Domain.Models;


namespace OOMAC.EF
{
    public class OOMACDBContext : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Contestant> Contestants { get; set; }

        public OOMACDBContext(DbContextOptions options) : base(options) { }

    }
}
