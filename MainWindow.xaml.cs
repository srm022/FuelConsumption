using System;
using System.Windows;

namespace FuelConsumption
{
    public partial class MainWindow : Window
    {
        decimal fuelPrice;
        string path = "ceny.txt";
        FuelTypeWindow FuelTypeWindow;
        FileHandler handler;
        Calculate calculator;

        public MainWindow()
        {
            handler = new FileHandler(path);
            handler.Reader();
            InitializeComponent();
            SetFuelPrice(handler.GetNPrice(0), handler.GetNLabel(0));          // czytanie pliku i jego pierwszej linii celem inicjalizacji
            TextBoxDistanceToTravel.Focus();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            calculator.CalculateConsumption(TextBoxDistanceToTravel.Text, TextBoxAmountOfFuel.Text);
            labelResult.Content = calculator.GetLabelResult();
            kilometerCost.Content = calculator.GetKilometerCost();
        }

        private void FuelTypeButton_Click(object sender, RoutedEventArgs e)
        {
            FuelTypeWindow = new FuelTypeWindow(handler);
            if(FuelTypeWindow.ShowDialog() == true)
                SetFuelPrice(FuelTypeWindow.GetChosenFuel(), FuelTypeWindow.GetChosenFuelText());
        }

        public void SetFuelPrice(decimal price, string text)
        {
            calculator = new Calculate(price);
            
            string ContentValue = String.Concat("Aktualny typ: ", text);
            TypeNameLabel.Content = String.Concat(ContentValue, ", " + string.Format("{0:C}", calculator.GetFuelPrice()));
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxDistanceToTravel.Text = "";
            TextBoxAmountOfFuel.Text = "";
            labelResult.Content = "";
            kilometerCost.Content = "";
            TextBoxDistanceToTravel.Focus();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
