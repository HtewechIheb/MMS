using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Mails.AR
{
    public enum MailTypeAr
    {
        [Display(Name = "واردة")]
        Ingoing = 0,
        [Display(Name = "صادرة")]
        Outgoing = 1,
        [Display(Name = "داخلية")]
        Internal = 2
    }
}
