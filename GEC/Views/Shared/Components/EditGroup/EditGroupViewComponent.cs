using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Groups;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Views.Contacts.Components.EditGroup
{
    public class EditGroupViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IGroupViewModel model, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            switch (websiteLanguage)
            {
                case WebsiteLanguage.Ar: return await Task.FromResult((IViewComponentResult)View("EditGroupAr", model));
                default: return await Task.FromResult((IViewComponentResult)View("EditGroupFr", model));
            }
        }
    }
}
