using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.Pages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            PopulateCustomers().ConfigureAwait(false);
            PopulateProducts().ConfigureAwait(false);

        }

        private async Task PopulateCustomers()
        {
            var collection = new ObservableCollection<KeyValuePair<int, string>>();
            using var client = new HttpClient();

            foreach (var customer in await client.GetFromJsonAsync<IEnumerable<CustomerModel>>("https://localhost:7217/api/customers"))
                collection.Add(new KeyValuePair<int, string>(customer.Id, customer.Name));

            cb_customers.ItemsSource = collection;
        }

        private async Task PopulateProducts()
        {
            var collection = new ObservableCollection<KeyValuePair<Guid, string>>();
            using var client = new HttpClient();

            foreach (var product in await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7217/api/customers"))
                collection.Add(new KeyValuePair<Guid, string>(product.Id, product.Name));

            cb_products.ItemsSource = collection;
        }


        private void btn_Add_ProductToList_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_Save_Order_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
