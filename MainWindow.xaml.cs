using System;
using System.Windows;

namespace FuelConsumption
{
    public partial class MainWindow : Window
    {
        decimal fuelPrice;
        FuelTypeWindow FuelTypeWindow;

        public MainWindow()
        {
            InitializeComponent();
            TextBoxDistanceToTravel.Focus();
            FuelTypeWindow = new FuelTypeWindow();
            SetFuelPrice(FuelTypeWindow.PricesInfoInit(), FuelTypeWindow.chosenFuelText);
            FuelTypeWindow.Close();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            Calculate calculator = new Calculate();
            double DistanceToTravel = Convert.ToDouble(TextBoxDistanceToTravel.Text); //TODO calculate exception
            double AmountOfFuel = Convert.ToDouble(TextBoxAmountOfFuel.Text);

            calculator.CalculateConsumption(DistanceToTravel, AmountOfFuel, fuelPrice);
            labelResult.Content = calculator.GetLabelResult();
            kilometerCost.Content = calculator.GetKilometerCost();
        }

        private void FuelTypeButton_Click(object sender, RoutedEventArgs e)
        {
            FuelTypeWindow = new FuelTypeWindow();
            if(FuelTypeWindow.ShowDialog() == true)
                SetFuelPrice(FuelTypeWindow.chosenFuel, FuelTypeWindow.chosenFuelText);
        }

        public void SetFuelPrice(decimal price, string text)
        {
            fuelPrice = price;
            string ContentValue = String.Concat("Aktualny typ: ", text);
            TypeNameLabel.Content = String.Concat(ContentValue, ", " + string.Format("{0:C}", price));
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
