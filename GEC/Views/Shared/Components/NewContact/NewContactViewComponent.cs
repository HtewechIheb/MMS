using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Contacts;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Views.Shared.Components.NewContact
{
    public class NewContactViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IContactViewModel model, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            switch (websiteLanguage)
            {
                case WebsiteLanguage.Ar: return await Task.FromResult((IViewComponentResult)View("NewContactAr", model));
                default: return await Task.FromResult((IViewComponentResult)View("NewContactFr", model));
            }
        }
    }
}
