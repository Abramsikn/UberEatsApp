using System;
namespace UberEatsApp.Models
{
    public class RESTAddressess
    {
        RESTAddressess adress = new RESTAddressess();

        public static String customerReg = @"http://10.0.2.2:8080/api/Register";
        public static String customerLog = @"http://10.0.2.2:8080/api/CustomerLogginIn";
        public static String customerUpdate = @"http://10.0.2.2:8080/api/UpdateCustomer";
        public static String RestaurantGet = @"http://10.0.2.2:8080/api/GetRestaurants";
        public static String Products = @"http://10.0.2.2:8080/api/Products";
        public static String Order = @"http://10.0.2.2:8080/api/Order";
    }
}
