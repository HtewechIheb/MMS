using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;

public static class DBConnection
{
    static string DbCnnStr = @"server=DESKTOP-3NHQM24\SQLEXPRESS;Database=GEC DB;Integrated Security=SSPI;";
        	
    public static string GetConnectionString()
    {
        return DbCnnStr;
    }

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(DbCnnStr);
    }
}