using System;

namespace SalesDepart
{
    public class Seller
    {
        public Seller(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public Seller(int id, string name, string surname)
        {
            ID = id;
            Name = name;
            Surname = surname;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return $"{ID} {Name}, {Surname}";
        }
    }

    public class Customer
    {
        public Customer(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public Customer(int id, string name, string surname)
        {
            ID = id;
            Name = name;
            Surname = surname;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return $"{ID} {Name}, {Surname}";
        }
    }

    public class SalesTable
    {
        public SalesTable(int sellerID, int customerID, decimal price, DateTime dateTime)
        {
            SellerID = sellerID;
            CustomerID = customerID;
            Price = price;
            this.dateTime = dateTime;
        }
        public SalesTable(int id, int sellerID, int customerID, decimal price, DateTime dateTime)
        {
            ID = id;
            SellerID = sellerID;
            CustomerID = customerID;
            Price = price;
            this.dateTime = dateTime;
        }

        public int ID { get; set; }
        public int SellerID { get; set; }
        public int CustomerID { get; set; }
        public decimal Price { get; set; }
        public DateTime dateTime { get; set; }

        public override string ToString()
        {
            return $"{ID} {SellerID}, {CustomerID}, {Price}, {dateTime.ToString()}";
        }
    }
}
