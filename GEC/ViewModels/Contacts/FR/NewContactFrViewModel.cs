using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC_DataLayer.Models.Enumerations.Contact;
using GEC.ViewModels.Enumerations.General;

namespace GEC.ViewModels.Contacts.FR
{
    public class NewContactFrViewModel : IContactViewModel
    {
        [Required(ErrorMessage = "Le champ Nature est nécessaire.")]
        public Nature? Nature { get; set; }
        [Required(ErrorMessage = "Le champ Type est nécessaire.")]
        public ContactType? ContactType { get; set; }
        [Required(ErrorMessage = "Le champ Nom est nécessaire."), MaxLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères."), Remote("CreateNameValidation", "ContactsAPI", AdditionalFields = "WebsiteLanguage")]
        public string Name { get; set; }
        [MaxLength(50, ErrorMessage = "L'email ne peut pas dépasser 50 caractères."), DataType(DataType.EmailAddress)]
        public string Email1 { get; set; }
        [MaxLength(50, ErrorMessage = "L'email ne peut pas dépasser 50 caractères."), DataType(DataType.EmailAddress)]
        public string Email2 { get; set; }
        [MaxLength(24, ErrorMessage = "Le numéro de téléphone ne peut pas dépasser 24 caractères."), DataType(DataType.PhoneNumber)]
        public string Telephone1 { get; set; }
        [MaxLength(24, ErrorMessage = "Le numéro de téléphone ne peut pas dépasser 24 caractères."), DataType(DataType.PhoneNumber)]
        public string Telephone2 { get; set; }
        [MaxLength(24, ErrorMessage = "Le fax ne peut pas dépasser 24 caractères.")]
        public string Fax { get; set; }
        [MaxLength(100, ErrorMessage = "L'adresse ne peut pas dépasser 100 caractères.")]
        public string Address { get; set; }
        public WebsiteLanguage WebsiteLanguage { get; set; } = WebsiteLanguage.Fr;

    }
}
