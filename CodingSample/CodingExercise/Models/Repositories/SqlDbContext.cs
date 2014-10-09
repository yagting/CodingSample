using System.Data.Entity;

namespace CodingExercise.Models.Repositories
{
    /// <summary>
    /// This class implements the entity framework DbContext
    /// </summary>
    public class SqlDbContext : DbContext
    {
        public SqlDbContext()
            : this("SQLDbContext")
        { }

        public SqlDbContext(string connectionStringName = "SQLDbContext")
            : base(connectionStringName)
        { }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Contrib> Contribs { get; set; }
        public DbSet<ReportBatch> ReportBatches { get; set; }
    }
}