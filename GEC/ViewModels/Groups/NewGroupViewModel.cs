using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GEC.ViewModels.CustomValidation;

namespace GEC.ViewModels.Groups
{
    public class NewGroupViewModel : IGroupViewModel
    {
        [Required, MaxLength(128), UniqueName]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
