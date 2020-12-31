using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC_DataLayer.Models.Enumerations.Contact;
using GEC.ViewModels.Enumerations.General;

namespace GEC.ViewModels.Contacts.AR
{
    public class NewContactArViewModel : IContactViewModel
    {
        [Required(ErrorMessage = "يجب ملء خانة الطبيعة.")]
        public Nature? Nature { get; set; }
        [Required(ErrorMessage = "يجب ملء خانة الصنف.")]
        public ContactType? ContactType { get; set; }
        [Required(ErrorMessage = "يجب ملء خانة الإسم."), MaxLength(50, ErrorMessage = "لا يمكن للإسم تجاوز 50 حرف."), Remote("CreateNameValidation", "ContactsAPI", AdditionalFields = "WebsiteLanguage")]
        public string Name { get; set; }
        [MaxLength(50, ErrorMessage = "لا يمكن للبريد الإلكتروني تجاوز 50 حرف."), DataType(DataType.EmailAddress)]
        public string Email1 { get; set; }
        [MaxLength(50, ErrorMessage = "لا يمكن للبريد الإلكتروني تجاوز 50 حرف."), DataType(DataType.EmailAddress)]
        public string Email2 { get; set; }
        [MaxLength(24, ErrorMessage = "لا يمكن لرقم الهاتف تجاوز 24 حرف."), DataType(DataType.PhoneNumber)]
        public string Telephone1 { get; set; }
        [MaxLength(24, ErrorMessage = "لا يمكن لرقم الهاتف تجاوز 24 حرف."), DataType(DataType.PhoneNumber)]
        public string Telephone2 { get; set; }
        [MaxLength(24, ErrorMessage = "لا يمكن للفاكس تجاوز 24 حرف.")]
        public string Fax { get; set; }
        [MaxLength(100, ErrorMessage = "لا يمكن للعنوان تجاوز 100 حرف.")]
        public string Address { get; set; }
        public WebsiteLanguage WebsiteLanguage { get; set; } = WebsiteLanguage.Ar;
    }
}
