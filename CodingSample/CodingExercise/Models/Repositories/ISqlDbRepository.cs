
namespace CodingExercise.Models.Repositories
{
    public interface ISqlDbRepository
    {
        void AddDeveloper(Developer dev);
        void AddReportBatch(ReportBatch rb);
        void SaveChanges();
    }
}
