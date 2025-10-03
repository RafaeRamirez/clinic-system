using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Interfaces;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public class PetService : IRegistrable
    {
        private readonly List<Patient> _patients;

        public PetService(List<Patient> patients)
        {
            _patients = patients;
        }

        public void Register()
        {
            Console.WriteLine("\n=== Register Pet for Patient (by Patient ID) ===");

            if (_patients.Count == 0)
            {
                Console.WriteLine("⚠ No patients available. Please register a patient first.");
                return;
            }

            Console.Write("Enter Patient ID: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("❌ Invalid ID.");
                return;
            }

            var patient = _patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine("⚠ Patient not found.");
                return;
            }

            Console.Write("Enter Pet ID: ");
            if (!int.TryParse(Console.ReadLine(), out int petId))
            {
                Console.WriteLine("❌ Invalid Pet ID.");
                return;
            }

            if (patient.Pets.Any(p => p.Id == petId))
            {
                Console.WriteLine("⚠ This patient already has a pet with that ID.");
                return;
            }

            Console.Write("Enter Pet Name: ");
            string petName = Console.ReadLine()!;

            Console.Write("Enter Pet Age: ");
            if (!int.TryParse(Console.ReadLine(), out int petAge))
            {
                Console.WriteLine("❌ Invalid age for pet.");
                return;
            }

            Console.Write("Enter Pet Species: ");
            string species = Console.ReadLine()!;

            Console.Write("Enter Pet Breed: ");
            string breed = Console.ReadLine()!;

            Console.Write("Enter Symptom (optional): ");
            string? symptomInput = Console.ReadLine();
            string? symptom = string.IsNullOrWhiteSpace(symptomInput) ? null : symptomInput.ToLower();

            Pet pet = new(petId, petName, petAge, species, breed, patient.Name, symptom);
            patient.Pets.Add(pet);

            Console.WriteLine("\n✅ Registered Pet:");
            pet.ShowInfo();
            Logger.LogInfo($"Pet {pet.Name} registered for patient {patient.Name}.");
        }
    }
}
