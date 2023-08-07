using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppUWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class SecondPage : Page
    {
        public ObservableCollection<Item> Items { get; set; }
        public SecondPage()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<Item>();
            Task<bool> res = LoadItemsApi(); // Cargar elementos en la colección
            if (res.Result)
            {
                itemListView.ItemsSource = Items;
            }
            else
            {
                myTextBox.Visibility = Visibility.Visible;
            }
        }
        private async Task<bool> LoadItemsApi()
        {
            string url = "https://jsonplaceholder.typicode.com/users"; // URL de la API a la que deseas realizar la solicitud
            ObservableCollection<Item> personList = new ObservableCollection<Item>();
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        personList = JsonConvert.DeserializeObject<ObservableCollection<Item>>(responseBody);
                        this.Items= personList;
                    }
                    
                }
                catch (Exception ex)
                {
                    myTextBox.Text = "Error: " + ex.Message;
                    return false;
                }
            }
            return true;
        }
    }
}
