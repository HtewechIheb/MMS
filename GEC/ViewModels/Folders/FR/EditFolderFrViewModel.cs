using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEC.ViewModels.Enumerations.General;

namespace GEC.ViewModels.Folders
{
    public class EditFolderFrViewModel : IFolderViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Le champ Nom est nécessaire."), MaxLength(128, ErrorMessage = "Le nom du dossier ne peut pas dépasser 128 caractères."), Remote("EditNameValidation", "FoldersAPI", AdditionalFields = "Id, WbesiteLanguage")]
        public string Name { get; set; }
        public string Description { get; set; }
        public WebsiteLanguage WebsiteLanguage { get; set; } = WebsiteLanguage.Fr;
    }
}
