using System;
using System.Collections.Generic;
using System.Text;
using GEC_DataLayer.Models.DAL;
using GEC_DataLayer.Models.Entities;

namespace GEC_DataLayer.Models.BLL
{
    public class BLL_Group
    {
        public static bool CheckNameUnicity(string name)
        {
            return DAL_Group.CheckNameUnicity(name);
        }
        public static bool CheckIfContactInGroup(long contactId, long groupId)
        {
            return DAL_Group.CheckIfContactInGroup(contactId, groupId);
        }
        public static long Add(Group group)
        {
            return DAL_Group.Add(group);
        }
        public static void Update(long id, Group group)
        {
            DAL_Group.Update(id, group);
        }
        public static void Delete(long id)
        {
            DAL_Group.Delete(id);
        }
        public static Group SelectById(long id)
        {
            return DAL_Group.SelectById(id);
        }
        public static List<Group> SelectAll()
        {
            return DAL_Group.SelectAll();
        }
        public static void AddMember(long contactId, long groupId)
        {
            DAL_Group.AddMember(contactId, groupId);
        }
        public static void ManageMembers(long[] contactIds, long groupId)
        {
            List<Contact> members = DAL_Group.SelectMembers(groupId);
            if (contactIds == null)
            {
                foreach(Contact member in members)
                {
                    DAL_Group.RemoveMember(member.Id.Value, groupId);
                }
            }
            else
            {
                foreach (long contactId in contactIds)
                {
                    if (!members.Exists(member => member.Id.Value == contactId))
                    {
                        DAL_Group.AddMember(contactId, groupId);
                    }
                }

                foreach (Contact member in members)
                {
                    if (!Array.Exists(contactIds, id => id == member.Id.Value))
                    {
                        DAL_Group.RemoveMember(member.Id.Value, groupId);
                    }
                }
            }
        }
        public static void RemoveMember(long contactId, long groupId)
        {
            DAL_Group.RemoveMember(contactId, groupId);
        }
        public static List<Contact> SelectMembers(long id)
        {
            return DAL_Group.SelectMembers(id);
        }
    }
}
