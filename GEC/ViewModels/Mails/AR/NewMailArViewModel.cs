using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GEC_DataLayer.Models.Enumerations.Mail;
using GEC.ViewModels.Enumerations.General;
using GEC.ViewModels.CustomValidation.Client.AR;

namespace GEC.ViewModels.Mails.AR
{
    public class NewMailArViewModel : IMailViewModel
    {
        [Required(ErrorMessage = "يجب ملء خانة الصنف.")]
        public MailType? MailType { get; set; }
        [Required(ErrorMessage = "يجب ملء خانة الوسيلة.")]
        public Channel? Channel { get; set; }
        [Required(ErrorMessage = "يجب ملء خانة تاريخ التسجيل."), DataType(DataType.Date)]
        public DateTime? RegistrationDate { get; set; }
        [Required(ErrorMessage = "يجب ملء خانة رقم التسجيل."), MaxLength(24, ErrorMessage = "لا يمكن لرقم التسجيل تجاوز 24 حرف.")]
        public string RegistrationNumber { get; set; }
        [Required(ErrorMessage = "يجب ملء خانة المرسل.")]
        public long? IdSender { get; set; }
        [MaxLength(24, ErrorMessage = "لا يمكن لرقم المرسل تجاوز 24 حرف.")]
        public string SenderRegistrationNumber { get; set; }
        [DataType(DataType.Date), NewMailArSendingDate]
        public DateTime? SendingDate { get; set; }
        [Required(ErrorMessage = "يجب ملء خانة المرسل إليه.")]
        public long? IdRecipient { get; set; }
        public ProcessingTimeFrame? ProcessingTimeFrame { get; set; }
        public Confidentiality? Confidentiality { get; set; }
        [Required(ErrorMessage = "يجب ملء خانة الموضوع."), MaxLength(255, ErrorMessage = "لا يمكن للموضوع تجاوز 255 حرف.")]
        public string Object { get; set; }
        public string Content { get; set; }
        public IFormFile DigitizedFile { get; set; }
        public Language? Language { get; set; }
        [MaxLength(100, ErrorMessage = "لا يمكن للكلمات الدالة تجاوز 100 حرف.")]
        public string KeyWords { get; set; }
        public string Observations { get; set; }
        public long? IdFolder { get; set; }
        [Required(ErrorMessage = "يجب ملء خانة له نسخة.")]
        public bool? HasHardCopy { get; set; }
        public WebsiteLanguage WebsiteLanguage { get; set; } = WebsiteLanguage.Ar;
    }
}
