using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameLibraryApp.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            IExceptionHandlerPathFeature? exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
                ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;

            return View(); 
        }
    }
}
