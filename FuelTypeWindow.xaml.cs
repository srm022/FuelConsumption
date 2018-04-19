using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace FuelConsumption
{
    public partial class FuelTypeWindow : Window
    {
        RadioButton[] radioLabel;
        FileHandler handler;
        decimal[] prices = new decimal[3];
        string[] label = new string[3];
        public decimal chosenFuel = 0;
        public string chosenFuelText = "";

        public FuelTypeWindow(FileHandler handler)
        {
            this.handler = handler;
            InitializeComponent();
            radioLabel = new RadioButton[3] { et, on, lpg }; 

            PricesInfo();
        }

        public void PricesInfo()
        {
            for (int i = 0; i < 3; i++)
            {
                label[i] = handler.GetNLabel(i);
                prices[i] = handler.GetNPrice(i);
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

        public decimal GetChosenFuel()
        {
            return chosenFuel;
        }

        public string GetChosenFuelText()
        {
            return chosenFuelText;
        }

        private void ChangePrices_Click(object sender, RoutedEventArgs e)
        {
            ChangePricesWindow pricesWindow = new ChangePricesWindow(prices, handler);
            if(pricesWindow.ShowDialog() == true)
            {
                for(int i = 0; i < 3; i++)
                {
                    prices[i] = pricesWindow.GetChangedFuelPrices(i);
                    SetRadioButtonContent(i);
                }

                handler.Reader();
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
