using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using GEC_DataLayer.Models.Entities;
using GEC_DataLayer.Models.Enumerations.Mail;
using MyUtilities;

namespace GEC_DataLayer.Models.DAL
{
    public class DAL_Folder
    {
        private static bool CheckEntityUnicity(long EntityKey)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Folder WHERE Id = @EntityKey";
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
                string query = "SELECT COUNT(*) FROM Folder WHERE Name = @EntityKey";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@EntityKey", SqlDbType.NVarChar).Value = EntityKey;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return true;
            else
                return false;
        }

        private static Folder GetEntityFromDataRow(DataRow dataRow)
        {
            Folder folder = new Folder();
            folder.Id = (long)dataRow["Id"];
            folder.Name = dataRow["Name"].ToString();
            folder.Description = dataRow["Description"] == DBNull.Value ? null : dataRow["Description"].ToString();

            return folder;
        }

        private static List<Folder> GetListFromDataTable(DataTable dataTable)
        {
            if (dataTable != null)
            {
                List<Folder> list = new List<Folder>(dataTable.Rows.Count);
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

        public static long Add(Folder folder)
        {
            SqlCommand command = new SqlCommand("createFolder", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = folder.Name;
            command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(folder.Description) ? folder.Description : SqlString.Null;

            return Convert.ToInt64(DataBaseAccessUtilities.ScalarRequest(command));
        }

        public static void Update(long id, Folder folder)
        {
            SqlCommand command = new SqlCommand("updateFolder", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = folder.Name;
            command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(folder.Description) ? folder.Description : SqlString.Null;

            DataBaseAccessUtilities.NonQueryRequest(command);
        }

        public static void Delete(long id)
        {
            SqlCommand command = new SqlCommand("deleteFolder", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

            DataBaseAccessUtilities.NonQueryRequest(command);
        }

        public static Folder SelectById(long id)
        {
            Folder folder;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectByIdFolder", connection);
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        folder = new Folder();
                        folder.Id = dataReader.GetInt64(0);
                        folder.Name = dataReader.GetString(1);
                        folder.Description = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);

                        return folder;
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

        public static List<Folder> SelectAll()
        {
            List<Folder> folders = new List<Folder>();
            Folder folder;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectAllFolder", connection);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader != null)
                    {
                        while (dataReader.Read())
                        {
                            folder = new Folder();
                            folder.Id = dataReader.GetInt64(0);
                            folder.Name = dataReader.GetString(1);
                            folder.Description = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);

                            folders.Add(folder);
                        }
                    }
                    return folders;
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

        public static List<Mail> SelectMails(long id)
        {
            List<Mail> mails = new List<Mail>();
            Mail mail;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectFolderMails", connection);
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
                            mail = new Mail();
                            mail.Id = dataReader.GetInt64(0);
                            mail.MailType = (MailType)Enum.Parse(typeof(MailType), dataReader.GetString(1), true);
                            mail.Channel = (Channel)Enum.Parse(typeof(Channel), dataReader.GetString(2), true);
                            mail.RegistrationDate = dataReader.GetDateTime(3);
                            mail.RegistrationNumber = dataReader.GetString(4);
                            mail.IdSender = dataReader.GetInt64(5);
                            mail.SenderRegistrationNumber = dataReader.IsDBNull(6) ? null : dataReader.GetString(6);
                            mail.SendingDate = dataReader.IsDBNull(7) ? null : (DateTime?)dataReader.GetDateTime(7);
                            mail.IdRecipient = dataReader.GetInt64(8);
                            mail.ProcessingTimeFrame = dataReader.IsDBNull(9) ? null : (ProcessingTimeFrame?)Enum.Parse(typeof(ProcessingTimeFrame), dataReader.GetString(9), true);
                            mail.Confidentiality = dataReader.IsDBNull(10) ? null : (Confidentiality?)Enum.Parse(typeof(Confidentiality), dataReader.GetString(10), true);
                            mail.Object = dataReader.GetString(11);
                            mail.Content = dataReader.IsDBNull(12) ? null : dataReader.GetString(12);
                            mail.DigitizedFile = dataReader.IsDBNull(13) ? null : dataReader.GetString(13);
                            mail.Language = dataReader.IsDBNull(14) ? null : (Language?)Enum.Parse(typeof(Language), dataReader.GetString(14), true);
                            mail.KeyWords = dataReader.IsDBNull(15) ? null : dataReader.GetString(15);
                            mail.Observations = dataReader.IsDBNull(16) ? null : dataReader.GetString(16);
                            mail.IdFolder = dataReader.IsDBNull(17) ? null : (long?)dataReader.GetInt64(17);
                            mail.HasHardCopy = dataReader.GetBoolean(18);

                            mails.Add(mail);
                        }
                    }
                    return mails;
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

