using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.ViewModels.Enumerations.General;
using Microsoft.AspNetCore.Mvc;

namespace GEC.Views.Shared.Components.DisplayGroups
{
    public class DisplayGroupsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            switch (websiteLanguage)
            {
                case WebsiteLanguage.Ar: return await Task.FromResult((IViewComponentResult)View("DisplayGroupsAr"));
                default: return await Task.FromResult((IViewComponentResult)View("DisplayGroupsFr"));
            }
        }
    }
}
