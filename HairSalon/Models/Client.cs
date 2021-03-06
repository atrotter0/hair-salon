using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
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
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
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
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
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
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO Clients (name, phone, email, stylist_id) VALUES (@ClientName, @ClientPhone, @ClientEmail, @StylistId);";
            cmd.Parameters.AddWithValue("@ClientName", this.Name);
            cmd.Parameters.AddWithValue("@ClientPhone", this.Phone);
            cmd.Parameters.AddWithValue("@ClientEmail", this.Email);
            cmd.Parameters.AddWithValue("@StylistId", this.StylistId);
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
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
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
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
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
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = @searchId;";
            cmd.Parameters.AddWithValue("@searchId", id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
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
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
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
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = " + this.StylistId + ";";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
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
