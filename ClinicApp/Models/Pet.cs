using System;

namespace VetClinic.Models
{
    public class Pet
    {
        public int Id { get; }
        public string Name { get; }
        public int Age { get; }
        public string Species { get; }
        public string Breed { get; }
        public string OwnerName { get; }
        public string? Symptom { get; }

        public Pet(int id, string name, int age, string species, string breed, string ownerName, string? symptom = null)
        {
            Id = id;
            Name = name;
            Age = age;
            Species = species;
            Breed = breed;
            OwnerName = ownerName;
            Symptom = symptom;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"🐶 Pet: {Name}, Species: {Species}, Breed: {Breed}, Age: {Age}, Symptom: {Symptom ?? "None"}");
        }
    }
}
