using System.Web.Mvc;

namespace CodingExercise.Controllers
{
    /// <summary>
    /// Controller class that handles Error events
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// An action class that is triggered when an error occurs. 
        /// This is automatically called when the <CustomError> setting is set in web.config 
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            ViewBag.ErrorCode = "500";
            ViewBag.ErrorMessage = "An unhandled exception was thrown.";

            return View("ErrorPage");
        }

        /// <summary>
        /// This is an error when a 404 is thrown. The settings in web.config 
        /// for error 404 must be set under <CustomError> in web.config
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult NotFound()
        {
            ViewBag.ErrorCode = "404";
            ViewBag.ErrorMessage = "The resource your are trying to access cannot be found. Please verify the url.";
            
            return View("ErrorPage");
        }
    }
}
