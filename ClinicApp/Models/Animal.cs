using System;

namespace VetClinic.Models
{

    public abstract class Animal(string name, int age, string species, string? symptom = null)
    {
        private string name = name;
        private int age = age;
        private string species = species;
        private string symptom = string.IsNullOrWhiteSpace(symptom) ? "Ninguno" : symptom;

        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
        public string Species { get => species; set => species = value; }
        public string Symptom { get => symptom; set => symptom = value; }
        public abstract void MakeSound();

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Animal: {Name}, Edad: {Age}, Especies: {Species}, Síntomas: {Symptom}");
        }
    }
}
