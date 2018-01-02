using System;
namespace UberEatsApp.Models
{
    public class Order
    {
        public int idOrder { get; set; }
        public string OrderAmount { get; set; }
        public string OrderQuantity { get; set; }
        public string OrderAddress { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardCCV { get; set; }
        public int Customer_Id { get; set; }

        public Order(int id, string orderAmt, string orderQty, string orderAddr, string cardName, string cardNumber, string cardCcv, int customerId)
        {
            idOrder = id;
            OrderAmount = orderAmt;
            OrderQuantity = orderQty;
            OrderAddress = orderAddr;
            CardName = cardName;
            CardNumber = cardNumber;
            CardCCV = cardCcv;
            Customer_Id = customerId;

        }

        public Order(string orderAmt, string orderQty, string orderAddr, string cardName, string cardNumber, string cardCcv, int customerId)
        {
            OrderAmount = orderAmt;
            OrderQuantity = orderQty;
            OrderAddress = orderAddr;
            CardName = cardName;
            CardNumber = cardNumber;
            CardCCV = cardCcv;
            Customer_Id = customerId;

        }

        public Order(string orderAmt, string orderQty, string orderAddr, string cardName, string cardNumber, string cardCcv)
        {
            OrderAmount = orderAmt;
            OrderQuantity = orderQty;
            OrderAddress = orderAddr;
            CardName = cardName;
            CardNumber = cardNumber;
            CardCCV = cardCcv;
        }

        public Order()
        {

        }
    }
}
