using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC_DataLayer.Models.Entities;

namespace GEC.ViewModels.Groups
{
    public class IndexGroupsViewModel : IGroupViewModel
    {
        public List<Group> Groups { get; set; }
    }
}
