using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using GEC_DataLayer.Models.Entities;
using GEC_DataLayer.Models.Enumerations.Contact;
using MyUtilities;

namespace GEC_DataLayer.Models.DAL
{
    public class DAL_Group
    {
        private static bool CheckEntityUnicity(long EntityKey)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM [Group] WHERE Id = @EntityKey";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@EntityKey", SqlDbType.BigInt).Value = EntityKey;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return true;
            else
                return false;
        }

        private static bool CheckEntityUnicity(string EntityKey)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM [Group] WHERE Name = @EntityKey";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@EntityKey", SqlDbType.NVarChar).Value = EntityKey;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return true;
            else
                return false;
        }

        private static Group GetEntityFromDataRow(DataRow dataRow)
        {
            Group group = new Group();
            group.Id = (long)dataRow["Id"];
            group.Name = dataRow["Name"].ToString();
            group.Description = dataRow["Description"] == DBNull.Value ? null : dataRow["Description"].ToString();

            return group;
        }

        private static List<Group> GetListFromDataTable(DataTable dataTable)
        {
            if (dataTable != null)
            {
                List<Group> list = new List<Group>(dataTable.Rows.Count);
                foreach (DataRow dataRow in dataTable.Rows)
                    list.Add(GetEntityFromDataRow(dataRow));
                return list;
            }
            else
                return null;
        }

        public static bool CheckNameUnicity(string name)
        {
            return CheckEntityUnicity(name);
        }

        public static bool CheckIfContactInGroup(long contactId, long groupId)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM ContactsGroups WHERE IdContact = @IdContact AND IdGroup = @IdGroup";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@IdContact", SqlDbType.VarChar).Value = contactId;
                command.Parameters.Add("@IdGroup", SqlDbType.VarChar).Value = groupId;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return false;
            else
                return true;
        }

        public static long Add(Group group)
        {
            SqlCommand command = new SqlCommand("createGroup", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = group.Name;
            command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(group.Description) ? group.Description : SqlString.Null;

            return Convert.ToInt64(DataBaseAccessUtilities.ScalarRequest(command));
        }

        public static void Update(long id, Group group)
        {
            SqlCommand command = new SqlCommand("updateGroup", DBConnection.GetConnection());
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = group.Name;
            command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(group.Description) ? group.Description : SqlString.Null;
            command.CommandType = CommandType.StoredProcedure;

            DataBaseAccessUtilities.NonQueryRequest(command);
        }

        public static void Delete(long id)
        {
            SqlCommand command = new SqlCommand("deleteGroup", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
            DataBaseAccessUtilities.NonQueryRequest(command);
        }

        public static Group SelectById(long id)
        {
            Group group;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectByIdGroup", connection);
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        group = new Group();
                        group.Id = dataReader.GetInt64(0);
                        group.Name = dataReader.GetString(1);
                        group.Description = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);
                        return group;
                    }
                    return null;
                }
                catch (SqlException e)
                {
                    throw new MyException(e, "Database Error", e.Message, "DAL");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static List<Group> SelectAll()
        {
            List<Group> groups = new List<Group>();
            Group group;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectAllGroup", connection);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader != null)
                    {
                        while (dataReader.Read())
                        {
                            group = new Group();
                            group.Id = dataReader.GetInt64(0);
                            group.Name = dataReader.GetString(1);
                            group.Description = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);

                            groups.Add(group);
                        }
                    }
                    return groups;
                }
                catch (SqlException e)
                {
                    throw new MyException(e, "Database Error", e.Message, "DAL");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static void AddMember(long contactId, long groupId)
        {
            SqlCommand command = new SqlCommand("addMemberToGroup", DBConnection.GetConnection());
            command.Parameters.Add("@IdContact", SqlDbType.BigInt).Value = contactId;
            command.Parameters.Add("@IdGroup", SqlDbType.BigInt).Value = groupId;
            command.CommandType = CommandType.StoredProcedure;

            DataBaseAccessUtilities.NonQueryRequest(command);
        }

        public static void RemoveMember(long contactId, long groupId)
        {
            SqlCommand command = new SqlCommand("removeMemberFromGroup", DBConnection.GetConnection());
            command.Parameters.Add("@IdContact", SqlDbType.BigInt).Value = contactId;
            command.Parameters.Add("@IdGroup", SqlDbType.BigInt).Value = groupId;
            command.CommandType = CommandType.StoredProcedure;

            DataBaseAccessUtilities.NonQueryRequest(command);
        }

        public static List<Contact> SelectMembers(long id)
        {
            List<Contact> contacts = new List<Contact>();
            Contact contact;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectGroupMembers", connection);
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader != null)
                    {
                        while (dataReader.Read())
                        {
                            contact = new Contact();
                            contact.Id = dataReader.GetInt64(0);
                            contact.Nature = (Nature)Enum.Parse(typeof(Nature), dataReader.GetString(1), true);
                            contact.ContactType = (ContactType)Enum.Parse(typeof(ContactType), dataReader.GetString(2), true);
                            contact.Name = dataReader.GetString(3);
                            contact.Email1 = dataReader.IsDBNull(4) ? null : dataReader.GetString(4);
                            contact.Email2 = dataReader.IsDBNull(5) ? null : dataReader.GetString(5);
                            contact.Telephone1 = dataReader.IsDBNull(6) ? null : dataReader.GetString(6);
                            contact.Telephone2 = dataReader.IsDBNull(7) ? null : dataReader.GetString(7);
                            contact.Fax = dataReader.IsDBNull(8) ? null : dataReader.GetString(8);
                            contact.Address = dataReader.IsDBNull(9) ? null : dataReader.GetString(9);

                            contacts.Add(contact);
                        }
                    }
                    return contacts;
                }
                catch (SqlException e)
                {
                    throw new MyException(e, "Database Error", e.Message, "DAL");
                }
                finally
                {
                    connection.Close();
                }
            }
        } 
    }
}
