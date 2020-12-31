using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Enumerations.General;
using GEC.ViewModels.Groups;
using GEC_DataLayer.Models.BLL;
using GEC_DataLayer.Models.Entities;

namespace GEC.Controllers.API
{
    [Route("API/Groups")]
    [ApiController]
    public class GroupsAPIController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromForm]NewGroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Group group = new Group(null, model.Name, model.Description);

                    long groupId = BLL_Group.Add(group);
                    group.Id = groupId;

                    return CreatedAtAction(nameof(Show), new { idString = groupId }, group);
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
                    Group group = BLL_Group.SelectById(id);
                    if (group != null)
                    {
                        return Ok(group);
                    }
                    else
                    {
                        return NotFound("Group Not Found.");
                    }
                }
                else if (idString.ToLower().Equals("all"))
                {
                    List<Group> groups = BLL_Group.SelectAll();
                    return Ok(groups);
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
        public IActionResult Update(long id, [FromForm]EditGroupViewModel model)
        {
            try
            {
                Group currentGroup = BLL_Group.SelectById(id);
                if (currentGroup == null)
                {
                    return BadRequest("Group Not Found.");
                }

                if (ModelState.IsValid)
                {
                    Group group = new Group(null, model.Name, model.Description);

                    BLL_Group.Update(id, group);

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
                if (BLL_Group.SelectById(id) == null)
                {
                    return NotFound("Group Not Found.");
                }
                BLL_Group.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("{id:long}/Members")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetMembers(long id)
        {
            try
            {
                Group group = BLL_Group.SelectById(id);
                if (group != null)
                {
                    List<Contact> members = BLL_Group.SelectMembers(id);
                    return Ok(members);
                }
                else
                {
                    return NotFound("Group Not Found.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("{id:long}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult ManageMembers(long id, [FromForm]ManageGroupMembersViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BLL_Group.ManageMembers(model.IdContact, id);

                    return NoContent();
                }
                return BadRequest("Model Invalid.");
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
        public IActionResult CreateNameValidation([Bind(Prefix = "NewGroup.Name")] string name, [Bind(Prefix = "NewGroup.WebsiteLanguage")] WebsiteLanguage websiteLanguage)
        {
            try
            {
                if (!BLL_Group.CheckNameUnicity(name))
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
        public IActionResult EditNameValidation([Bind(Prefix = "EditGroup.Name")] string name, [Bind(Prefix = "EditGroup.Id")] long id, [Bind(Prefix = "EditGroup.WebsiteLanguage")] WebsiteLanguage websiteLanguage)
        {
            try
            {
                Group currentGroup = BLL_Group.SelectById(id);
                if (currentGroup != null && !currentGroup.Name.Equals(name) && !BLL_Group.CheckNameUnicity(name))
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