using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GEC_DataLayer.Models.Enumerations.Mail
{
    public enum Language
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
