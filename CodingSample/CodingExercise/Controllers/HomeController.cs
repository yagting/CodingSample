using CodingExercise.Models;
using CodingExercise.Models.Repositories;
using CodingExercise.Utilities;
using System.Web.Mvc;

namespace CodingExercise.Controllers
{
    /// <summary>
    /// This is the default controller
    /// </summary>
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] 
    public class HomeController : Controller
    {
        private IDevExporter _devExporter;

        public HomeController() : this(new DevExporter(new MdbRepository<SourceDeveloper>(), new SqlDbRepository(new SqlDbContext())))
        { }

        public HomeController(IDevExporter devExporter)
        {
            _devExporter = devExporter;
        }

        /// <summary>
        /// Default action in this controller
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Mongo DB Export to SQL Server";

            return View("Index");
        }

        /// <summary>
        /// Initiate the export of Mongo Data to SQL
        /// </summary>
        /// <returns></returns>
        [AjaxActionOnlyAttribute()]
        public ActionResult Export()
        {
            ReportBatch reportBatch = _devExporter.Export();

            if (reportBatch != null)
            {
                ViewBag.Message = "Processing Result";
                return PartialView("ShowReport", reportBatch);
            }
            else
            {
                return View("Index");
            }
        }
    }
}