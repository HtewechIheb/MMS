using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GEC.ViewModels.Enumerations.Mails.AR
{
    public enum ChannelAr
    {
        [Display(Name = "وثيقة")]
        Hard = 0,
        [Display(Name = "بريد إلكتروني")]
        Email = 1,
        [Display(Name = "طرد بريدي")]
        ParcelPost = 2
    }
}
