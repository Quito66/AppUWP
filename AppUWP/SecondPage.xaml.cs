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
        public Item Item { get; set; }
        public SecondPage()
        {
            this.InitializeComponent();
            this.Loaded += Page_Loaded;
        }
        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Item = new Item();
            await LoadItemsApi(); // Cargar elementos en la colección
            itemListView.Items.Add(Item);
        }
        public async Task LoadItemsApi()
        {
            string url = "https://api.chucknorris.io/jokes/random"; // URL de la API a la que deseas realizar la solicitud
            Item personList = new Item();
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        personList = JsonConvert.DeserializeObject<Item>(responseBody);
                        this.Item= personList;
                    }
                    
                }
                catch (Exception ex)
                {
                    myTextBox.Text = "Error: " + ex.Message;
                    myTextBox.Visibility= Visibility.Visible;
                }
            }
        }
    }
}
