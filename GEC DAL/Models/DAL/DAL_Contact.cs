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
    public class DAL_Contact
    {
        private static bool CheckEntityUnicity(long EntityKey)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Contact WHERE Id = @EntityKey";
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
                string query = "SELECT COUNT(*) FROM Contact WHERE Name = @EntityKey";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@EntityKey", SqlDbType.NVarChar).Value = EntityKey;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return true;
            else
                return false;
        }

        private static Contact GetEntityFromDataRow(DataRow dataRow)
        {
            Contact contact = new Contact();
            contact.Id = (long)dataRow["Id"];
            contact.Nature = (Nature)Enum.Parse(typeof(Nature), dataRow["Nature"].ToString(), true);
            contact.ContactType = (ContactType)Enum.Parse(typeof(ContactType), dataRow["Type"].ToString(), true);
            contact.Name = dataRow["Name"].ToString();
            contact.Email1 = dataRow["Email1"].ToString();
            contact.Email2 = dataRow["Email2"].ToString();
            contact.Telephone1 = dataRow["Telephone1"].ToString();
            contact.Telephone2 = dataRow["Telephone2"].ToString();
            contact.Fax = dataRow["Fax"].ToString();
            contact.Address = dataRow["Adress"].ToString();

            return contact;
        }

        private static List<Contact> GetListFromDataTable(DataTable dataTable)
        {
            if (dataTable != null)
            {
                List<Contact> list = new List<Contact>(dataTable.Rows.Count);
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

        public static long Add(Contact contact)
        {
            if (!CheckEntityUnicity(contact.Name))
            {
                throw new MyException("Database Error", "Contact name must be unique.", "DAL");
            }
            SqlCommand command = new SqlCommand("createContact", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Type", SqlDbType.VarChar).Value = contact.ContactType;
            command.Parameters.Add("@Nature", SqlDbType.VarChar).Value = contact.Nature;
            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = contact.Name;
            command.Parameters.Add("@Email1", SqlDbType.VarChar).Value = !string.IsNullOrWhiteSpace(contact.Email1) ? contact.Email1 : SqlString.Null;
            command.Parameters.Add("@Email2", SqlDbType.VarChar).Value = !string.IsNullOrWhiteSpace(contact.Email2) ? contact.Email2 : SqlString.Null;
            command.Parameters.Add("@Telephone1", SqlDbType.VarChar).Value = !string.IsNullOrWhiteSpace(contact.Telephone1) ? contact.Telephone1 : SqlString.Null;
            command.Parameters.Add("@Telephone2", SqlDbType.VarChar).Value = !string.IsNullOrWhiteSpace(contact.Telephone2) ? contact.Telephone2 : SqlString.Null;
            command.Parameters.Add("@Fax", SqlDbType.VarChar).Value = !string.IsNullOrWhiteSpace(contact.Fax) ? contact.Fax : SqlString.Null;
            command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(contact.Address) ? contact.Address : SqlString.Null;

            return Convert.ToInt64(DataBaseAccessUtilities.ScalarRequest(command));
        }

        public static void Update(long id, Contact contact)
        {
            SqlCommand command = new SqlCommand("updateContact", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
            command.Parameters.Add("@Nature", SqlDbType.VarChar).Value = contact.Nature;
            command.Parameters.Add("@Type", SqlDbType.VarChar).Value = contact.ContactType;
            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = contact.Name;
            command.Parameters.Add("@Email1", SqlDbType.VarChar).Value = !string.IsNullOrWhiteSpace(contact.Email1) ? contact.Email1 : SqlString.Null;
            command.Parameters.Add("@Email2", SqlDbType.VarChar).Value = !string.IsNullOrWhiteSpace(contact.Email2) ? contact.Email2 : SqlString.Null;
            command.Parameters.Add("@Telephone1", SqlDbType.VarChar).Value = !string.IsNullOrWhiteSpace(contact.Telephone1) ? contact.Telephone1 : SqlString.Null;
            command.Parameters.Add("@Telephone2", SqlDbType.VarChar).Value = !string.IsNullOrWhiteSpace(contact.Telephone2) ? contact.Telephone2 : SqlString.Null;
            command.Parameters.Add("@Fax", SqlDbType.VarChar).Value = !string.IsNullOrWhiteSpace(contact.Fax) ? contact.Fax : SqlString.Null;
            command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(contact.Address) ? contact.Address : SqlString.Null;

            DataBaseAccessUtilities.NonQueryRequest(command);
        }

        public static void Delete(long id)
        {
            SqlCommand command = new SqlCommand("deleteContact", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
            DataBaseAccessUtilities.NonQueryRequest(command);
        }

        public static Contact SelectById(long id)
        {
            Contact contact;

            using(SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectByIdContact", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
                try
                {
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
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
                        return contact;
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

        public static List<Contact> SelectAll()
        {
            List<Contact> contacts = new List<Contact>();
            Contact contact;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectAllContact", connection);
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

        public static List<Contact> DisconnectedSelectAll()
        {
            DataTable dataTable;
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectAllContact", connection);
                command.CommandType = CommandType.StoredProcedure;
                dataTable = DataBaseAccessUtilities.SelectRequest(command);
            }
            return GetListFromDataTable(dataTable);
        }

        public static List<Group> SelectGroups(long id)
        {
            List<Group> groups = new List<Group>();
            Group group;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectContactGroups", connection);
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
    }
}
