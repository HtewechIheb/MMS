using System;
using System.Collections.Generic;
using System.Text;
using GEC_DataLayer.Models.DAL;
using GEC_DataLayer.Models.Entities;

namespace GEC_DataLayer.Models.BLL
{
    public class BLL_Contact
    {
        public static bool CheckNameUnicity(string name)
        {
            return DAL_Contact.CheckNameUnicity(name);
        }

        public static long Add(Contact contact) {
            return DAL_Contact.Add(contact); 
        }

        public static void Update(long id, Contact contact)
        {
            DAL_Contact.Update(id, contact);
        }

        public static void Delete(long id)
        {
            DAL_Contact.Delete(id);
        }

        public static Contact SelectById(long id)
        {
            return DAL_Contact.SelectById(id);
        }

        public static List<Contact> SelectAll()
        {
            return DAL_Contact.SelectAll();
        }

        public static List<Group> SelectGroups(long id)
        {
            return DAL_Contact.SelectGroups(id);
        }
    }
}
