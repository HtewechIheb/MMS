using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GEC_DataLayer.Models.Enumerations.Mail
{
    public enum Channel
    {
        [Display(Name = "Dur")]
        Hard = 0,
        [Display(Name = "Email")]
        Email = 1,
        [Display(Name = "Colis Postal")]
        ParcelPost = 2
    }
}
