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
    }
}
