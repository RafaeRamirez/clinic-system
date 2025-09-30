using System;
using ClinicApp.Interfaces;

namespace ClinicApp.Models
{
    public class Pet : Animal, IRegistrable
    {
        private string breed;
        private string owner;

        public string Breed { get => breed; set => breed = value; }
        public string Owner { get => owner; set => owner = value; }
        
        public Pet(string name, int age, string species, string breed, string owner)
            : base(name, age, species)
        {
            Breed = breed;
            Owner = owner;
        }

        public override void MakeSound()
        {
            switch (Species.ToLower())
            {
                case "dog": Console.WriteLine($"{Name} says Woof!"); break;
                case "cat": Console.WriteLine($"{Name} says Meow!"); break;
                case "bird": Console.WriteLine($"{Name} says Tweet!"); break;
                default: Console.WriteLine($"{Name} makes an unknown sound."); break;
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Pet: {Name}, Species: {Species}, Breed: {Breed}, Owner: {Owner}");
        }

        public void Register()
        {
            Console.WriteLine($"Pet {Name} registered for {Owner}.");
        }
    }
}
