using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC_DataLayer.Models.Entities;

namespace GEC.ViewModels.Contacts
{
    public class ShowContactViewModel : IContactViewModel
    {
        public Contact Contact { get; set; }
        public List<Group> ContactGroups { get; set; }
    }
}
