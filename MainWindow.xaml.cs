using System.Windows;
using System.Data;
using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;

namespace SalesDepart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConnectionStringSettings connectionSettings = ConfigurationManager.ConnectionStrings["localdb"];

            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionSettings.ProviderName);

            string connectionString = connectionSettings.ConnectionString;

            using (SqlSalesRepository repo = new SqlSalesRepository(connectionString, factory))
            {
                repo.CreateDB("Sales");
            }
        }
    }
}
