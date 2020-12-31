using System;
using System.Collections.Generic;
using System.Text;
using GEC_DataLayer.Models.DAL;
using GEC_DataLayer.Models.Entities;

namespace GEC_DataLayer.Models.BLL
{
    public class BLL_Folder
    {
        public static bool CheckNameUnicity(string name)
        {
            return DAL_Folder.CheckNameUnicity(name);
        }
        public static long Add(Folder folder)
        {
            return DAL_Folder.Add(folder);
        }
        public static void Update(long id, Folder folder)
        {
            DAL_Folder.Update(id, folder);
        }
        public static void Delete(long id)
        {
            DAL_Folder.Delete(id);
        }
        public static Folder SelectById(long id)
        {
            return DAL_Folder.SelectById(id);
        }
        public static List<Folder> SelectAll()
        {
            return DAL_Folder.SelectAll();
        }

        public static List<Mail> SelectMails(long id)
        {
            return DAL_Folder.SelectMails(id);
        }
    }
}
