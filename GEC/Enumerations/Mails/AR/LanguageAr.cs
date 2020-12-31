using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Mails.AR
{
    public enum LanguageAr
    {
        [Display(Name = "بدون")]
        None = 0,
        [Display(Name = "العربية")]
        Arabic = 1,
        [Display(Name = "الفرنسية")]
        French = 2,
        [Display(Name = "الإنجليزية")]
        English = 3,
        [Display(Name = "الألمانية")]
        Deutsh = 4,
        [Display(Name = "الإيطالية")]
        Italian = 5,
        [Display(Name = "الإسبانية")]
        Spanish = 6
    }
}
