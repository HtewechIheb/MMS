using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Contacts;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Views.Contacts.Components.EditContact
{
    public class EditContactViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IContactViewModel model, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            switch (websiteLanguage)
            {
                case WebsiteLanguage.Ar: return await Task.FromResult((IViewComponentResult)View("EditContactAr", model));
                default: return await Task.FromResult((IViewComponentResult)View("EditContactFr", model));
            }
        }
    }
}
