namespace TakeThree_Assignment_KajensCykeluthyrning
{

    public abstract class Vehicle
    {
        public int Number { get; }
        public decimal HourlyPrice { get; }
        public bool IsAvailable { get; set; } = true;

        public abstract string TypeName { get; }   //namn på kvittooo

        public virtual string BatteryText => "";   //för batteri tomt för fordon utan batteri

        protected Vehicle(int number, decimal hourlyPrice)   // ärver from vehicle
        {
            Number = number;
            HourlyPrice = hourlyPrice;
        }


        public virtual string? GetRentalBlocker(Customer customer)
        {
            if (!IsAvailable)
            {
                return $"{TypeName} {Number} är redan hyrd...";
            }
            return null;    //ovan ger meddelande som säger att de inte går att hyra och om inte så ger den nischhhhh
        }

        public virtual void OnReturned(int hours)
        {
        }
    }


}