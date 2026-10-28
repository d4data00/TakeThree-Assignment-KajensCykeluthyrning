using System.Xml.Linq;

namespace TakeThree_Assignment_KajensCykeluthyrning
{

    public class RentalService //känner bara till vehicle o anropar via vehicle.GetRental....
    {
        private readonly List<Vehicle> _vehicle = new();
        private readonly List<Customer> _customers = new();
        private readonly List<Rental> _activeRentals = new();


        public IReadOnlyList<Vehicle> Vehicles => _vehicle;

        public RentalService()
        {
            _vehicle.Add(new Bike(1));
            _vehicle.Add(new Bike(2));
            _vehicle.Add(new ElectricBike(3, 70));
            _vehicle.Add(new ElectricBike(4, 20));

        }
        public void RegisterCustomer(Customer customer)
        {
            _customers.Add(customer);
        }
        public string Rent(string customerName, int vehicleNumber, int hours)
        {
            Customer? customer = FindCustomer(customerName);

            if (customer == null)
            {
                return $"Kunden {customerName} finns inte...";
            }

            Vehicle? vehicle = FindVehicle(vehicleNumber);
            if (vehicle == null)
            {
                return $"Fordonet med nummer {vehicleNumber} finns inte...";
            }

            string? blocker = vehicle.GetRentalBlocker(customer);
            if (blocker != null)
            {
                return blocker;
            }
            vehicle.IsAvailable = false;
            _activeRentals.Add(new Rental(customer, vehicle, hours));
            return $"{customer.Name} hyr nu {vehicle.TypeName} nr. {vehicle.Number} i {hours} timmar, yay!";
        }

        public string Return(int vehicleNumber)
        {
            Rental? rental = _activeRentals.Find(r => r.Vehicle.Number == vehicleNumber);
            if (rental == null)
            {
                return $"Fordon {vehicleNumber} är inte hyrd";
            }

            rental.Vehicle.OnReturned(rental.Hours);
            rental.Vehicle.IsAvailable = true;
            _activeRentals.Remove(rental);
            return rental.CreateReceipt();

        }

        public string Charge(int vehicleNumber)
        {
            Vehicle? vehicle = FindVehicle(vehicleNumber);
            if (vehicle is not ElectricBike electricBike)
            {
                return $"Fordon {vehicleNumber} är ingen elcykel!";
            }
            if (!electricBike.IsAvailable)
            {
                return $"Elcykel {vehicleNumber} är uthyrd och kan inte laddas nu.";
            }

            electricBike.Charge();
            return $"Elcykel {vehicleNumber} är laddad till 100% :D";
        }
        private Customer? FindCustomer(string name)
        {
            return _customers.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private Vehicle? FindVehicle(int number)
        {
            return _vehicle.Find(ValueTask => ValueTask.Number == number);
        }
    }


}