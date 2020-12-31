using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GEC_DataLayer.Models.Enumerations.Contact
{
    public enum ContactType
    {
        [Display(Name = "Interne")]
        Internal = 0,
        [Display(Name = "Externe")]
        External = 1
    }
}
