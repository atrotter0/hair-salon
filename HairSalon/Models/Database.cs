using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class DB
    {
        public static Dictionary<string, object> SqlHelper { get; set; }

        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }

        public static void CreateConnectionAndCommand()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            SqlHelper.Add("conn", conn);
            SqlHelper.Add("cmd", cmd);
            Console.WriteLine(SqlHelper["conn"]);
        }

        public static void CloseAndCheckConnection()
        {
            // SqlHelper["conn"].Close();
            // Console.WriteLine(SqlHelper["conn"]);

            // if (SqlHelper["conn"] != null)
            // {
            //     SqlHelper["conn"].Dispose();
            // }
        }
    }
}
