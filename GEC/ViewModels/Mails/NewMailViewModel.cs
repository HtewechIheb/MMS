using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GEC.ViewModels.CustomValidation;
using GEC_DataLayer.Models.Enumerations.Mail;

namespace GEC.ViewModels.Mails
{
    public class NewMailViewModel : IMailViewModel
    {
        [Required]
        public MailType? MailType { get; set; }
        [Required]
        public Channel? Channel { get; set; }
        [Required]
        public DateTime? RegistrationDate { get; set; }
        [Required, MaxLength(24)]
        public string RegistrationNumber { get; set; }
        [Required]
        public long? IdSender { get; set; }
        [MaxLength(24)]
        public string SenderRegistrationNumber { get; set; }
        [SendingDate]
        public DateTime? SendingDate { get; set; }
        [Required]
        public long? IdRecipient { get; set; }
        public ProcessingTimeFrame? ProcessingTimeFrame { get; set; }
        public Confidentiality? Confidentiality { get; set; }
        [Required, MaxLength(255)]
        public string Object { get; set; }
        public string Content { get; set; }
        public IFormFile DigitizedFile { get; set; }
        public Language? Language { get; set; }
        [MaxLength(100)]
        public string KeyWords { get; set; }
        public string Observations { get; set; }
        public long? IdFolder { get; set; }
        [Required]
        public bool? HasHardCopy { get; set; }
    }
}
