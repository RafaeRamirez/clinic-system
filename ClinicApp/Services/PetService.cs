using System;
using System.Collections.Generic;
using VetClinic.Interfaces;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public class PatientService : IRegistrable
    {


        public void Register()

        {
            Console.WriteLine("\n=== Register Patient ===");

            Console.Write("Enter Patient ID: ");
            int id = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Address: ");
            string address = Console.ReadLine()!;

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine()!;

            Patient patient = new Patient(id, name, age, address, phone);
            patient.Register();

            Logger.LogInfo($"Patient {patient.Name} registered successfully.");

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
