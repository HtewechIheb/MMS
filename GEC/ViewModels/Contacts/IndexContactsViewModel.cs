using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC_DataLayer.Models.Entities;

namespace GEC.ViewModels.Contacts
{
    public class IndexContactsViewModel : IContactViewModel
    {
        public List<Contact> Contacts { get; set; }
        public List<Group> Groups { get; set; }
    }
}
