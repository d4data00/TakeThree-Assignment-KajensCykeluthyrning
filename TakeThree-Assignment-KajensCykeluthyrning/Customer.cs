namespace TakeThree_Assignment_KajensCykeluthyrning
{
    public class Customer
    {
        public const int MinimumAge = 15;
        public string Name { get; }
        public int Age { get; }
        public bool IsMember { get; }

        public Customer(string name, int age, bool isMember)
        {
            Name = name;

            Age = age;

            IsMember = isMember && age >= MinimumAge;
        }
    }

}