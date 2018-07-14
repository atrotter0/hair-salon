using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Stylist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Stylist (string name, int id = 0)
        {
            this.Name = name;
            this.Id = id;
        }

        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist stylist = (Stylist) otherStylist;
                bool nameEquality = (this.Name == stylist.Name);
                return (nameEquality);
            }
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist>() {};
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            var rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                int StylistId = rdr.GetInt32(0);
                string StylistName = rdr.GetString(1);
                Stylist newStylist = new Stylist(StylistName);
                newStylist.Id = StylistId;
                allStylists.Add(newStylist);
            }
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

        public static void DeleteAll()
        {
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();
            conn.Close();

            if (conn != null)
            {
               conn.Dispose();
            }
        }

        public void Save()
        {
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@StylistName);";
            cmd.Parameters.AddWithValue("@StylistName", this.Name);
            cmd.ExecuteNonQuery();
            this.Id = (int) cmd.LastInsertedId;
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update()
        {
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @StylistName WHERE id = @StylistId;";
            cmd.Parameters.AddWithValue("@StylistName", this.Name);
            cmd.Parameters.AddWithValue("@StylistId", this.Id);
            cmd.ExecuteNonQuery();
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"DELETE FROM stylists WHERE id = @StylistId;";
            cmd.Parameters.AddWithValue("@StylistId", this.Id);
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"UPDATE clients SET stylist_Id = 0 WHERE stylist_id = @StylistId;";
            cmd.ExecuteNonQuery();
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Stylist Find(int id)
        {
            NpgsqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = @searchId;";
            cmd.Parameters.AddWithValue("@searchId", id);
            var rdr = cmd.ExecuteReader();
            int stylistId = 0;
            string stylistName = "";

            while (rdr.Read())
            {
                 stylistId = rdr.GetInt32(0);
                 stylistName = rdr.GetString(1);
            }
            Stylist foundStylist = new Stylist(stylistName, stylistId);
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return foundStylist;
        }


        public List<Client> GetClientsForStylist()
        {
            List<Client> clientsForStylist = new List<Client>() {};
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = " + this.Id + ";";
            var rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string phone = rdr.GetString(2);
                string email = rdr.GetString(3);
                int stylistId = rdr.GetInt32(4);
                Client newClient = new Client(name, phone, email, stylistId, id);
                clientsForStylist.Add(newClient);
            }
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return clientsForStylist;
        }
    }
}
