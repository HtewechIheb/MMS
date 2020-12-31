using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Home;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult RedirectToIndex()
        {
            return RedirectToAction(nameof(Index), new { websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("{websiteLanguage}")]
        public IActionResult Index(WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            switch (websiteLanguage)
            {
                case WebsiteLanguage.Ar: return View("IndexAr", model);
                default: return View("IndexFr", model);
            }
        }
    }
}