using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Contacts.FR
{
    public enum NatureFr
    {
        [Display(Name = "Personne Physique")]
        Natural = 0,
        [Display(Name = "Personne Morale")]
        LegalPerson = 1
    }
}
