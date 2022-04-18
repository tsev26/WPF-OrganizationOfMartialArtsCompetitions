using Microsoft.EntityFrameworkCore;
using OOMAC.Domain.Models;
using OOMAC.Domain.Models.Calculating;

namespace OOMAC.EF
{
    public class OOMACDBContext : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Contestant> Contestants { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<TournamentSummary> TournamentSummary { get; set; }

        public OOMACDBContext(DbContextOptions options) : base(options) { }

    }
}
