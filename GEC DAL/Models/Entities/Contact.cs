using System;
using System.Collections.Generic;
using System.Text;
using GEC_DataLayer.Models.Enumerations.Contact;


namespace GEC_DataLayer.Models.Entities
{
    public class Contact
    {
        public long? Id { get; set; }
        public Nature Nature { get; set; }
        public ContactType ContactType { get; set; }
        public string Name { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }

        public Contact()
        {

        }

        public Contact(long? pId, Nature pNature, ContactType pContactType, string pName, string pEmail1,
            string pEmail2, string pTelephone1, string pTelephone2, string pFax, string pAddress)
        {
            if (string.IsNullOrWhiteSpace(pName))
            {
                throw new ArgumentException("Name is required.");
            }

            Id = pId;
            Nature = pNature;
            ContactType = pContactType;
            Name = pName;
            Email1 = pEmail1;
            Email2 = pEmail2;
            Telephone1 = pTelephone1;
            Telephone2 = pTelephone2;
            Fax = pFax;
            Address = pAddress;
        }
    }
}
