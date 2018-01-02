using System;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Newtonsoft.Json;

namespace UberEatsApp.Models
{
    public class service
    {
        HttpClient client = new HttpClient();
        //Customer customer = null;

        public Customer GetCusts(string Email, string Password)
        {
            string url = "http://10.0.2.2:8080/api/CustomerLogginIn?Email=" + Email + "&Password=" + Password + "";
            HttpResponseMessage response = client.GetAsync(url).Result;
            Customer data = new Customer();
            data = JsonConvert.DeserializeObject<Customer>(response.Content.ReadAsStringAsync().Result);

            return data;
        }

        /*public async void GetCustss(string Email, string Password)
        {
            string url = @"http://10.0.2.2:8080/api/CustomerLogginIn?Email=" + Email + "&Password=" + Password;
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Customer data = new Customer();
                data = JsonConvert.DeserializeObject<Customer>(content);

                var loggedOn = Application.Context.GetSharedPreferences("CustomerList", FileCreationMode.Private);
                var loggedEdit = loggedOn.Edit();

                loggedEdit.PutString("Firstname", JsonConvert.DeserializeObject<Customer>(content).Firstname);
                loggedEdit.PutString("Lastname", JsonConvert.DeserializeObject<Customer>(content).Lastname);
                loggedEdit.PutString("Contact", JsonConvert.DeserializeObject<Customer>(content).Contact);
                loggedEdit.PutString("Email", JsonConvert.DeserializeObject<Customer>(content).Email);
                loggedEdit.PutString("Password", JsonConvert.DeserializeObject<Customer>(content).Password);

                loggedEdit.Commit();
            }
        }*/

        //Update Service
        public async void Update(Customer cust, int id)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(cust),Encoding.UTF8 , "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync("http://10.0.2.2:8080/api/UpdateCustomer?id=" + id, content);
            var jasonC = content.ReadAsStringAsync().Result; 
        }

        //Register
        public async void Register(Customer cust)
        {
            string url = "http://10.0.2.2:8080/api/Register";
            var json = JsonConvert.SerializeObject(cust);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                GetCusts(cust.Email, cust.Password);
            }
        }
    }
}
