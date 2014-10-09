
namespace CodingExercise.Models.Repositories
{
    /// <summary>
    /// This is the repository w/c implements the ISqlDbRepository interface
    /// </summary>
    public sealed class SqlDbRepository : ISqlDbRepository
    {
        private SqlDbContext _sqlDbCtx = null;

        public SqlDbRepository(SqlDbContext sqlDbCtx)
        {
            _sqlDbCtx = sqlDbCtx;
        }

        /// <summary>
        /// Add a Developer, Awards and Contribs to the DB Context
        /// </summary>
        /// <param name="dev">Instance of type Developer</param>
        public void AddDeveloper(Developer dev)
        {
            _sqlDbCtx.Developers.Add(dev);
            _sqlDbCtx.Contribs.AddRange(dev.Contribs);
            _sqlDbCtx.Awards.AddRange(dev.Awards);
        }

        /// <summary>
        /// Add a ReportBatch to the Database to keep track of process batch runs
        /// </summary>
        /// <param name="rb"></param>
        public void AddReportBatch(ReportBatch rb)
        {
            _sqlDbCtx.ReportBatches.Add(rb);
        }

        /// <summary>
        /// Commits the changes to the database
        /// </summary>
        public void SaveChanges()
        {
            _sqlDbCtx.SaveChanges();
        }
    }
}