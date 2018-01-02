
using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using UberEatsApp.Models;
using static UberEatsApp.ProductActivity;

namespace UberEatsApp
{
    [Activity(Label = "Cart")]
    public class CartActivity : Activity
    {
        static string TotalAmount = "";
        static string Quantity = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CartItems);

            Intent intent = new Intent();
            ListView listView2;
            TextView txtTotal, txtQuantity, txtProductname;
            Button btncheckOut;

            List<string> Mylist = new List<string>();


            listView2 = FindViewById<ListView>(Resource.Id.cartItems);
            txtTotal = FindViewById<TextView>(Resource.Id.cartTotal);
            txtProductname = FindViewById<TextView>(Resource.Id.cartProductName);
            txtQuantity = FindViewById<TextView>(Resource.Id.cartQuantity);
            btncheckOut = FindViewById<Button>(Resource.Id.btnCheckout);


            ISharedPreferences pref = Application.Context.GetSharedPreferences("Cart", FileCreationMode.Private);
            ISharedPreferencesEditor edit = pref.Edit();
            string ProductName = null;
            string pPrice = null;


            ProductName = Intent.GetStringExtra("ProductName");
            pPrice = Intent.GetStringExtra("ProductPrice");
            string Total = Intent.GetStringExtra("total");
            string quantiy = Intent.GetStringExtra("quantity");

            TotalAmount  = Total;
            Quantity = quantiy;

            txtTotal.Text = "Total Amount : " + "R " + TotalAmount;
            txtProductname.Text = ProductName;
            txtQuantity.Text = "Number of Quantity : " + Quantity;

            Mylist.Add("Item name : " + ProductName);
            //Mylist.Add(pPrice);
            Mylist.Add(txtTotal.Text);
            Mylist.Add(txtQuantity.Text);



            ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, Mylist);

            listView2.Adapter = adapter;
            btncheckOut.Click += BtncheckOut_Click;
        }

        void BtncheckOut_Click(object sender, EventArgs e)
        {
            Intent ip = new Intent(this, typeof(OrderActivity));
            //ip.PutExtra("total",);
            string Customer_Id = Intent.GetStringExtra("Customer_Id");
            string Firstname = Intent.GetStringExtra("Firstname");
            string Email = Intent.GetStringExtra("Email");
            string Password = Intent.GetStringExtra("Password");
            ip.PutExtra("Customer_Id", Customer_Id);
            //ip.PutExtra("name", first_name);
            ip.PutExtra("total", TotalAmount);
            ip.PutExtra("quantity", Quantity);
            ip.PutExtra("Email", Email);
            ip.PutExtra("Password", Password);
            StartActivity(ip);
        }
    }
}
