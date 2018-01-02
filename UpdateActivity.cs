using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using UberEatsApp.Models;

namespace UberEatsApp
{
    [Activity(Label = "Update")]
    public class UpdateActivity : Activity
    {
        Button btnEdit;
        EditText txtEditF, txtEditL, txtEditCont, txtEditE, txtEditP;
        Customer custom = new Customer();
        //DataAccess da = new DataAccess();
        service ser = new service();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Update);

            var loggedOn = Application.Context.GetSharedPreferences("CustomerList", FileCreationMode.Private);

            string Email = Intent.GetStringExtra("Email");
            string Password = Intent.GetStringExtra("Password");

            custom = ser.GetCusts(Email,Password); //

            txtEditF = FindViewById<EditText>(Resource.Id.txtEditFirstname);
            txtEditL = FindViewById<EditText>(Resource.Id.txtEditLastname);
            txtEditCont = FindViewById<EditText>(Resource.Id.txtEditContact);
            txtEditE = FindViewById<EditText>(Resource.Id.txtEmail);
            txtEditP = FindViewById<EditText>(Resource.Id.txtPassword);


            //txtEditF.Text = custom.Firstname;
            //txtEditL.Text = custom.Lastname;
            //txtEditCont.Text = custom.Contact;

            btnEdit = FindViewById<Button>(Resource.Id.btnUpdate);

            btnEdit.Click += BtnEdit_ClickAsync;
        }

        private void BtnEdit_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                custom.Firstname = txtEditF.Text;
                custom.Lastname = txtEditL.Text;
                custom.Contact = txtEditCont.Text;

                ser.Update(custom, custom.Customer_Id);

                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle(" Update ");
                alert.SetMessage(" User information is updated successfully ");
                alert.SetButton("OK", (c, ev) =>
                {
                    // Ok button click task 
                    Intent inter = new Intent(this, typeof(MainActivity));
                    StartActivity(inter);

                });
                alert.Show();


            }
            catch (Exception error)
            {
                Toast.MakeText(this, error.ToString(), ToastLength.Short).Show();
                Intent inters = new Intent(this, typeof(MainActivity));
                inters.PutExtra("Customer_Id", custom.Customer_Id);
                inters.PutExtra("Password", custom.Email);
                StartActivity(inters);

            }


        }
    }
}
