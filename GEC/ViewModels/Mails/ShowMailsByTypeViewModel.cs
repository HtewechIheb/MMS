using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC_DataLayer.Models.Entities;
using GEC_DataLayer.Models.Enumerations.Mail;

namespace GEC.ViewModels.Mails
{
    public class ShowMailsByTypeViewModel : IMailViewModel
    {
        public MailType MailType { get; set; }
        public List<Mail> Mails { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
