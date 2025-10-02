using System;
using VetClinic.Interfaces;

namespace VetClinic.Models
{
    public class Pet : Animal 
    {
        public string Breed { get; set; }
        public string OwnerName { get; set; }

        public Pet(string name, int age, string species, string breed, string ownerName, string? symptoms = null)
            : base(name, age, species, symptoms)
        {
            Breed = breed;
            OwnerName = ownerName;
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

        public override void ShowInfo()
        {
            Console.WriteLine($"🐾 Pet: {Name}, Species: {Species}, Breed: {Breed}, Age: {Age}, Symptoms: {Symptoms}, Owner: {OwnerName}");
        }

    }
}
