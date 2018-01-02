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
    [Activity(Label = "Payment")]
    public class PaymentActivity : Activity
    {
        EditText txtCardNaam, txtCardNum, txtCvv, txtCustId;
        Button makePayment;
        static string email = null;
        static string password = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Payment);

            txtCardNaam = FindViewById<EditText>(Resource.Id.txtCardName);
            txtCardNum = FindViewById<EditText>(Resource.Id.txtCardNumber);
            txtCvv = FindViewById<EditText>(Resource.Id.txtCCV);
            makePayment = FindViewById<Button>(Resource.Id.btnMakePayment);
            txtCustId = FindViewById<EditText>(Resource.Id.txtCustomerid);


            email = Intent.GetStringExtra("Email");
            password = Intent.GetStringExtra("Password");

            service sv = new service();

            Customer ct = sv.GetCusts(email, password);

            txtCustId.Text = ct.Customer_Id.ToString();

            makePayment.Click += MakePayment_Click;
        }

        async void MakePayment_Click(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();

                var user = new Payment()
                {
                    CardName = txtCardNaam.Text,
                    CardNumber = txtCardNum.Text,
                    CardCCV = txtCvv.Text,
                    Customer_Id = Convert.ToInt32(txtCustId.Text)
                };

                txtCardNaam.Text = "";
                txtCardNum.Text = "";
                txtCvv.Text = "";
                txtCustId.Text = "";

                string url = "http://10.0.2.2:8080/api/Payment";
                var uri = new System.Uri(string.Format(url));
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Payment pay = JsonConvert.DeserializeObject<Payment>(data);

                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("Checkout ");
                    alert.SetMessage("Payment was successful ");
                    alert.SetButton("OK", (c, ev) =>
                    {

                        Intent ip = new Intent(this, typeof(OrderActivity));
                        string Total = Intent.GetStringExtra("total");
                        string quan = Intent.GetStringExtra("quantity");

                        ip.PutExtra("total", Total);
                        ip.PutExtra("quantity", quan);
                        ip.PutExtra("Email", email);
                        ip.PutExtra("Password", password);
                        StartActivity(ip);
                        // Ok button click task  
                    });
                    alert.Show();

                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }

        }
    }
}
