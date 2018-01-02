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
    [Activity(Label = "Order")]
    public class OrderActivity : Activity
    {
        Customer cust = new Customer();
        service sev = new service();

        EditText CustId, deliveryAddress, cardName, cardNumber, cardCcv;
        Button btnOrder;

        static string totalAmont;
        static string qnty;
        static string Email, Password;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Checkout);

            deliveryAddress = FindViewById<EditText>(Resource.Id.txtDeliveryAddress);
            cardName = FindViewById<EditText>(Resource.Id.txtCardName);
            cardNumber = FindViewById<EditText>(Resource.Id.txtCardNumber);
            cardCcv = FindViewById<EditText>(Resource.Id.txtCCV);
            CustId = FindViewById<EditText>(Resource.Id.txtCustomerid);

            btnOrder = FindViewById<Button>(Resource.Id.btnOrder);

            totalAmont = Intent.GetStringExtra("total");
            qnty = Intent.GetStringExtra("quantity");
            Email = Intent.GetStringExtra("Email");
            Password = Intent.GetStringExtra("Password");

            cust = sev.GetCusts(Email, Password);

            //
            Email = Intent.GetStringExtra("Email");
            Password = Intent.GetStringExtra("Password");

            service sv = new service();
            Customer ct = sv.GetCusts(Email, Password);
            //CustId.Text = ct.Customer_Id.ToString();
            //

            btnOrder.Click += BtnOrder_Click;
        }

        async void BtnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                var user = new Order()
                {
                    OrderAmount = totalAmont,
                    OrderQuantity = qnty,
                    OrderAddress = deliveryAddress.Text,
                    CardName = cardName.Text,
                    CardNumber = cardNumber.Text,
                    CardCCV = cardCcv.Text,
                    //Customer_Id = cust.Customer_Id

                };

                deliveryAddress.Text = "";
                cardName.Text = "";
                cardNumber.Text = "";
                cardCcv.Text = "";

                string url = "http://10.0.2.2:8080/api/Order";

                var uri = new System.Uri(string.Format(url));
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Order pay = JsonConvert.DeserializeObject<Order>(data);

                    deliveryAddress.Text = "";

                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle(" Order ");
                    alert.SetMessage("Order placed successfully ");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        // Ok button click task 
                        Intent ip = new Intent(this, typeof(MainActivity));
                        //StartActivity(ip);

                        string Total = Intent.GetStringExtra("total");
                        string quan = Intent.GetStringExtra("quantity");

                        ip.PutExtra("total", Total);
                        ip.PutExtra("quantity", quan);
                        ip.PutExtra("Email", Email);
                        ip.PutExtra("Password", Password);
                        StartActivity(ip);
                    });
                    alert.Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                //case Resource.Id.Main:
                //Intent it = new Intent(this, typeof(The_MainActivity));
                //return true;

                default:
                    return false;
            }
        }
    }
}
