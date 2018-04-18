using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Spalanie
{
    public partial class FuelType : Window
    {
        RadioButton[] radioLabel = new RadioButton[3];
        decimal[] price = new decimal[3];
        string[] label = new string[3];
        public decimal chosenFuel = 0;
        public string chosenFuelText = "";


        public FuelType()
        {
            InitializeComponent();
            radioLabel[0] = et;
            radioLabel[1] = on;
            radioLabel[2] = lpg;

            PricesInfo();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            RadioCheck();
            DialogResult = true;
            Close();
        }
        
        private void Prices_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void PricesInfo()
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader("ceny.txt"))
            {
                for(int i = 0; !sr.EndOfStream; i++)
                {
                    string oneLine = sr.ReadLine();
                    label[i] = LabelRegex(oneLine);
                    price[i] = Decimal.Parse(PriceRegex(oneLine), System.Globalization.CultureInfo.InvariantCulture);

                    radioLabel[i].Content = String.Format(" ({0:C}) ", price[i]);
                    
                    switch (i)
                    {
                        case 0:
                            radioLabel[i].Content += "Benzyna";
                            break;
                        case 1:
                            radioLabel[i].Content += "Olej napędowy";
                            break;
                        case 2:
                            radioLabel[i].Content += "Gaz";
                            break;
                    }
                }
            }
        }

        private void RadioCheck()
        {
            if (on.IsChecked == true)
            {
                chosenFuel = price[1];
                chosenFuelText = label[1];
            }
            else if (lpg.IsChecked == true)
            {
                chosenFuel = price[2];
                chosenFuelText = label[2];
            }
            else
            {
                chosenFuel = price[0];
                chosenFuelText = label[0];
            }
        }

        public decimal PricesInfoInit()
        {
            chosenFuelText = label[0];
            return price[0];
        }

        private string LabelRegex(string label)
        {
            return Regex.Match(label, "^[A-Z]+").Value;
        }

        private string PriceRegex(string price)
        {
            return Regex.Match(price, @"\d+\.\d+").Value;
        }
    }
}
