using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace OOMAC.EF
{
    public class OOMACDBContextFactory : IDesignTimeDbContextFactory<OOMACDBContext>
    {
        private readonly string _connectionString;

        public OOMACDBContextFactory()
        {
            _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=OOMAC;";
        }

        public OOMACDBContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<OOMACDBContext>();
            options.UseSqlServer(_connectionString);

            return new OOMACDBContext(options.Options);
        }
    }
}
