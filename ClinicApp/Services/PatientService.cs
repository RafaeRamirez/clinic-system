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
            Console.WriteLine("\n=== Registrar paciente ===");

            Console.Write("Ingrese el ID del paciente: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine(" ID inválida. Registro cancelado.");
                return null!;
            }

            Console.Write("Introducir nombre: ");
            string name = Console.ReadLine()!;

            Console.Write("Ingrese edad: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine(" Edad no válida. Registro cancelado.");
                return null!;
            }

            Console.Write("Introducir dirección: ");
            string address = Console.ReadLine()!;

            Console.Write("Ingresar Teléfono:");
            string phone = Console.ReadLine()!;



            Patient patient = new(id, name, age, address, phone);
            patient.Register();

            Logger.LogInfo($"Paciente registrado exitosamente: {patient}");

            return patient;


        }

        public void Add(List<Patient> patients, Patient patient)
        {
            if (patients.Any(p => p.Id == patient.Id))
            {
                Console.WriteLine("⚠ Un paciente con esta identificación ya existe.");
                return;
            }

            patients.Add(patient);
            Console.WriteLine($" Paciente {patient.Name} añadido exitosamente");
            Logger.LogInfo($"Paciente {patient.Name} añadido a la lista.");
        }

        public void ShowPatients(List<Patient> patients)
        {
            Console.WriteLine("\n===Lista de pacientes ===");

            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No hay pacientes registrados.");
                return;
            }

            foreach (var patient in patients)
            {
                patient.ShowInfo();
            }
        }
    }
}
