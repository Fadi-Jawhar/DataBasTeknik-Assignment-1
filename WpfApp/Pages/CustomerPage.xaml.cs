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
using WpfApp.Models.Entities;
using WpfApp.Services;

namespace WpfApp.Pages
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();
            PopulateCustomerModel().ConfigureAwait(false);

        }

        public async Task PopulateCustomerModel()
        {
            var collection = new ObservableCollection<KeyValuePair<int, string>>();
            using var client = new HttpClient();

            foreach (var customer in await client.GetFromJsonAsync<IEnumerable<CustomerModel>>("https://localhost:7217/api/customers"))
                collection.Add(new KeyValuePair<int, string>(customer.Id, customer.Name));

            cb_customer_list.ItemsSource = collection;
        }

        private async void btn_customer_Save_Click(object sender, RoutedEventArgs e)
        {
            using var client = new HttpClient();

            await client.PostAsJsonAsync("https://localhost:7217/api/customers", new CustomerCreateModel
            {
                Name = tb_customer_name.Text,
                Email = tb_customer_email.Text,
                Phone = tb_customer_phone.Text
            });

            
            tb_customer_name.Text = string.Empty;
            tb_customer_email.Text = string.Empty;
            tb_customer_phone.Text = string.Empty;
            cb_customer_list.SelectedIndex = -1;

        }

        private async void btn_customer_Update_Click(object sender, RoutedEventArgs e)
        {
            var uRL = "https://localhost:7217/api/Customers";
            using var client = new HttpClient();
            var customer = (CustomerModel)cb_customer_list.SelectedItem;

            if (customer != null && tb_customer_name.Text != "")
            {
                try
                {
                    customer.Name = tb_customer_name.Text;
                    customer.Email = tb_customer_email.Text;
                    customer.Phone = tb_customer_phone.Text;

                    await client.PutAsJsonAsync($"{uRL}?id={customer.Id}", customer);

                    MessageBox.Show("Kund uppdaterad");
                    tb_customer_name.Text = string.Empty;
                    tb_customer_email.Text = string.Empty;
                    tb_customer_phone.Text = string.Empty;
                    cb_customer_list.SelectedIndex = -1;
                    await PopulateCustomerModel();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Namn krävs att fylla i för att uppdatera kund");
            }
        }
    }
}
