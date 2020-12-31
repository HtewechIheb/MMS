using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Groups;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Views.Contacts.Components.AddContactToGroup
{
    public class ManageGroupMembersViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IGroupViewModel model, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            switch (websiteLanguage)
            {
                case WebsiteLanguage.Ar: return await Task.FromResult((IViewComponentResult)View("ManageGroupMembersAr", model));
                default: return await Task.FromResult((IViewComponentResult)View("ManageGroupMembersFr", model));
            }
        }
    }
}
