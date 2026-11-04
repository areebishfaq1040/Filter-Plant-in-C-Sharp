using System;

namespace Project_Filter_Plant

{
    public class Customer
    {
        public int id;
        public string name;
        public string phone;
        public string address;
        public string password;

        public int quantity;
        public string gap;
        public string deliveryDate;
        public int totalBill;
        public int paidAmount;
        public string billStatus;

        public Customer(string name, string phone, string address, string password, int id)
        {
            this.name = name;
            this.phone = phone;
            this.address = address;
            this.password = password;
            this.id = id;

            this.quantity = 0;
            this.gap = "N/A";
            this.deliveryDate = "N/A";
            this.totalBill = 0;
            this.paidAmount = 0;
            this.billStatus = "Pending";
        }

        public void PlaceOrder(int qty, string deliveryGap, string date)
        {
            this.quantity = qty;
            this.gap = deliveryGap;
            this.deliveryDate = date;
            this.totalBill = qty * 200;
            this.billStatus = "Pending";
        }

        public void CancelOrder()
        {
            this.quantity = 0;
            this.gap = "N/A";
            this.deliveryDate = "N/A";
            this.totalBill = 0;
            this.billStatus = "Pending";
        }

        public void AddDeliveryPayment(int canesDelivered, string status)
        {
            this.totalBill += canesDelivered * 200;
            if (status.ToLower() == "paid")
            {
                this.paidAmount += canesDelivered * 200;
                this.billStatus = "Paid";
            }
            else
            {
                this.billStatus = "Pending";
            }
        }
    }
}