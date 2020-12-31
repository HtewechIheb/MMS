using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GEC_DataLayer.Models.Enumerations.Mail
{
    public enum ProcessingTimeFrame
    {
        [Display(Name = "Aucun")]
        None = 0,
        [Display(Name = "Normal")]
        Normal = 1,
        [Display(Name = "Urgent")]
        Urgent = 2       
    }
}
