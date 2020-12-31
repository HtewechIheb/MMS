using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC_DataLayer.Models.Entities;

namespace GEC.ViewModels.Groups
{
    public class ShowGroupViewModel : IGroupViewModel
    {
        public Group Group { get; set; }
        public List<Group> Groups { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
