using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC_DataLayer.Models.Entities;

namespace GEC.ViewModels.Mails
{
    public class ShowMailViewModel : IMailViewModel
    {
        public Mail Mail { get; set; }
        public Folder Folder { get; set; }
        public Contact Sender { get; set; }
        public Contact Recipient { get; set; }
    }
}
