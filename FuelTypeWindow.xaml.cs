using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace FuelConsumption
{
    public partial class FuelTypeWindow : Window
    {
        RadioButton[] radioLabel;
        decimal[] prices = new decimal[3];
        string[] label = new string[3];
        public decimal chosenFuel = 0;
        public string chosenFuelText = "";
        
        public FuelTypeWindow()
        {
            InitializeComponent();
            radioLabel = new RadioButton[3] { et, on, lpg }; 

            PricesInfo();
        }

        public void PricesInfo()
        {
            FileHandler handler = new FileHandler();
            string[] ReadLines = handler.Reader("ceny.txt");

            for(int i = 0; i < 3; i++)
            {
                label[i] = LabelRegex(ReadLines[i]);
                prices[i] = Decimal.Parse(PriceRegex(ReadLines[i]));
                SetRadioButtonContent(i);
            }
        }

        private void SetRadioButtonContent(int i)
        {
            radioLabel[i].Content = String.Format(" ({0:C}) ", prices[i]);

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

        private void RadioCheck()
        {
            if (on.IsChecked == true)
            {
                chosenFuel = prices[1];
                chosenFuelText = label[1];
            }
            else if (lpg.IsChecked == true)
            {
                chosenFuel = prices[2];
                chosenFuelText = label[2];
            }
            else
            {
                chosenFuel = prices[0];
                chosenFuelText = label[0];
            }
        }

        public decimal PricesInfoInit()
        {
            chosenFuelText = label[0];
            return prices[0];
        }

        private string LabelRegex(string label)
        {
            return Regex.Match(label, "^[A-Z]+").Value;
        }

        private string PriceRegex(string price)
        {
            return Regex.Match(price, @"\d+\,\d+").Value;
        }

        private void ChangePrices_Click(object sender, RoutedEventArgs e)
        {
            ChangePricesWindow pricesWindow = new ChangePricesWindow(prices);
            if(pricesWindow.ShowDialog() == true)
            {
                for(int i = 0; i < 3; i++)
                {
                    prices[i] = pricesWindow.GetChangedFuelPrices(i);
                    SetRadioButtonContent(i);
                    Console.WriteLine(i);
                }

                pricesWindow.Close();
            }
        }

        public void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            RadioCheck();
            DialogResult = true;
            Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
