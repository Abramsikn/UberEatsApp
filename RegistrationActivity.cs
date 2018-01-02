using System;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using UberEatsApp.Models;

namespace UberEatsApp
{
    [Activity(Label = "Registration")]
    public class RegistrationActivity : Activity
    {
        //string strConnString = ConfigurationManager.ConnectionStrings["UberEatsAppDervice"].ConnectionString;
        static string url = "http://10.0.2.2:8080/api/Register";
        EditText txtFirstname, txtLastname, txtContact, txtEmail, txtPassword;
        HttpClient client;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Register);

            txtFirstname = FindViewById<EditText>(Resource.Id.txtFirstname);
            txtLastname = FindViewById<EditText>(Resource.Id.txtLastname);
            txtContact = FindViewById<EditText>(Resource.Id.txtContact);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);

            Button butRegister = FindViewById<Button>(Resource.Id.btnRegisterCustomer);
            butRegister.Click += ButRegister_Click;

            TextView text = FindViewById<TextView>(Resource.Id.txtRegister);

            text.Click += delegate
            {
                Intent inte = new Intent(this, typeof(LoginActivity));
                StartActivity(inte);
            };

        }

        private async void ButRegister_Click(object sender, EventArgs e)
        {
            try
            {
                client = new HttpClient();
                var myClient = new Customer
                {
                    Firstname = txtFirstname.Text,
                    Lastname = txtLastname.Text,
                    Contact = txtContact.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text
                };

                txtFirstname.Text = "";
                txtLastname.Text = "";
                txtContact.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";


                var uri = new System.Uri(string.Format(url));
                var json = JsonConvert.SerializeObject(myClient);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Customer custs = JsonConvert.DeserializeObject<Customer>(data);
                    Toast.MakeText(this, "Thank you for registering with UberEats", ToastLength.Long).Show();
                    Intent ip = new Intent(this, typeof(MainActivity));
                    StartActivity(ip);
                }
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

    }
}
