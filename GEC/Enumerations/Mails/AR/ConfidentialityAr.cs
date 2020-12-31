using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Mails.AR
{
    public enum ConfidentialityAr
    {
        [Display(Name = "بدون")]
        None = 0,
        [Display(Name = "عادي")]
        Normal = 1,
        [Display(Name = "سري")]
        Confidential = 2        
    }
}
