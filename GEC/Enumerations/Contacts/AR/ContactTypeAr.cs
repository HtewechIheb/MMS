using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Contacts.AR
{
    public enum ContactTypeAr
    {
        [Display(Name = "داخلي")]
        Internal = 0,
        [Display(Name = "خارجي")]
        External = 1
    }
}
