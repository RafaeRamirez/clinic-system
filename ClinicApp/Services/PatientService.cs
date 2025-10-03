using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public class PatientService
    {
        public Patient? Register()
        {
            Console.WriteLine("\n=== Register Patient ===");

            Console.Write("Enter Patient ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ Invalid ID. Registration aborted.");
                return null!;
            }

            Console.Write("Enter Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter Age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("❌ Invalid age. Registration aborted.");
                return null!;
            }

            Console.Write("Enter Address: ");
            string address = Console.ReadLine()!;

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine()!;

            Patient patient = new(id, name, age, address, phone);
            patient.Register();
            Logger.LogInfo($"Patient {patient.Name} registered successfully.");
            return patient;
        }

        public void Add(List<Patient> patients, Patient patient)
        {
            if (patients.Any(p => p.Id == patient.Id))
            {
                Console.WriteLine("⚠ A patient with this ID already exists.");
                return;
            }

            patients.Add(patient);
            Console.WriteLine($"✅ Patient {patient.Name} added successfully.");
            Logger.LogInfo($"Patient {patient.Name} added to the list.");
        }

        public void ShowPatients(List<Patient> patients)
        {
            Console.WriteLine("\n=== List of Patients ===");

            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No patients registered.");
                return;
            }

            foreach (var patient in patients)
            {
                patient.ShowInfo();
            }
        }
    }
}
