using System;
using System.Collections.Generic;
using System.Text;
using GEC_DataLayer.Models.Entities;
using GEC_DataLayer.Models.DAL;

namespace GEC_DataLayer.Models.BLL
{
    public class BLL_Mail
    {
        public static long Add(Mail mail)
        {
            return DAL_Mail.Add(mail);
        }

        public static void Update(long id, Mail updatedMail)
        {
            DAL_Mail.Update(id, updatedMail);
        }

        public static void Delete(long id)
        {
            DAL_Mail.Delete(id);
        }

        public static Mail SelectById(long id)
        {
            return DAL_Mail.SelectById(id);
        }

        public static List<Mail> SelectByType(string type)
        {
            return DAL_Mail.SelectByType(type);
        }

        public static List<Mail> SelectAll()
        {
            return DAL_Mail.SelectAll();
        }

        public static List<Mail> SelectByContactId(long id)
        {
            return DAL_Mail.SelectByContactId(id);
        }

        public static List<Mail> Search(string queryString, string dayString = null, string monthString = null, string yearString = null)
        {
            return DAL_Mail.Search(queryString, dayString, monthString, yearString);
        }

        public static long GetMailNumber()
        {
            return DAL_Mail.GetMailNumber();
        }
    }
}
