using System;

namespace SalesDepart
{
    public class Model
    {
        public Model(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public Model(int id, string name, string surname)
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
        public int ID { get; set; }
        public int SalesmanID { get; set; }
        public int CustomerID { get; set; }
        public int Price { get; set; }
        public DateTime dateTime { get; set; }

        public override string ToString()
        {
            return $"{ID} {SalesmanID}, {CustomerID}, {Price}, {dateTime.ToString()}";
        }
    }
}
