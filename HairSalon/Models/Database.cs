using System;
using MySql.Data.MySqlClient;
using Npgsql;
using HairSalon;

namespace HairSalon.Models
{
    public class DB
    {
        public static NpgsqlConnection Connection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
