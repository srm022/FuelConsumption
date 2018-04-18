using FuelConsumption;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Spalanie
{
    public partial class MainWindow : Window
    {
        decimal fuelPrice;
        FuelType FuelTypeWindow;

        public MainWindow()
        {
            InitializeComponent();
            TextBoxDistanceToTravel.Focus();
            FuelTypeWindow = new FuelType();
            SetFuelPrice(FuelTypeWindow.PricesInfoInit(), FuelTypeWindow.chosenFuelText);
            FuelTypeWindow.Close();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            Calculate calculator = new Calculate();
            double DistanceToTravel = Convert.ToDouble(TextBoxDistanceToTravel.Text);
            double AmountOfFuel = Convert.ToDouble(TextBoxAmountOfFuel.Text);

            calculator.CalculateConsumption(DistanceToTravel, AmountOfFuel, fuelPrice);
            labelResult.Content = calculator.GetLabelResult();
            kilometerCost.Content = calculator.GetKilometerCost();
        }

        private void FuelTypeButton_Click(object sender, RoutedEventArgs e)
        {
            FuelTypeWindow = new FuelType();
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

        /*
        double paliwo, dystans, spalanie;
        try
        {
            // Wydobycie danych z linii edycyjnych, konwersja
            dystans = Convert.ToDouble(textBoxDistance.Text);
            paliwo = Convert.ToDouble(textBoxFuel.Text);
            // Wyznaczenie średniego spalania
            spalanie = (paliwo * 100) / dystans;
            // Sprawdzenie, czy nie doszło do dzielenia przez zero
            if (double.IsInfinity(spalanie))
                labelResult.Content = "Nieprawidłowy dystans";
            else
            {
                // Zaokrąglenie wyniku do dwóch miejsc po przecinku
                spalanie = Math.Round(spalanie, 2);
                labelResult.Content = "Spalanie: " + Convert.ToString(spalanie);
                kilometerCost.Content = "Koszt na kilometr: " + Convert.ToString(Math.Round((spalanie * (double)fuelPrice)/100, 2));
            }
        }
        catch (FormatException) // Wprowadzone dane nie były liczbami
        {
            labelResult.Content = "Niepoprawne dane";
        }
        catch (OverflowException) //  Wynik operacji poza zakresem double
        {
            labelResult.Content = "Dane poza dozwolonym zakresem";
        }*/



    }
}
