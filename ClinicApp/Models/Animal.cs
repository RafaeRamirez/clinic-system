using System;

namespace VetClinic.Models
{

    public abstract class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Species { get; set; }
        public string Symptoms { get; set; }

        protected Animal(string name, int age, string species, string? symptoms = null)
        {
            Name = name;
            Age = age;
            Species = species;
            Symptoms = string.IsNullOrWhiteSpace(symptoms) ? "Ninguna" : symptoms;
        }

        public abstract void MakeSound();

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Animal: {Name}, Edad: {Age}, Especies: {Species}, Síntomas: {Symptoms}");
        }
    }
}
