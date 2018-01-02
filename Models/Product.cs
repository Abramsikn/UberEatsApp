using System;
namespace UberEatsApp.Models
{
    public class Product
    {
        public int idProduct { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public byte[] Image { get; set; }

        public Product()
        {

        }

        public Product(string tmp)
        {
            ProductName = tmp;

        }
        public Product(string name, string price, byte[] image)
        {
            ProductName = name;
            ProductPrice = price;
            Image = image;

        }
        public Product(int id, string name, string price, byte[] image)
        {
            idProduct = id;
            ProductName = name;
            ProductPrice = price;
            Image = image;
        }
    }
}
