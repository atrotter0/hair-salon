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
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
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
    }
}
