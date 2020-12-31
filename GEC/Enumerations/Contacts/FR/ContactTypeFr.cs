using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Contacts.FR
{
    public enum ContactTypeFr
    {
        [Display(Name = "Interne")]
        Internal = 0,
        [Display(Name = "Externe")]
        External = 1
    }
}
