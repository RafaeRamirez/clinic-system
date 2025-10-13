using System;
using System.Collections.Generic;
using VetClinic.Interfaces;

namespace VetClinic.Models
{
    // Represents a clinic patient who owns one or more pets
    public class Patient : INotifiable
    {
        // Parameterless constructor (required for JSON deserialization)
        public Patient() { }

        // Main constructor used when creating a new patient
        public Patient(int id, string name, int age, string address, string phone)
        {
            Id = id;
            Name = name;
            Age = age;
            Address = address;
            Phone = phone;
            Pets = new List<Pet>();
        }

        // Public properties (needed for JSON serialization)
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public List<Pet> Pets { get; set; } = new();

        // Displays full patient information and their registered pets
        public void ShowInfo()
        {
            Console.WriteLine($"\n👤 Patient: {Name}, Age: {Age}, Address: {Address}, Phone: {Phone}");

            if (Pets.Count == 0)
            {
                Console.WriteLine("⚠ No pets registered.");
            }
            else
            {
                Console.WriteLine("🐾 Pets:");
                foreach (var pet in Pets)
                {
                    pet.ShowInfo();
                }
            }
        }

        // Sends a notification message to the patient
        public void Notify(string message)
        {
            Console.WriteLine($"🔔 Notification to {Name}: {message}");
        }

        // Returns a simple text summary of patient information
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Age: {Age}, Address: {Address}, Phone: {Phone}, Pets: {Pets.Count}";
        }
    }
}

