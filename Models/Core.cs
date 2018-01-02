using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Newtonsoft.Json;

namespace UberEatsApp.Models
{
    public class Core
    {
        //Login
        public static async Task<Customer> GetCusts(string Email, string Password)
        {
            string url = @"http://10.0.2.2:8080/api/CustomerLogginIn?Email=" + Email + "&Password=" + Password + ";";

            dynamic results = await DataAccess.getCustomerData(url).ConfigureAwait(false);

            if (results["Customer"] != null)
            {
                Customer cust = new Customer();
                cust.Customer_Id = (Int32)results["Customer_Id"];
                cust.Firstname = (string)results["Firstname"];
                cust.Lastname = (string)results["Lastname"];
                cust.Contact = (string)results["Contatct"];
                cust.Email = (string)results["Email"];
                cust.Password = (string)results["Password"];
                return cust;
            }
            return null;
        }

        //Update customer's profile
        public static async Task<Customer> Update(Customer custo)
        {
            string url = @"http://10.0.2.2:8080/api/UpdateCustomer?Customer_Id=" + custo.Customer_Id + ";";

            dynamic results = await DataAccess.GetCustomer(url).ConfigureAwait(false);

            if (results["Customer"] != null)
            {
                Customer cust = new Customer();
                cust.Customer_Id = (Int32)results["Customer_Id"];
                cust.Firstname = (string)results["Firstname"];
                cust.Lastname = (string)results["Lastname"];
                cust.Contact = (string)results["Contatct"];
                cust.Email = (string)results["Email"];
                cust.Password = (string)results["Password"];
                return cust;
            }
            return null;
        }

        public Core()
        {
        }
    }
}
