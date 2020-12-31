using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC_DataLayer.Models.Entities;

namespace GEC.ViewModels.Folders
{
    public class IndexFoldersViewModel : IFolderViewModel
    {
        public List<Folder> Folders { get; set; }
    }
}
