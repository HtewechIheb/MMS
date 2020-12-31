using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GEC_DataLayer.Models.Enumerations.Contact
{
    public enum Nature
    {
        [Display(Name = "Naturelle")]
        Natural = 0,
        [Display(Name = "Legalle")]
        LegalPerson = 1
    }
}
