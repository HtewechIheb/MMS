using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GEC.ViewModels.Contacts;
using GEC_DataLayer.Models.BLL;
using GEC_DataLayer.Models.Entities;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Controllers
{
    public class ContactsController : Controller
    {
        [Route("Contacts")]
        public IActionResult RedirectToIndex()
        {
            return RedirectToAction(nameof(Index), new { websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("Contacts/{id:long}")]
        public IActionResult RedirectToShow(long id)
        {
            return RedirectToAction(nameof(Show), new { id = id, websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("{websiteLanguage}/Contacts")]
        [HttpGet]
        public IActionResult Index(WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            try
            {
                List<Contact> contacts = BLL_Contact.SelectAll();
                List<Group> groups = BLL_Group.SelectAll();

                IndexContactsViewModel model = new IndexContactsViewModel()
                {
                    Contacts = contacts,
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

        [Route("{websiteLanguage}/Contacts/{id:long}")]
        [HttpGet]
        public IActionResult Show(long id, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            try
            {
                Contact contact = BLL_Contact.SelectById(id);
                if (contact == null)
                {
                    return NotFound("Contact Not Found.");
                }
                List<Group> contactGroups = BLL_Contact.SelectGroups(id);

                ShowContactViewModel model = new ShowContactViewModel()
                {
                    Contact = contact,
                    ContactGroups = contactGroups
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