using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Folders;
using GEC_DataLayer.Models.Entities;
using GEC_DataLayer.Models.BLL;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Controllers
{
    public class FoldersController : Controller
    {
        [Route("Folders")]
        public IActionResult RedirectToIndex()
        {
            return RedirectToAction(nameof(Index), new { websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("Folders/{id:long}")]
        public IActionResult RedirectToShow(long id)
        {
            return RedirectToAction(nameof(Show), new { id = id, websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("{websiteLanguage}/Folders")]
        [HttpGet]
        public IActionResult Index(WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            try
            {
                List<Folder> folders = BLL_Folder.SelectAll();

                IndexFoldersViewModel model = new IndexFoldersViewModel()
                {
                    Folders = folders
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

        [Route("{websiteLanguage}/Folders/{id:long}")]
        [HttpGet]
        public IActionResult Show(long id, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            try
            {
                Folder folder = BLL_Folder.SelectById(id);
                if (folder == null)
                {
                    return NotFound("Folder Not Found.");
                }
                List<Mail> folderMails = BLL_Folder.SelectMails(id);
                List<Folder> folders = BLL_Folder.SelectAll();
                List<Contact> contacts = BLL_Contact.SelectAll();

                ShowFolderViewModel model = new ShowFolderViewModel()
                {
                    Folder = folder,
                    Folders = folders,
                    Mails = folderMails,
                    Contacts = contacts
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