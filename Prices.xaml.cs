using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Spalanie
{
    /// <summary>
    /// Logika interakcji dla klasy Prices.xaml
    /// </summary>
    public partial class Prices : Window
    {
        public Prices()
        {
            InitializeComponent();

            setPrice();
        }

        private async void setPrice()
        {
            double price;
            using (StreamReader sr = new StreamReader("ceny.txt"))
            {
                string oneLine = await sr.ReadLineAsync();
                oneLine = Regex.Match(oneLine, @"\d+\.\d+").Value;
                price = Double.Parse(oneLine, CultureInfo.InvariantCulture);
                ETprice.Content = Math.Round(price, 2);

                /*onePrice = await sr.ReadLineAsync();
                Double.Parse(onePrice);
                ONprice.Content = onePrice;

                onePrice = await sr.ReadLineAsync();
                Double.Parse(onePrice);
                LPGprice.Content = onePrice;*/
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
