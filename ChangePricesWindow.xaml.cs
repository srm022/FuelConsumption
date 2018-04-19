using System.Text.RegularExpressions;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System;

namespace FuelConsumption
{
    public partial class ChangePricesWindow : Window
    {
        FileHandler handler;
        decimal[] prices;

        public ChangePricesWindow(decimal[] prices, FileHandler handler)
        {
            this.prices = prices;
            this.handler = handler;

            InitializeComponent();
            ETPriceTextBox.Text = prices[0].ToString();
            ONPriceTextBox.Text = prices[1].ToString();
            LPGPriceTextBox.Text = prices[2].ToString();
        }

        private bool ParseTextBoxes()                                           // przenosi wartosci z textboxow do tablicy cen i zaokragla do 2 msc.
        {
            try
            {
                if (!TextBoxRegexCheck(ETPriceTextBox.Text) || !TextBoxRegexCheck(ONPriceTextBox.Text) || !TextBoxRegexCheck(LPGPriceTextBox.Text))
                    return false;
                else
                {
                    prices[0] = Math.Round(Decimal.Parse(ETPriceTextBox.Text), 2);
                    prices[1] = Math.Round(Decimal.Parse(ONPriceTextBox.Text), 2);
                    prices[2] = Math.Round(Decimal.Parse(LPGPriceTextBox.Text), 2);
                    return true;
                }
            }
            catch(FormatException)
            {
                return false;
            }
        }

        private bool TextBoxRegexCheck(string PriceString)
        {
            return Regex.IsMatch(PriceString, @"^\d+\,\d+$");
        }

        public decimal GetChangedFuelPrices(int i)              // zwraca wartosci z textboxów
        {
            return prices[i];
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ParseTextBoxes())
                MessageBox.Show("Wpisane dane są w złym formacie!", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                handler.Writer(prices);
                DialogResult = true;
                Close();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ETPriceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ETPriceTextBox.Text == "" || ETPriceTextBox.Text == prices[0].ToString() || !TextBoxRegexCheck(ETPriceTextBox.Text)) // sprawdza, czy wartość textboxa jest pusta lub w nieprawidłowym formacie
            {
                ETPriceTextBox.Text = prices[0].ToString();
                ETPriceTextBox.Foreground = SystemColors.ActiveBorderBrush;
            }
        }

        private void ONPriceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ONPriceTextBox.Text == "" || ONPriceTextBox.Text == prices[1].ToString() || !TextBoxRegexCheck(ETPriceTextBox.Text)) // sprawdza, czy wartość textboxa jest pusta lub w nieprawidłowym formacie
            {
                ONPriceTextBox.Text = prices[1].ToString();
                ONPriceTextBox.Foreground = SystemColors.ActiveBorderBrush;
            }
        }

        private void LPGPriceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LPGPriceTextBox.Text == "" || LPGPriceTextBox.Text == prices[2].ToString() || !TextBoxRegexCheck(ETPriceTextBox.Text)) // sprawdza, czy wartość textboxa jest pusta lub w nieprawidłowym formacie
            {
                LPGPriceTextBox.Text = prices[2].ToString();
                LPGPriceTextBox.Foreground = SystemColors.ActiveBorderBrush;
            }
        }

        private void ETPriceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ETPriceTextBox.Foreground = SystemColors.WindowTextBrush;
        }

        private void ONPriceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ONPriceTextBox.Foreground = SystemColors.WindowTextBrush;
        }

        private void LPGPriceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LPGPriceTextBox.Foreground = SystemColors.WindowTextBrush;
        }
    }
}
