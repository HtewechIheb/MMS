using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.ViewModels.Enumerations.General;
using GEC.ViewModels.Folders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GEC_DataLayer.Models.BLL;
using GEC_DataLayer.Models.Entities;

namespace GEC.Controllers.API
{
    [Route("API/Folders")]
    [ApiController]
    public class FoldersAPIController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromForm]NewFolderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Folder folder = new Folder(null, model.Name, model.Description);

                    long folderId = BLL_Folder.Add(folder);
                    folder.Id = folderId;

                    return CreatedAtAction(nameof(Show), new { idString = folderId }, folder);
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
                    Folder folder = BLL_Folder.SelectById(id);
                    if (folder != null)
                    {
                        return Ok(folder);
                    }
                    else
                    {
                        return NotFound("Folder Not Found.");
                    }
                }
                else if (idString.ToLower().Equals("all"))
                {
                    List<Folder> folders = BLL_Folder.SelectAll();
                    return Ok(folders);
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
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Update(long id, [FromForm]EditFolderViewModel model)
        {
            try
            {
                if (BLL_Folder.SelectById(id) == null)
                {
                    return NotFound("Folder Not Found.");
                }
                if (ModelState.IsValid)
                {
                    Folder updatedFolder = new Folder(null, model.Name, model.Description);
                    BLL_Folder.Update(id, updatedFolder);
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
                if (BLL_Folder.SelectById(id) == null)
                {
                    return NotFound("Folder Not Found.");
                }
                BLL_Folder.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("{id:long}/Mails")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetMails(long id)
        {
            try
            {
                Folder folder = BLL_Folder.SelectById(id);
                if (folder != null)
                {
                    List<Mail> mails = BLL_Folder.SelectMails(id);
                    return Ok(mails);
                }
                else
                {
                    return NotFound("Folder Not Found.");
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
        public IActionResult CreateNameValidation([Bind(Prefix = "NewFolder.Name")] string name, [Bind(Prefix = "NewFolder.WebsiteLanguage")] WebsiteLanguage websiteLanguage)
        {
            try
            {
                if (!BLL_Folder.CheckNameUnicity(name))
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
        public IActionResult EditNameValidation([Bind(Prefix = "EditFolder.Name")] string name, [Bind(Prefix = "EditFolder.Id")] long id, [Bind(Prefix = "EditFolder.WebsiteLanguage")] WebsiteLanguage websiteLanguage)
        {
            try
            {
                Folder currentFolder = BLL_Folder.SelectById(id);
                if (currentFolder != null && !currentFolder.Name.Equals(name) && !BLL_Folder.CheckNameUnicity(name))
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