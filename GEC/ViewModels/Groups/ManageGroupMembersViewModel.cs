using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.ViewModels.Groups
{
    public class ManageGroupMembersViewModel : IGroupViewModel
    {
        public long[] IdContact { get; set; }
        public long IdGroup { get; set; }
    }
}
