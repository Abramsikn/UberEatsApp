using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using UberEatsApp.Models;
using Android.Graphics;
using Android.Views;
using Android.Text;
using System.Collections;
using System.Linq;

namespace UberEatsApp
{
    [Activity(Label = "UberEats", MainLauncher = true, Icon = "@drawable/uberuber")]
    public class MainActivity : Activity
    {
        static string uri = @"http://10.0.2.2:8080/api/Restaurant";
        public static Context contextt;
        private static List<Restaurant> rest = new List<Restaurant>();
        static ListView listRestaurants;

        private SearchView search;
        private static ArrayList ARestaurant;
        private ArrayAdapter<Restaurant> adatp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource 
            SetContentView(Resource.Layout.Main);

            listRestaurants = FindViewById<ListView>(Resource.Id.lstRests);
            listRestaurants.ItemClick += ListRest_ItemClick;

            GettRestu restau = new GettRestu();
            restau.Execute();

            search = FindViewById<SearchView>(Resource.Id.searchView1);
            search.SetQueryHint("Search for restuarant");

            listRestaurants.Adapter = new ProImageAdapter(this, rest);

            search.QueryTextChange += Search_QueryTextChange;

            search.QueryTextSubmit += (sender, e) => {
                Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;
            };
        }
        private void Search_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            //adtp.Filter.InvokeFilter(e.NewText);
            //listProp.TextFilter(e.NewText);
        }

        private void ListRest_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent ti = new Intent(this, typeof(ProductActivity));
            string Email = Intent.GetStringExtra("Email");
            string Password = Intent.GetStringExtra("Password");
            ti.PutExtra("Email", Email);
            ti.PutExtra("Password", Password);

            StartActivity(ti);
            //Toast.MakeText(this, adatp.GetItem(e.Position).ToString(), ToastLength.Long).Show();
        }
        /* Menu */
        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main_Menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }



        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.login_id:
                    var intent = new Intent(this, typeof(LoginActivity));
                    StartActivity(intent);
                    return true;
                case Resource.Id.reg_id:
                    var intents = new Intent(this, typeof(RegistrationActivity));
                    StartActivity(intents);
                    return true;
                default:
                    return false;
            }
        }

        public class GettRestu : AsyncTask
        {
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                HttpClient client = new HttpClient();

                Uri url = new Uri(uri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                var restaurant = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<Restaurant>>(restaurant);

                foreach (var g in result)
                {
                    rest.Add(g);
                }
                return true;
            }
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
            }
            protected override void OnPostExecute(Java.Lang.Object result)
            {
                base.OnPostExecute(result);
                listRestaurants.Adapter = new ProImageAdapter(contextt, rest);
            }
        }

        public class ProImageAdapter : BaseAdapter<Restaurant>
        {
            private List<Restaurant> prope = new List<Restaurant>();
            static Context context;

            public ProImageAdapter(Context con, List<Restaurant> lstP)
            {
                prope.Clear();
                context = con;
                prope = lstP;
                this.NotifyDataSetChanged();
            }

            public override Restaurant this[int position]
            {
                get
                {
                    return prope[position];
                }
            }

            public override int Count
            {
                get
                {
                    return prope.Count;
                }
            }
            public Context Mcontext
            {
                get;
                private set;
            }
            public override long GetItemId(int position)
            {
                return position;
            }

            public Bitmap getBitmap(byte[] getByte)
            {
                if (getByte.Length != 0)
                {
                    return BitmapFactory.DecodeByteArray(getByte, 0, getByte.Length);
                }
                else
                {
                    return null;
                }
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View restuarants = convertView;
                if (restuarants == null)
                {
                    restuarants = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Home, parent, false);
                }
                TextView txtName = restuarants.FindViewById<TextView>(Resource.Id.txtName);
                //TextView txtType = restuarants.FindViewById<TextView>(Resource.Id.txtType);
                //TextView txtDesc = restuarants.FindViewById<TextView>(Resource.Id.txtDesc);
                //TextView txtAdres = restuarants.FindViewById<TextView>(Resource.Id.txtAdres);
                TextView txtCitty = restuarants.FindViewById<TextView>(Resource.Id.txtCitty);
                TextView txtContct = restuarants.FindViewById<TextView>(Resource.Id.txtContct);
                ImageView Image = restuarants.FindViewById<ImageView>(Resource.Id.Image);

                if (prope[position].Image != null)
                {
                    Image.SetImageBitmap(BitmapFactory.DecodeByteArray(prope[position].Image, 0, prope[position].Image.Length));
                }

                txtName.Text = prope[position].Rest_Name;
                //txtType.Text = prope[position].Rest_Type;
                //txtDesc.Text = prope[position].Rest_Desc;
                //txtAdres.Text = prope[position].Rest_Address;
                txtCitty.Text = prope[position].Rest_City;
                txtContct.Text = prope[position].Rest_Contact;

                Image.Tag = prope[position].Image;
                return restuarants;
            }
        }
    }
}

