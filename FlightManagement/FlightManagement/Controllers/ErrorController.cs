using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode, string? message = null)
        {
            ViewBag.ErrorCode = statusCode;

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorTitle = "Page Not Found";
                    ViewBag.ErrorMessage = message ?? "Oops! The page you are looking for doesn’t exist or might have been moved.";
                    break;

                case 403:
                    ViewBag.ErrorTitle = "Access Denied";
                    ViewBag.ErrorMessage = message ?? "You don’t have permission to access this page.";
                    break;

                case 401:
                    ViewBag.ErrorTitle = "Unauthorized";
                    ViewBag.ErrorMessage = message ?? "You need to log in first to view this content.";
                    break;

                default:
                    ViewBag.ErrorTitle = "Unexpected Error";
                    ViewBag.ErrorMessage = message ?? "An unexpected error occurred. Please go back to the home page.";
                    break;
            }

            return View("ErrorPage");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult GeneralError()
        {
            // Get details (for logging only)
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //Logging errors occured:
             _logger.LogError($"The path {exceptionDetails?.Path} threw" +
                 $" an exception {exceptionDetails?.Error}");

            ViewBag.ErrorTitle = "Server Error";
            ViewBag.ErrorMessage = "Something went wrong while processing your request. Please try again later.";

            return View("ErrorPage");
        }
    }
}
