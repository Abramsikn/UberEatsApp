
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UberEatsApp
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        Button btnRestuarants;
        Button btnProfile;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Home);


            //you can create a method for your events as well (to nav)
            btnRestuarants.Click += btnRestuarants_Click;
            btnProfile.Click += btnProfile_Click;
        }

        void btnRestuarants_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(UpdateActivity));
            StartActivity(intent);
        }

        void btnProfile_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(UpdateActivity));
            StartActivity(intent);
        }
    }
}
