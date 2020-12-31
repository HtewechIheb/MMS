using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.ViewModels.Enumerations.General;
using Microsoft.AspNetCore.Mvc;

namespace GEC.Views.Shared.Components.DisplayFolders
{
    public class DisplayFoldersViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            switch (websiteLanguage)
            {
                case WebsiteLanguage.Ar: return await Task.FromResult((IViewComponentResult)View("DisplayFoldersAr"));
                default: return await Task.FromResult((IViewComponentResult)View("DisplayFoldersFr"));
            }
        }
    }
}
