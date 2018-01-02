using System;
namespace UberEatsApp.Models
{
    public class Customer
    {
        public int Customer_Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Customer()
        {

        }

        public Customer(int id, string firstname, string lastname, string contact, string email, string password)
        {
            Customer_Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Contact = contact;
            Email = email;
            Password = password;
        }

        public Customer(string firstname, string lastname, string contact)
        {
            Firstname = firstname;
            Lastname = lastname;
            Contact = contact;
        }

        public Customer(string firstname, string lastname, string contact, string credit_card, string email, string password)
        {
            Firstname = firstname;
            Lastname = lastname;
            Contact = contact;
            Email = email;
            Password = password;
        }
    }
}
