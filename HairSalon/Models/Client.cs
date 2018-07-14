using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Npgsql;
using HairSalon;

namespace HairSalon.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int StylistId { get; set; }

        public Client (string name, string phone, string email, int stylistId, int id = 0)
        {
            this.Name = name;
            this.Phone = phone;
            this.Email = email;
            this.StylistId = stylistId;
            this.Id = id;
        }

        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client Client = (Client) otherClient;
                bool nameEquality = (this.Name == Client.Name);
                bool phoneEquality = (this.Name == Client.Name);
                bool emailEquality = (this.Name == Client.Name);
                return (nameEquality && phoneEquality && emailEquality);
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client>() {};
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            var rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string phone = rdr.GetString(2);
                string email = rdr.GetString(3);
                int stylistId = rdr.GetInt32(4);
                Client newClient = new Client(name, phone, email, stylistId, id);
                allClients.Add(newClient);
            }
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static void DeleteAll()
        {
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();
            conn.Close();

            if (conn != null)
            {
               conn.Dispose();
            }
        }

        public void Save()
        {
            int clientId = 0;
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"INSERT INTO Clients (name, phone, email, stylist_id) VALUES (@ClientName, @ClientPhone, @ClientEmail, @StylistId);";
            cmd.Parameters.AddWithValue("@ClientName", this.Name);
            cmd.Parameters.AddWithValue("@ClientPhone", this.Phone);
            cmd.Parameters.AddWithValue("@ClientEmail", this.Email);
            cmd.Parameters.AddWithValue("@StylistId", this.StylistId);
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"SELECT * FROM clients ORDER BY id DESC LIMIT 1;";
            var rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                clientId = rdr.GetInt32(0);
            }
            this.Id = (int) clientId;
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
            cmd.CommandText = @"UPDATE clients SET name = @ClientName, phone = @ClientPhone, email = @ClientEmail, stylist_id = @StylistId WHERE id = @ClientId;";
            cmd.Parameters.AddWithValue("@ClientName", this.Name);
            cmd.Parameters.AddWithValue("@ClientPhone", this.Phone);
            cmd.Parameters.AddWithValue("@ClientEmail", this.Email);
            cmd.Parameters.AddWithValue("@StylistId", this.StylistId);
            cmd.Parameters.AddWithValue("@ClientId", this.Id);
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
            cmd.CommandText = @"DELETE FROM clients WHERE id = @ClientId;";
            cmd.Parameters.AddWithValue("@ClientId", this.Id);
            cmd.ExecuteNonQuery();
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Client Find(int id)
        {
            NpgsqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = @searchId;";
            cmd.Parameters.AddWithValue("@searchId", id);
            var rdr = cmd.ExecuteReader();
            int clientId = 0;
            string name = "";
            string phone = "";
            string email = "";
            int stylistId = 0;

            while (rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                name = rdr.GetString(1);
                phone = rdr.GetString(2);
                email = rdr.GetString(3);
                stylistId = rdr.GetInt32(4);
            }
            Client foundClient = new Client(name, phone, email, stylistId, clientId);
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return foundClient;
        }

        public List<Stylist> GetAllStylists()
        {
            List<Stylist> allStylists = new List<Stylist>() {};
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            var rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                Stylist newStylist = new Stylist(stylistName);
                newStylist.Id = stylistId;
                allStylists.Add(newStylist);
            }
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }


        public Stylist GetStylist()
        {
            Stylist stylist = new Stylist("");
            NpgsqlConnection conn = DB.Connection();
            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand() as NpgsqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = " + this.StylistId + ";";
            var rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                stylist.Name = stylistName;
                stylist.Id = stylistId;
            }
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return stylist;
        }
    }
}
