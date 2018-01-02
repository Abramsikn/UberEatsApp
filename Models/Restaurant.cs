using System;
namespace UberEatsApp.Models
{
    public class Restaurant
    {
        public int Rest_Id { get; set; }
        public string Rest_Name { get; set; }
        public string Rest_Type { get; set; }
        public string Rest_Desc { get; set; }
        public string Rest_Address { get; set; }
        public string Rest_City { get; set; }
        public string Rest_Contact { get; set; }
        public byte[] Image { get; set; }

        public Restaurant(string name, string type, string desc, string address, string city, string contact, byte[] image)
        {
            Rest_Name = name;
            Rest_Type = type;
            Rest_Desc = desc;
            Rest_Address = address;
            Rest_City = city;
            Rest_Contact = contact;
            Image = image;
        }
        public Restaurant(int id, string name, string type, string desc, string address, string city, string contact, byte[] image)
        {
            Rest_Id = id;
            Rest_Name = name;
            Rest_Type = type;
            Rest_Desc = desc;
            Rest_Address = address;
            Rest_City = city;
            Rest_Contact = contact;
            Image = image;
        }

        public Restaurant()
        {
        }
    }
}
