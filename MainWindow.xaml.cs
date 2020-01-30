using System.Windows;
using System.Data;
using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;

namespace SalesDepart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IEnumerable<Customer> customers { get; set; }
        public IEnumerable<SalesTable> sales { get; set; }

        public IEnumerable<Seller> seller { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ConnectionStringSettings connectionSettings = ConfigurationManager.ConnectionStrings["localdb"];

            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionSettings.ProviderName);

            string connectionString = connectionSettings.ConnectionString;





            using (SqlSalesRepository repo = new SqlSalesRepository(connectionString, factory))
            {

                repo.CreateDB();
                repo.CreateTables();
                repo.InsertCustomer(1, "Ulvi", "Bashirov");
                repo.InsertCustomer(2, "Elvin", "Ismayilov");
                repo.InsertCustomer(3, "Johhny", "Depp");
                repo.InsertSeller(1, "Abraham", "Lincoln");
                repo.InsertSeller(2, "Bill", "Gates");
                repo.InsertSeller(3, "Steve", "Jobs");
                repo.InsertSales(1, 1, 1, 50.25, new DateTime(2020, 01, 20, 20, 15, 0));
                repo.InsertSales(2, 2, 2, 125.30, new DateTime(2020, 01, 21, 20, 12, 10));
                repo.InsertSales(3, 3, 3, 5.75, new DateTime(2020, 01, 22, 21, 07, 10));
                //repo.DropTables();
                //repo.DropDB();


                customers = repo.GetCustomers();
                sales = repo.GetSales();
                seller = repo.GetSellers();


                combo.Items.Add("Customers");
                combo.Items.Add("Sellers");
                combo.Items.Add("Sales");


                repo.Save();
            }
        }

        private void combolist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (combo.SelectedIndex == 0)
            {
                combolist.ItemsSource = customers;
            }
            else if (combo.SelectedIndex == 1)
            {
                combolist.ItemsSource = seller;
            }
            else if (combo.SelectedIndex == 2)
            {
                combolist.ItemsSource = sales;
            }
            else
            {
                combolist.ItemsSource = null;
            }
        }
    }
}
