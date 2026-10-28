namespace TakeThree_Assignment_KajensCykeluthyrning
{

    public class ElectricBike : Vehicle
    {
        private const int MinimumAge = 18;
        private const int MinimumBatteryToRent = 30;
        private const int BatteryUsePerHour = 10;


        public int BatteryLevel { get; private set; }

        public override string TypeName => "Elcykel";
        public override string BatteryText => $"{BatteryLevel}%";

        public ElectricBike(int number, int batteryLevel) : base(number, 70m)
        {
            BatteryLevel = Math.Clamp(batteryLevel, 0, 100);
        }
        public override string? GetRentalBlocker(Customer customer)
        {
            string? blocker = base.GetRentalBlocker(customer);
            if (blocker != null)
            {
                return blocker;
            }
            if (customer.Age < MinimumAge)
            {
                return $"Du måste vara minst {MinimumAge} år för att hyra en elcykel...";       //spärr
            }
            if (BatteryLevel < MinimumBatteryToRent)
            {
                return $"Elcykel {Number} har för lite batteri ({BatteryLevel}%). Ladda den först :) ";
            }
            return null;
        }

        public override void OnReturned(int hours)
        {
            BatteryLevel = Math.Max(0, BatteryLevel - hours * BatteryUsePerHour);
        }
        public void Charge()
        {
            BatteryLevel = 100;
        }
    }

}