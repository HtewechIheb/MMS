using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using GEC.ViewModels.Mails;
using GEC_DataLayer.Models.BLL;
using GEC_DataLayer.Models.Entities;
using GEC_DataLayer.Models.Enumerations.Mail;
using GEC.ViewModels.Enumerations.General;

namespace GEC.Controllers
{
    public class MailsController : Controller
    {
        private readonly IWebHostEnvironment webHostingEnvironment;

        public MailsController(IWebHostEnvironment webHostingEnvironment)
        {
            this.webHostingEnvironment = webHostingEnvironment;
        }

        [Route("Mails")]
        public IActionResult RedirectToIndex()
        {
            return RedirectToAction(nameof(Index), new { websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("Mails/{id:long}")]
        public IActionResult RedirectToShow(long id)
        {
            return RedirectToAction(nameof(Show), new { id = id, websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("Mails/{type}")]
        public IActionResult RedirectToShowMailsByType(string type)
        {
            return RedirectToAction(nameof(ShowMailsByType), new { type = type, websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("Mails/Search")]
        public IActionResult RedirectToSearch(string queryString, string dayString = null, string monthString = null, string yearString = null)
        {
            return RedirectToAction(nameof(Search), new { queryString = queryString, dayString = dayString, monthString = monthString, yearString = yearString, websiteLanguage = WebsiteLanguage.Fr });
        }

        [Route("{websiteLanguage}/Mails")]
        [HttpGet]
        public IActionResult Index(WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            try
            {
                List<Mail> mails = BLL_Mail.SelectAll();
                List<Contact> contacts = BLL_Contact.SelectAll();

                IndexMailsViewModel model = new IndexMailsViewModel()
                {
                    Mails = mails,
                    Contacts = contacts,
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

        [Route("{websiteLanguage}/Mails/{id:long}")]
        [HttpGet]
        public IActionResult Show(long id, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            try
            {
                Mail mail;
                Folder mailFolder;
                Contact mailSender, mailRecipient;
                mail = BLL_Mail.SelectById(id);
                if (mail == null)
                {
                    return NotFound("Mail Not Found.");
                }
                if (mail.IdFolder.HasValue)
                {
                    mailFolder = BLL_Folder.SelectById(mail.IdFolder.Value);
                }
                else
                {
                    mailFolder = null;
                }
                mailSender = BLL_Contact.SelectById(mail.IdSender);
                mailRecipient = BLL_Contact.SelectById(mail.IdRecipient);

                ShowMailViewModel model = new ShowMailViewModel()
                {
                    Mail = mail,
                    Folder = mailFolder,
                    Sender = mailSender,
                    Recipient = mailRecipient
                };
                switch(websiteLanguage)
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

        [Route("{websiteLanguage}/Mails/{type}")]
        [HttpGet]
        public IActionResult ShowMailsByType(string type, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            try
            {
                if (!Enum.TryParse(typeof(MailType), type, true, out object result))
                {
                    return NotFound("Invalid Parameter.");
                }
                List<Mail> mails = BLL_Mail.SelectByType(type);
                List<Contact> contacts = BLL_Contact.SelectAll();

                ShowMailsByTypeViewModel model = new ShowMailsByTypeViewModel()
                {
                    MailType = (MailType)Enum.Parse(typeof(MailType), type),
                    Mails = mails,
                    Contacts = contacts,
                };

                switch (websiteLanguage)
                {
                    case WebsiteLanguage.Ar: return View("ShowTypeAr", model);
                    default: return View("ShowTypeFr", model);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("{websiteLanguage}/Mails/Search")]
        [HttpGet]
        public IActionResult Search(string queryString, string dayString = null, string monthString = null, string yearString = null, WebsiteLanguage websiteLanguage = WebsiteLanguage.Fr)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(queryString))
                {
                    return BadRequest("Query String is Null.");
                }

                List<Mail> mails = BLL_Mail.Search(queryString, dayString, monthString, yearString);
                List<Contact> contacts = BLL_Contact.SelectAll();

                IndexMailsViewModel model = new IndexMailsViewModel()
                {
                    Mails = mails,
                    Contacts = contacts
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
    }
}