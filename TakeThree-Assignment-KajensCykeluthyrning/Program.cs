using TakeThree_Assignment_KajensCykeluthyrning;
using System.Globalization;
Console.OutputEncoding = System.Text.Encoding.UTF8; //så coola emoticons syns hihebehehe

CultureInfo.CurrentCulture = new CultureInfo("sv-SE");

var service = new RentalService();
bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("⸜(｡˃ ᵕ ˂ )⸝** Kajens Cykeluthyrning **⸜(｡˃ ᵕ ˂ )⸝");
    Console.WriteLine("1. Visa fordon 2. Registera kund 3. Hyr ut");
    Console.WriteLine("4. Lämna tillbaka 5. Ladda 6. Avsluta");
    Console.WriteLine("Gör ett val: ");

    switch (Console.ReadLine())
    {
        case "1":
            ShowVehicles();
            break;
        case "2":
            RegisterCustomer();
            break;
        case "3":
            RentVehicle();
            break;
        case "4":
            ReturnVehicle();
            break;
        case "5":
            ChargeVehicle();
            break;
        case "6":
            running = false;
            break;
        default:
            Console.WriteLine("Ogiltigt val.. Försök igen.");
            break;
    }
}

void ShowVehicles()
{
    Console.WriteLine($"{"Nr",-4}{"Typ",-10}{"Status",-8}Batteri");

    foreach (Vehicle vehicle in service.Vehicles)
    {
        string status = vehicle.IsAvailable ? "ledig" : "uthyrd";
        Console.WriteLine($"{vehicle.Number,-4}{vehicle.TypeName,-10}{status,-8}{vehicle.BatteryText}");
    }
}

void RegisterCustomer()
{
    Console.Write("Name: ");
    string name = Console.ReadLine() ?? "";
    int age = ReadPositiveInt("Ålder: ");

    if (age < Customer.MinimumAge)
    {
        Console.WriteLine($"Oj! Man måste vara minst {Customer.MinimumAge} år. Programmet Avslutas. ");
        running = false;  //mer av en graceful exit än enviorment learn frome xperience
        return;
    }

    Console.Write("Registera som medlem? j/n: ");
    string answer = (Console.ReadLine() ?? "").Trim().ToLower();
    bool isMember = answer == "j" || answer == "ja";

    service.RegisterCustomer(new Customer(name, age, isMember));
    Console.WriteLine($"Kund {name} registrerad!");
    //sätta spärr på medlemskap pga. varför ska man kunna regga sig ifall man inte kan hyra ett fordon??? FAST DE FUNKST JU INTE edit: jo nu fick ändra o fixa istället för Enviorment kör running false
}


void RentVehicle()
{
    Console.Write("Kund: ");
    string name = Console.ReadLine() ?? "";
    int vehicleNumber = ReadPositiveInt("Fordonsnummer: ");
    int hours = ReadPositiveInt("Antal timmar: ");

    Console.WriteLine(service.Rent(name, vehicleNumber, hours));
}

void ReturnVehicle()
{
    int vehicleNumber = ReadPositiveInt("Fordonsnummer: ");
    Console.WriteLine(service.Return(vehicleNumber));
}

void ChargeVehicle()
{
    int vehicleNumber = ReadPositiveInt("Fordonsnummer: ");
    Console.WriteLine(service.Charge(vehicleNumber));
}

int ReadPositiveInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
        {
            return value;
        }
        Console.WriteLine("Prova ett heltal större än 0! ");
    }
}