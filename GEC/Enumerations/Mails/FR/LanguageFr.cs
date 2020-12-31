using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Mails.FR
{
    public enum LanguageFr
    {
        [Display(Name = "Aucun")]
        None = 0,
        [Display(Name = "Arabe")]
        Arabic = 1,
        [Display(Name = "Français")]
        French = 2,
        [Display(Name = "Englais")]
        English = 3,
        [Display(Name = "Allemand")]
        Deutsh = 4,
        [Display(Name = "Italien")]
        Italian = 5,
        [Display(Name = "Español")]
        Spanish = 6
    }
}
