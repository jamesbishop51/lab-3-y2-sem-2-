using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities db = new NORTHWNDEntities();
        public MainWindow()
        {
            InitializeComponent();
        }
        //question 1
        private void btnQueryEx1_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        select c.CompanyName;

            lbxCustomersEx1.ItemsSource = query.ToList();
        }
        //question 2
        private void btnQueryEx2_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        select c;

            dgCustomersEx2.ItemsSource = query.ToList();
        }
        //question 3
        private void btnQueryEx3_Click(object sender, RoutedEventArgs e)
        {
            var query = from o in db.Orders
                        where o.Customer.City.Equals("London")
                        || o.Customer.City.Equals("Paris")
                        || o.Customer.Country.Equals("USA")
                        orderby o.Customer.CompanyName
                        select new
                        {
                            CustomerName = o.Customer.CompanyName,
                            City = o.Customer.City,
                            Address = o.ShipAddress
                        };

            dgCustomersEx3.ItemsSource = query.ToList().Distinct();
        }
        //question 4
        private void btnQueryEx4_Click(object sender, RoutedEventArgs e)
        {
            var query = from p in db.Products
                        where p.Category.CategoryName.Equals("Beverages")
                        orderby p.ProductID descending
                        select new
                        {
                            p.ProductID,
                            p.ProductName,
                            p.Category.CategoryName,
                            p.UnitPrice
                        };
            dgCustomersEx4.ItemsSource = query.ToList();
        }
        //question 5
        private void btnQueryEx5_Click(object sender, RoutedEventArgs e)
        {

            Product p = new Product()
            {
                ProductName = "Kickapoo jungle joy juice",
                UnitPrice = 12.49m,
                CategoryID = 1
            };

            db.Products.Add(p);
            db.SaveChanges();

            ShowProducts(dgCustomersEx5);
        }
        //gets the latest product information and displays it
        private void ShowProducts(DataGrid currentGrid)
        {
            var query = from p in db.Products
                        where p.Category.CategoryName.Equals("Beverages")
                        orderby p.ProductID descending
                        select new
                        {
                            p.ProductID,
                            p.ProductName,
                            p.Category.CategoryName,
                            p.UnitPrice
                        };
            currentGrid.ItemsSource = query.ToList();
        }

    }
}
