namespace TakeThree_Assignment_KajensCykeluthyrning {
    public class Rental
    {
        private const decimal MemberDiscountRate = 0.10m;

        public Customer Customer { get; }
        public Vehicle Vehicle { get; }
        public int Hours { get; }

        public Rental(Customer customer, Vehicle vehicle, int hours)
        {
            Customer = customer;
            Vehicle = vehicle;
            Hours = hours;
        }

        public decimal BasePrice => Vehicle.HourlyPrice * Hours;
        public decimal Discount => Customer.IsMember ? BasePrice * MemberDiscountRate : 0m;
        public decimal TotalPrice => BasePrice - Discount;

        public string CreateReceipt()
        {
            string receipt = "KVITTO\n" +
                $"{Vehicle.TypeName}, {Hours} timmar, {Vehicle.HourlyPrice:F0} KR/h,  {BasePrice:F2} KR";

            if (Customer.IsMember)
            {
                receipt += $"\nMedlemsrabatt (10%)  -{Discount:F2} KR";
            }

            receipt += $"\nAtt betala {TotalPrice:F2} KR";
            return receipt;
        }
    }

}