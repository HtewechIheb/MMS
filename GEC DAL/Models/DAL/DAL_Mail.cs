using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data;
using GEC_DataLayer.Models.Entities;
using GEC_DataLayer.Models.Enumerations.Mail;
using MyUtilities;

namespace GEC_DataLayer.Models.DAL
{
    public class DAL_Mail
    {
        private static bool CheckEntityUnicity(long EntityKey)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Mail WHERE Id = @EntityKey";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@EntityKey", SqlDbType.BigInt).Value = EntityKey;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return true;
            else
                return false;
        }

        private static Mail GetEntityFromDataRow(DataRow dataRow)
        {
            Mail mail = new Mail();
            mail.Id = (long)dataRow["Id"];
            mail.MailType = (MailType)Enum.Parse(typeof(MailType), dataRow["MailType"].ToString(), true);
            mail.Channel = (Channel)Enum.Parse(typeof(Channel), dataRow["Channel"].ToString(), true);
            mail.RegistrationDate = (DateTime)dataRow["RegistrationDate"];
            mail.RegistrationNumber = dataRow["RegistrationNumber"].ToString();
            mail.IdSender = (long)dataRow["IdSender"];
            mail.SenderRegistrationNumber = dataRow["SenderRegistrationNumber"].ToString();
            mail.SendingDate = dataRow["SendingDate"] == DBNull.Value ? null : (DateTime?)dataRow["SendingDate"];
            mail.IdRecipient = (long)dataRow["IdRecipient"];
            mail.ProcessingTimeFrame = dataRow["ProcessingTimeFrame"] == DBNull.Value ? null : (ProcessingTimeFrame?)Enum.Parse(typeof(ProcessingTimeFrame), dataRow["ProcessingTimeFrame"].ToString(), true);
            mail.Confidentiality = dataRow["Confidentiality"] == DBNull.Value ? null : (Confidentiality?)Enum.Parse(typeof(Confidentiality), dataRow["Confidentiality"].ToString(), true);
            mail.Object = dataRow["Object"].ToString();
            mail.Content = dataRow["Content"].ToString();
            mail.DigitizedFile = dataRow["DigitizedFile"].ToString();
            mail.Language = dataRow["Language"] == DBNull.Value ? null : (Language?)Enum.Parse(typeof(Language), dataRow["Language"].ToString(), true);
            mail.KeyWords = dataRow["KeyWords"].ToString();
            mail.Observations = dataRow["Observations"].ToString();
            mail.IdFolder = dataRow["IdFolder"] == DBNull.Value ? null : (long?)dataRow["IdFolder"];
            mail.HasHardCopy = (bool)dataRow["HasHardCopy"];

            return mail;
        }

        private static List<Mail> GetListFromDataTable(DataTable dataTable)
        {
            if (dataTable != null)
            {
                List<Mail> list = new List<Mail>(dataTable.Rows.Count);
                foreach (DataRow dataRow in dataTable.Rows)
                    list.Add(GetEntityFromDataRow(dataRow));
                return list;
            }
            else
                return null;
        }

        public static long Add(Mail mail)
        {
            SqlCommand command = new SqlCommand("createMail", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@MailType", SqlDbType.VarChar).Value = mail.MailType;
            command.Parameters.Add("@Channel", SqlDbType.VarChar).Value = mail.Channel;
            command.Parameters.Add("@RegistrationDate", SqlDbType.Date).Value = mail.RegistrationDate;
            command.Parameters.Add("@RegistrationNumber", SqlDbType.VarChar).Value = mail.RegistrationNumber;
            command.Parameters.Add("@IdSender", SqlDbType.BigInt).Value = mail.IdSender;
            command.Parameters.Add("@SenderRegistrationNumber", SqlDbType.VarChar).Value = mail.SenderRegistrationNumber ?? SqlString.Null;
            command.Parameters.Add("@SendingDate", SqlDbType.Date).Value = mail.SendingDate.HasValue ? (object)mail.SendingDate.Value : SqlDateTime.Null;
            command.Parameters.Add("@IdRecipient", SqlDbType.BigInt).Value = mail.IdRecipient;
            command.Parameters.Add("@ProcessingTimeFrame", SqlDbType.VarChar).Value = mail.ProcessingTimeFrame.HasValue ? (object)mail.ProcessingTimeFrame.Value : SqlString.Null;
            command.Parameters.Add("@Confidentiality", SqlDbType.VarChar).Value = mail.Confidentiality.HasValue ? (object)mail.Confidentiality.Value : SqlString.Null;
            command.Parameters.Add("@Object", SqlDbType.NVarChar).Value = mail.Object;
            command.Parameters.Add("@Content", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(mail.Content) ? mail.Content : SqlString.Null;
            command.Parameters.Add("@DigitizedFile", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(mail.DigitizedFile) ? mail.DigitizedFile : SqlString.Null;
            command.Parameters.Add("@Language", SqlDbType.VarChar).Value = mail.Language.HasValue ? (object)mail.Language.Value : SqlString.Null;
            command.Parameters.Add("@KeyWords", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(mail.KeyWords) ? mail.KeyWords : SqlString.Null;
            command.Parameters.Add("@Observations", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(mail.Observations) ? mail.Observations : SqlString.Null;
            command.Parameters.Add("@IdFolder", SqlDbType.BigInt).Value = mail.IdFolder.HasValue ? mail.IdFolder.Value : SqlInt64.Null;
            command.Parameters.Add("@HasHardCopy", SqlDbType.Bit).Value = mail.HasHardCopy;

            return Convert.ToInt64(DataBaseAccessUtilities.ScalarRequest(command));
        }

        public static void Update(long id, Mail updatedMail)
        {
            SqlCommand command = new SqlCommand("updateMail", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;            

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
            command.Parameters.Add("@MailType", SqlDbType.VarChar).Value = updatedMail.MailType;
            command.Parameters.Add("@Channel", SqlDbType.VarChar).Value = updatedMail.Channel;
            command.Parameters.Add("@RegistrationDate", SqlDbType.Date).Value = updatedMail.RegistrationDate;
            command.Parameters.Add("@RegistrationNumber", SqlDbType.VarChar).Value = updatedMail.RegistrationNumber;
            command.Parameters.Add("@IdSender", SqlDbType.BigInt).Value = updatedMail.IdSender;
            command.Parameters.Add("@SenderRegistrationNumber", SqlDbType.VarChar).Value = updatedMail.SenderRegistrationNumber ?? SqlString.Null;
            command.Parameters.Add("@SendingDate", SqlDbType.Date).Value = updatedMail.SendingDate.HasValue ? (object)updatedMail.SendingDate.Value : SqlDateTime.Null;
            command.Parameters.Add("@IdRecipient", SqlDbType.BigInt).Value = updatedMail.IdRecipient;
            command.Parameters.Add("@ProcessingTimeFrame", SqlDbType.VarChar).Value = updatedMail.ProcessingTimeFrame.HasValue ? (object)updatedMail.ProcessingTimeFrame.Value : SqlString.Null;
            command.Parameters.Add("@Confidentiality", SqlDbType.VarChar).Value = updatedMail.Confidentiality.HasValue ? (object)updatedMail.Confidentiality.Value : SqlString.Null;
            command.Parameters.Add("@Object", SqlDbType.NVarChar).Value = updatedMail.Object;
            command.Parameters.Add("@Content", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(updatedMail.Content) ? updatedMail.Content : SqlString.Null;
            command.Parameters.Add("@DigitizedFile", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(updatedMail.DigitizedFile) ? updatedMail.DigitizedFile : SqlString.Null;
            command.Parameters.Add("@Language", SqlDbType.VarChar).Value = updatedMail.Language.HasValue ? (object)updatedMail.Language.Value : SqlString.Null;
            command.Parameters.Add("@KeyWords", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(updatedMail.KeyWords) ? updatedMail.KeyWords : SqlString.Null;
            command.Parameters.Add("@Observations", SqlDbType.NVarChar).Value = !string.IsNullOrWhiteSpace(updatedMail.Observations) ? updatedMail.Observations : SqlString.Null;
            command.Parameters.Add("@IdFolder", SqlDbType.BigInt).Value = updatedMail.IdFolder.HasValue ? updatedMail.IdFolder.Value : SqlInt64.Null;
            command.Parameters.Add("@HasHardCopy", SqlDbType.Bit).Value = updatedMail.HasHardCopy;

            DataBaseAccessUtilities.NonQueryRequest(command);
        }

        public static void Delete(long id)
        {
            SqlCommand command = new SqlCommand("deleteMail", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
            DataBaseAccessUtilities.NonQueryRequest(command);
        }

        public static Mail SelectById(long id)
        {
            Mail mail;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectByIdMail", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;                
                try
                {
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
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
                        return mail;
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

        public static List<Mail> SelectByType(string type)
        {
            List<Mail> mails = new List<Mail>();
            Mail mail;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectByTypeMail", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@MailType", SqlDbType.VarChar).Value = type;                
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

        public static List<Mail> SelectAll()
        {
            List<Mail> mails = new List<Mail>();
            Mail mail;

            using(SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectAllMail", connection);
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

        public static List<Mail> SelectByContactId(long id)
        {
            List<Mail> mails = new List<Mail>();
            Mail mail;

            using(SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectByContactIdMail", connection);
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if(dataReader != null)
                    {
                        while(dataReader.Read())
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

        public static List<Mail> DisconnectedSelectAll()
        {
            DataTable dataTable;
            using(SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("selectAllMail", connection);
                command.CommandType = CommandType.StoredProcedure;
                dataTable = DataBaseAccessUtilities.SelectRequest(command);
            }
            return GetListFromDataTable(dataTable);
        }

        public static List<Mail> Search(string queryString, string dayString = null, string monthString = null, string yearString = null)
        {
            List<Mail> mails = new List<Mail>();
            Mail mail;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand("searchMail", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@QueryString", SqlDbType.NVarChar).Value = queryString;
                command.Parameters.Add("@QueryDay", SqlDbType.Int).Value = !string.IsNullOrWhiteSpace(dayString) ? Convert.ToInt32(dayString): SqlInt32.Null;
                command.Parameters.Add("@QueryMonth", SqlDbType.Int).Value = !string.IsNullOrWhiteSpace(monthString) ? Convert.ToInt32(monthString) : SqlInt32.Null;
                command.Parameters.Add("@QueryYear", SqlDbType.Int).Value = !string.IsNullOrWhiteSpace(yearString) ? Convert.ToInt32(yearString) : SqlInt32.Null;
                command.Parameters.Add("@QueryBigInt", SqlDbType.BigInt).Value = long.TryParse(queryString, out long queryBigInt) ? queryBigInt : SqlInt64.Null;
                                
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
        
        public static long GetMailNumber()
        {
            SqlCommand command = new SqlCommand("GetNumberMail", DBConnection.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            return Convert.ToInt64(DataBaseAccessUtilities.ScalarRequest(command));
        }
    }    
}
