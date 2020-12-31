using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GEC.ViewModels.CustomValidation;

namespace GEC.ViewModels.Folders
{
    public class NewFolderViewModel : IFolderViewModel
    {
        [Required, MaxLength(128), UniqueName]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
