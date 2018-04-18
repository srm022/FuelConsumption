using System;

namespace FuelConsumption
{
    class Calculate
    {
        string labelResult, kilometerCost;
        
        public void CalculateConsumption(double distanceToTravel, double amountOfFuel, decimal fuelPrice)
        {
            double consumption = 0;

            try
            {
                consumption = (amountOfFuel * 100) / distanceToTravel;  // Wyznaczenie średniego spalania

                if (double.IsInfinity(consumption))                     // Sprawdzenie, czy nie doszło do dzielenia przez zero
                    labelResult = "Nieprawidłowy dystans";
                else
                {
                    consumption = Math.Round(consumption, 2);           // Zaokrąglenie wyniku do dwóch miejsc po przecinku
                    labelResult = "Spalanie: " + Convert.ToString(consumption);
                    kilometerCost = "Koszt na kilometr: " + Convert.ToString(Math.Round((consumption * (double)fuelPrice) / 100, 2));
                }
            }
            catch (FormatException)                                     // Wprowadzone dane nie były liczbami
            {
                labelResult = "Niepoprawne dane";
            }
            catch (OverflowException)                                   // Wynik operacji poza zakresem double
            {
                labelResult = "Dane poza dozwolonym zakresem";
            }
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
