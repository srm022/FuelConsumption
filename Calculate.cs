using System;
using System.Windows;

namespace FuelConsumption
{
    class Calculate
    {
        string labelResult, kilometerCost;
        decimal fuelPrice;

        public Calculate(decimal fuelPrice)
        {
            this.fuelPrice = fuelPrice;
        }

        public decimal GetFuelPrice()
        {
            return fuelPrice;
        }
        
        public void CalculateConsumption(string distanceInString, string amountInString)
        {
            try
            {
                double consumption = 0;
                double distanceToTravel = Convert.ToDouble(distanceInString);
                double amountOfFuel = Convert.ToDouble(amountInString);
                
                consumption = (amountOfFuel * 100) / distanceToTravel;  // Wyznaczenie średniego spalania

                if (double.IsInfinity(consumption) || !(distanceToTravel > 0) || !(amountOfFuel > 0))                     // Sprawdzenie, czy nie doszło do dzielenia przez zero
                {
                    MessageBox.Show("Wpisz wartości większe od 0.", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    EmptyFields();
                }
                else
                {
                    consumption = Math.Round(consumption, 2);           // Zaokrąglenie wyniku do dwóch miejsc po przecinku
                    labelResult = "Spalanie: " + Convert.ToString(consumption);
                    kilometerCost = "Koszt na kilometr: " + Convert.ToString(Math.Round((consumption * (double)fuelPrice) / 100, 2));
                }
            }
            catch (FormatException)                                     // Wprowadzone dane nie były liczbami
            {
                MessageBox.Show("Wpisane dane są w złym formacie!", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                EmptyFields();
            }
            catch (OverflowException)                                   // Wynik operacji poza zakresem double
            {
                MessageBox.Show("Dane poza dozwolonym zakresem.", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                EmptyFields();
            }
        }

        private void EmptyFields()
        {
            labelResult = "";
            kilometerCost = "";
        }

        public string GetLabelResult()
        {
            return labelResult;
        }

        public string GetKilometerCost()
        {
            return kilometerCost;
        }
    }
}
