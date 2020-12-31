using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GEC_DataLayer.Models.Enumerations.Mail
{
    public enum MailType
    {
        [Display(Name = "Entrant")]
        Ingoing = 0,
        [Display(Name = "Sortant")]
        Outgoing = 1,
        [Display(Name = "Interne")]
        Internal = 2
    }
}
