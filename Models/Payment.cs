using System;
namespace UberEatsApp.Models
{
    public class Payment
    {
        public int idPayment { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardCCV { get; set; }
        public int Customer_Id { get; set; }

        public Payment()
        {

        }



        public Payment(int id, string cardName, string cardNum, string ccv, int custId)
        {
            idPayment = id;
            CardName = cardName;
            CardNumber = cardNum;
            CardCCV = ccv;
            Customer_Id = custId;

        }

        public Payment(string cardName, string cardNum, string cvv)
        {

            CardName = cardName;
            CardNumber = cardNum;
            CardCCV = cvv;
        }

    }
}
