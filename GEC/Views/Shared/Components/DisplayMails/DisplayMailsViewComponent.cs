using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Mails;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Views.Shared.Components.DisplayMails
{
    public class DisplayMailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(DisplayMailsViewModel model, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            switch (websiteLanguage)
            {
                case WebsiteLanguage.Ar: return await Task.FromResult((IViewComponentResult)View("DisplayMailsAr", model));
                default: return await Task.FromResult((IViewComponentResult)View("DisplayMailsFr", model));
            }
        }
    }
}
