using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GEC.ViewModels.CustomValidation;
using GEC_DataLayer.Models.Enumerations.Contact;

namespace GEC.ViewModels.Contacts
{
    public class NewContactViewModel : IContactViewModel
    {
        [Required]
        public Nature? Nature { get; set; }
        [Required]
        public ContactType? ContactType { get; set; }
        [Required, MaxLength(50), UniqueName]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email1 { get; set; }
        [MaxLength(50)]
        public string Email2 { get; set; }
        [MaxLength(24)]
        public string Telephone1 { get; set; }
        [MaxLength(24)]
        public string Telephone2 { get; set; }
        [MaxLength(24)]
        public string Fax { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
