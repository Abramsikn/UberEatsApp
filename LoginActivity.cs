using System.Collections.Specialized;
using System.Net;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using System.Net.Http;
using System.Text;
using Org.Json;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UberEatsApp.Models;
using Newtonsoft.Json;


namespace UberEatsApp
{
    [Activity(Label = "Login")]
    public class LoginActivity : Activity
    {
        TextView text;
        //HttpClient client;
        EditText txtE, txtP;
        Button btnLogin;
        //isLoggedIn = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);

            text = FindViewById<TextView>(Resource.Id.txtCreateAcc);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogIn);

            txtE = FindViewById<EditText>(Resource.Id.txtEmail);
            txtP = FindViewById<EditText>(Resource.Id.txtPassword);

            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //client = new HttpClient();
                service serDt = new service();
                //DataAccess datA = new DataAccess();

                Customer cust = new Customer();
                cust = serDt.GetCusts(txtE.Text, txtP.Text);

                if (String.IsNullOrEmpty(txtE.Text) && String.IsNullOrEmpty(txtP.Text))
                {
                    Toast.MakeText(this, "Email and password can't be empty, please provide correct information", ToastLength.Short).Show();
                }
                else if (txtE.Text == cust.Email && txtP.Text == cust.Password)
                {
                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle(" Login ");
                    alert.SetMessage("Successfully logged on as  " + cust.Firstname + " " + cust.Lastname);
                    alert.SetButton("OK", (c, ev) =>
                    {
                        // Ok button click task 
                        Intent ti = new Intent(this, typeof(RestuarantActivity));
                        ti.PutExtra("Email", cust.Email);
                        ti.PutExtra("Password", cust.Password);
                        StartActivity(ti);
                    });
                    alert.Show();

                    //Toast.MakeText(this, "Successfully logged in " + cust.Firstname + cust.Lastname, ToastLength.Short).Show();
                    //Intent ti = new Intent(this, typeof(RestuarantActivity));
                    //StartActivity(ti);
                }
                else
                {
                    Toast.MakeText(this, "Incorrect login details. Register if you havent registered.", ToastLength.Short).Show();
                }
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}
