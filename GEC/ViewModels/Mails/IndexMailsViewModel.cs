using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC_DataLayer.Models.Entities;

namespace GEC.ViewModels.Mails
{
    public class IndexMailsViewModel : IMailViewModel
    {
        public List<Mail> Mails { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
