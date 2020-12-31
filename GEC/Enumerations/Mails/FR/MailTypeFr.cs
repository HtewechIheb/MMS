using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Mails.FR
{
    public enum MailTypeFr
    {
        [Display(Name = "Entrant")]
        Ingoing = 0,
        [Display(Name = "Sortant")]
        Outgoing = 1,
        [Display(Name = "Interne")]
        Internal = 2
    }
}
