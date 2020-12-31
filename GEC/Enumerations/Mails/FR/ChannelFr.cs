using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Mails.FR
{
    public enum ChannelFr
    {
        [Display(Name = "Document")]
        Hard = 0,
        [Display(Name = "Email")]
        Email = 1,
        [Display(Name = "Colis Postal")]
        ParcelPost = 2
    }
}
