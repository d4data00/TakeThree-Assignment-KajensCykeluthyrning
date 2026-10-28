namespace TakeThree_Assignment_KajensCykeluthyrning
{

    public class Bike : Vehicle
    {
        private const int MinimumAge = 15;
        public override string TypeName => "Cykel";
        public Bike(int number) : base(number, 40m)
        {
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
                return $"Du måste vara minst {MinimumAge} år för att hyra en cykel...";
            }
            return null;
        }


    }

}