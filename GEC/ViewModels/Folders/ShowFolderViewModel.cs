using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC_DataLayer.Models.Entities;

namespace GEC.ViewModels.Folders
{
    public class ShowFolderViewModel : IFolderViewModel
    {
        public Folder Folder { get; set; }
        public List<Folder> Folders { get; set; }
        public List<Mail> Mails { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
