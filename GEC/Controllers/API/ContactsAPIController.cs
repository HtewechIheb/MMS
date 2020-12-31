using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Contacts;
using GEC.ViewModels.Enumerations.General;
using GEC_DataLayer.Models.Entities;
using GEC_DataLayer.Models.BLL;

namespace GEC.Controllers.API
{
    [Route("API/Contacts")]
    [ApiController]
    public class ContactsAPIController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromForm]NewContactViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contact contact = new Contact(null, model.Nature.Value, model.ContactType.Value, model.Name,
                        model.Email1, model.Email2, model.Telephone1, model.Telephone2, model.Fax, model.Address);

                    long contactId = BLL_Contact.Add(contact);
                    contact.Id = contactId;
                    return CreatedAtAction(nameof(Show), new { idString = contactId }, contact);
                }
                return BadRequest("Model Invalid.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("{idString}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Show(string idString)
        {
            try
            {
                if (long.TryParse(idString, out long id))
                {
                    Contact contact = BLL_Contact.SelectById(id);
                    if (contact != null)
                    {
                        return Ok(contact);
                    }
                    else
                    {
                        return NotFound("Contact Not Found.");
                    }
                }
                else if (idString.ToLower().Equals("all"))
                {
                    List<Contact> contacts = BLL_Contact.SelectAll();
                    return Ok(contacts);
                }
                return NotFound("Invalid Parameter.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("{id:long}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Update(long id, [FromForm]EditContactViewModel model)
        {
            try
            {
                if (BLL_Contact.SelectById(id) == null)
                {
                    return NotFound("Contact Not Found.");
                }

                if (ModelState.IsValid)
                {
                    Contact updatedContact = new Contact(null, model.Nature.Value, model.ContactType.Value, model.Name,
                        model.Email1, model.Email2, model.Telephone1, model.Telephone2, model.Fax, model.Address);

                    BLL_Contact.Update(id, updatedContact);
                    return NoContent();
                }
                return BadRequest("Model Invalid.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("{id:long}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(long id)
        {
            try
            {
                if (BLL_Contact.SelectById(id) == null)
                {
                    return NotFound("Contact Not Found.");
                }
                List<Mail> relatedMails = BLL_Mail.SelectByContactId(id);
                if (relatedMails != null)
                {
                    foreach (Mail mail in relatedMails)
                    {
                        BLL_Mail.Delete(mail.Id.Value);
                    }
                }
                BLL_Contact.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("{id:long}/Groups")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetGroups(long id)
        {
            try
            {
                Contact contact = BLL_Contact.SelectById(id);
                if (contact != null)
                {
                    List<Group> groups = BLL_Contact.SelectGroups(id);
                    return Ok(groups);
                }
                else
                {
                    return NotFound("Contact Not Found.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("Validation/Create")]
        [AcceptVerbs("Get", "Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateNameValidation([Bind(Prefix = "NewContact.Name")] string name, [Bind(Prefix = "NewContact.WebsiteLanguage")] WebsiteLanguage websiteLanguage)
        {
            try
            {
               if (!BLL_Contact.CheckNameUnicity(name))
                {
                    switch (websiteLanguage)
                    {
                        case WebsiteLanguage.Ar: return new JsonResult($"إسم {name} مستعمل.");
                        default: return new JsonResult($"Le nom {name} est déjà utilisé.");
                    }
                }
                return new JsonResult(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("Validation/Edit")]
        [AcceptVerbs("Get", "Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult EditNameValidation([Bind(Prefix = "EditContact.Name")] string name, [Bind(Prefix = "EditContact.Id")] long id, [Bind(Prefix = "EditContact.WebsiteLanguage")] WebsiteLanguage websiteLanguage)
        {
            try
            {
                Contact currentContact = BLL_Contact.SelectById(id);
                if (currentContact != null && !currentContact.Name.Equals(name) && !BLL_Contact.CheckNameUnicity(name))
                {
                    switch (websiteLanguage)
                    {
                        case WebsiteLanguage.Ar: return new JsonResult($"إسم {name} مستعمل.");
                        default: return new JsonResult($"Le nom {name} est déjà utilisé.");
                    }
                }
                return new JsonResult(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }
    }
}
