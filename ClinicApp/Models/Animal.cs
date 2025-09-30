using System;

namespace ClinicApp.Models
{
    public class Animal
    {
        private string name;
        private int age;
        private string species;

        public string Name { get => name; set => name = value; }
        public int Age { get => age; set { if (value >= 0) age = value; } }
        public string Species { get => species; set => species = value; }

        public Animal(string name, int age, string species)
        {
            Name = name;
            Age = age;
            Species = species;
        }

        public virtual void MakeSound()
        {
            Console.WriteLine($"{Name} hace un sonido.");
        }
    }
}
