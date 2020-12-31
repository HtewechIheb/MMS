using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Folders;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Views.Shared.Components.EditFolder
{
    public class EditFolderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IFolderViewModel model, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            switch (websiteLanguage)
            {
                case WebsiteLanguage.Ar: return await Task.FromResult((IViewComponentResult)View("EditFolderAr", model));
                default: return await Task.FromResult((IViewComponentResult)View("EditFolderFr", model));
            }
        }
    }
}
