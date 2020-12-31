using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Mails.FR
{
    public enum ConfidentialityFr
    {
        [Display(Name = "Aucun")]
        None = 0,
        [Display(Name = "Normal")]
        Normal = 1,
        [Display(Name = "Confidentiel")]
        Confidential = 2        
    }
}
