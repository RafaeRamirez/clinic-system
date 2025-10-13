using System;

namespace VetClinic.Models
{
    // Represents a pet owned by a patient in the veterinary system
    public class Pet(int id, string name, int age, string species, string breed, int patientId, string? symptom = null)
        : Animal(name, age, species, symptom)
    {
        // Private fields for internal use only
        private readonly int id = id;             // Unique identifier for the pet
        private readonly string breed = breed;     // Breed of the pet
        private readonly int patientId = patientId; // ID of the pet's owner (patient)

        // Public read-only properties
        public int Id => id;
        public string Breed => breed;
        public int PatientId => patientId;

        // Makes a sound depending on the species of the pet
        public override void MakeSound()
        {
            switch (Species)
            {
                case "Gato":
                    Console.WriteLine("Miau");
                    break;
                case "Perro":
                    Console.WriteLine("Guau");
                    break;
                default:
                    Console.WriteLine("Some generic animal sound");
                    break;
            }
        }

        // Displays detailed information about the pet
        public override void ShowInfo()
        {
            Console.WriteLine($"🐶 Pet: {Name}, Species: {Species}, Breed: {Breed}, Age: {Age}, Symptom: {Symptom ?? "None"}");
        }
    }
}
