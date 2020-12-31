using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Contacts.AR
{
    public enum NatureAr
    {
        [Display(Name = "شخص")]
        Natural = 0,
        [Display(Name = "مؤسسة")]
        LegalPerson = 1
    }
}
