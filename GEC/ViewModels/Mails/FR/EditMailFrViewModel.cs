using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GEC_DataLayer.Models.Enumerations.Mail;
using GEC.ViewModels.Enumerations.General;
using GEC.ViewModels.CustomValidation.Client.FR;

namespace GEC.ViewModels.Mails.FR
{
    public class EditMailFrViewModel : IMailViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Le champ Type est nécessaire.")]
        public MailType? MailType { get; set; }
        [Required(ErrorMessage = "Le champ Channel est nécessaire.")]
        public Channel? Channel { get; set; }
        [Required(ErrorMessage = "Le champ Date d'enregistrement est nécessaire."), DataType(DataType.Date)]
        public DateTime? RegistrationDate { get; set; }
        [Required(ErrorMessage = "Le champ Numéro d'enregistrement est nécessaire."), MaxLength(24, ErrorMessage = "Le nombre d'enregistrement ne peut pas dépasser 24 caractères.")]
        public string RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Le champ Expéditeur est nécessaire.")]
        public long? IdSender { get; set; }
        [MaxLength(24, ErrorMessage = "Le nombre d'enregistrement d'éxpediteur ne peut pas dépasser 24 caractères.")]
        public string SenderRegistrationNumber { get; set; }
        [DataType(DataType.Date), EditMailFrSendingDate]
        public DateTime? SendingDate { get; set; }
        [Required(ErrorMessage = "Le champ Destinataire est nécessaire.")]
        public long? IdRecipient { get; set; }
        public ProcessingTimeFrame? ProcessingTimeFrame { get; set; }
        public Confidentiality? Confidentiality { get; set; }
        [Required(ErrorMessage = "Le champ Objet est nécessaire."), MaxLength(24, ErrorMessage = "L'objet ne peut pas dépasser 255 caractères.")]
        public string Object { get; set; }
        public string Content { get; set; }
        public IFormFile DigitizedFile { get; set; }
        public Language? Language { get; set; }
        [MaxLength(24, ErrorMessage = "Les mots clés ne peuvent pas dépasser 100 caractères.")]
        public string KeyWords { get; set; }
        public string Observations { get; set; }
        public long? IdFolder { get; set; }
        [Required(ErrorMessage = "Le champ Copie Physique est nécessaire.")]
        public bool? HasHardCopy { get; set; }
        public WebsiteLanguage WebsiteLanguage { get; set; } = WebsiteLanguage.Fr;
    }
}
