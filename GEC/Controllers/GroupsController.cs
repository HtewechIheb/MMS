using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Groups;
using GEC_DataLayer.Models.Entities;
using GEC.ViewModels.Enumerations.General;
using GEC_DataLayer.Models.BLL;

namespace GEC.Controllers
{
    public class GroupsController : Controller
    {
        [Route("Groups")]
        public IActionResult RedirectToIndex()
        {
            return RedirectToAction(nameof(Index), new { websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("Groups/{id:long}")]
        public IActionResult RedirectToShow(long id)
        {
            return RedirectToAction(nameof(Show), new { id = id, websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("{websiteLanguage}/Groups")]
        [HttpGet]
        public IActionResult Index(WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            try
            {
                List<Group> groups = BLL_Group.SelectAll();

                IndexGroupsViewModel model = new IndexGroupsViewModel()
                {
                    Groups = groups
                };
                switch (websiteLanguage)
                {
                    case WebsiteLanguage.Ar: return View("IndexAr", model);
                    default: return View("IndexFr", model);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("{websiteLanguage}/Groups/{id:long}")]
        [HttpGet]
        public IActionResult Show(long id, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            try
            {
                Group group = BLL_Group.SelectById(id);
                if (group == null)
                {
                    return NotFound("Group Not Found.");
                }
                List<Contact> groupContacts = BLL_Group.SelectMembers(id);
                List<Group> groups = BLL_Group.SelectAll();

                ShowGroupViewModel model = new ShowGroupViewModel()
                {
                    Group = group,
                    Groups = groups,
                    Contacts = groupContacts
                };
                switch (websiteLanguage)
                {
                    case WebsiteLanguage.Ar: return View("ShowAr", model);
                    default: return View("ShowFr", model);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }
    }
}