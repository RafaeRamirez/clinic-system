using System;

namespace VetClinic.Models
{
    // Abstract base class for all animal types
    public abstract class Animal(string name, int age, string species, string? symptom = null)
    {
        // Private fields
        private string name = name;
        private int age = age;
        private string species = species;
        private string symptom = string.IsNullOrWhiteSpace(symptom) ? "None" : symptom;

        // Public properties
        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
        public string Species { get => species; set => species = value; }
        public string Symptom { get => symptom; set => symptom = value; }

        // Abstract method implemented by subclasses
        public abstract void MakeSound();

        // Displays basic animal information
        public virtual void ShowInfo()
        {
            Console.WriteLine($"Animal: {Name}, Age: {Age}, Species: {Species}, Symptoms: {Symptom}");
        }
    }
}
