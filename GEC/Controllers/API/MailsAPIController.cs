using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using GEC.ViewModels.Mails;
using GEC_DataLayer.Models.Entities;
using GEC_DataLayer.Models.BLL;
using GEC_DataLayer.Models.Enumerations.Mail;

namespace GEC.Controllers.API
{
    [Route("API/Mails")]
    [ApiController]
    public class MailsAPIController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostingEnvironment;

        public MailsAPIController(IWebHostEnvironment webHostingEnvironment)
        {
            this.webHostingEnvironment = webHostingEnvironment;
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromForm]NewMailViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;

                    if (model.DigitizedFile != null)
                    {
                        string uploadsFolder = Path.Combine(webHostingEnvironment.WebRootPath, "Mails");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.DigitizedFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            model.DigitizedFile.CopyTo(stream);
                        }
                    }

                    Mail mail = new Mail(null, model.MailType.Value, model.Channel.Value, model.RegistrationDate.Value,
                    GenerateRegistrationNumber(model.MailType.Value), model.IdSender.Value, model.SenderRegistrationNumber, model.SendingDate,
                    model.IdRecipient.Value, model.ProcessingTimeFrame, model.Confidentiality, model.Object, model.Content,
                    uniqueFileName, model.Language, model.KeyWords, model.Observations, model.IdFolder, model.HasHardCopy.Value);

                    long mailId = BLL_Mail.Add(mail);
                    mail.Id = mailId;

                    return CreatedAtAction(nameof(Show), new { idString = mailId }, mail);
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
                    Mail mail = BLL_Mail.SelectById(id);
                    if (mail != null)
                    {
                        return Ok(mail);
                    }
                    else
                    {
                        return NotFound("Mail Not Found.");
                    }
                }
                else if (Enum.TryParse(typeof(MailType), idString, true, out object result))
                {
                    List<Mail> mails = BLL_Mail.SelectByType(idString);
                    return Ok(mails);
                }
                else if (idString.ToLower().Equals("all"))
                {
                    List<Mail> mails = BLL_Mail.SelectAll();
                    return Ok(mails);
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
        public IActionResult Update(long id, [FromForm]EditMailViewModel model)
        {
            try
            {
                Mail currentMail = BLL_Mail.SelectById(id);
                if (currentMail == null)
                {
                    return NotFound("Mail Not Found.");
                }

                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;

                    if (model.DigitizedFile != null)
                    {
                        string uploadsFolder = Path.Combine(webHostingEnvironment.WebRootPath, "Mails");
                        if (currentMail.DigitizedFile != null)
                        {
                            System.IO.File.Delete(Path.Combine(uploadsFolder, currentMail.DigitizedFile));
                        }
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.DigitizedFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            model.DigitizedFile.CopyTo(stream);
                        };
                    }

                    Mail updatedMail = new Mail(null, model.MailType.Value, model.Channel.Value, model.RegistrationDate.Value,
                    model.RegistrationNumber, model.IdSender.Value, model.SenderRegistrationNumber, model.SendingDate,
                    model.IdRecipient.Value, model.ProcessingTimeFrame, model.Confidentiality, model.Object, model.Content,
                    uniqueFileName, model.Language, model.KeyWords, model.Observations, model.IdFolder, model.HasHardCopy.Value);

                    BLL_Mail.Update(id, updatedMail);

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
                Mail currentMail = BLL_Mail.SelectById(id);
                if (currentMail == null)
                {
                    return NotFound("Mail Not Found.");
                }
                string uploadsFolder = Path.Combine(webHostingEnvironment.WebRootPath, "Mails");
                if (currentMail.DigitizedFile != null)
                {
                    System.IO.File.Delete(Path.Combine(uploadsFolder, currentMail.DigitizedFile));
                }
                BLL_Mail.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("Search")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Search(string queryString, string dayString = null, string monthString = null, string yearString = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(queryString))
                {
                    return BadRequest("Query String is Null.");
                }

                List<Mail> searchResult = BLL_Mail.Search(queryString, dayString, monthString, yearString);

                return Ok(searchResult);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [Route("Download/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Download(int id)
        {
            try
            {
                Mail mail = BLL_Mail.SelectById(id);
                if (mail == null)
                {
                    return NotFound("File Not Found.");
                }

                string fileName = mail.DigitizedFile;
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return NotFound("File Not Found.");
                }
                string filePath = Path.Combine(webHostingEnvironment.WebRootPath, "Mails");
                FileStream fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Open, FileAccess.Read);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(fileName, out string contentType))
                {
                    contentType = "application/octet-stream";
                }
                return new FileStreamResult(fileStream, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue(contentType));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        private string padNumber(long number, int length)
        {
            string numberString = number.ToString();
            int numberLength = numberString.Length;
            List<char> numberDigits = numberString.ToList();
            if (numberLength < length)
            {
                for (int i = 0; i < length - numberLength; i++)
                {
                    numberDigits.Insert(0, '0');
                }
            }
            return string.Join(null, numberDigits);
        }

        private string GenerateRegistrationNumber(MailType mailType)
        {
            int mailsPerYearDigit = 4;
            string currentYear = DateTime.Now.ToString("yy");
            string mailNumber = padNumber(BLL_Mail.GetMailNumber(), mailsPerYearDigit);
            string registrationNumber = "";
            switch ((int)mailType)
            {
                case 0:
                    registrationNumber = $"E{currentYear}{mailNumber}";
                    break;
                case 1:
                    registrationNumber = $"S{currentYear}{mailNumber}";
                    break;
                case 2:
                    registrationNumber = $"I{currentYear}{mailNumber}";
                    break;
            }
            return registrationNumber;
        }


        [Route("Generator/RegistrationNumber")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetRegistrationNumber(MailType mailType)
        {
            try
            {
                return Ok(GenerateRegistrationNumber(mailType));
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }
    }
}