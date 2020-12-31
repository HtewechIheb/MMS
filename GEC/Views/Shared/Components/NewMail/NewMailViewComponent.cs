using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Mails;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Views.Shared.Components.NewMail
{
    public class NewMailViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IMailViewModel model, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            switch (websiteLanguage)
            {
                case WebsiteLanguage.Ar: return await Task.FromResult((IViewComponentResult)View("NewMailAr", model));
                default: return await Task.FromResult((IViewComponentResult)View("NewMailFr", model));
            }
        }
    }
}
