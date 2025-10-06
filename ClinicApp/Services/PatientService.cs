using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Interfaces;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public class PatientService : IRegistrable
    {
        private int count = 0;
        private readonly List<Patient> patients = [];

        private void Add(Patient patient)
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

        public void ShowPatients()
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

        public void Register()
        {
            Console.WriteLine("\n=== Registrar paciente ===");

            // Console.Write("Ingrese el ID del paciente: ");
            // if (!int.TryParse(Console.ReadLine(), out int id))
            // {
            //     Console.WriteLine(" ID inválida. Registro cancelado.");
            //     return;
            // }
            int id = count + 1;
            count++;

            Console.Write("Introducir nombre: ");
            string name = Console.ReadLine()!;

            Console.Write("Ingrese edad: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine(" Edad no válida. Registro cancelado.");
                return;
            }

            Console.Write("Introducir dirección: ");
            string address = Console.ReadLine()!;

            Console.Write("Ingresar Teléfono:");
            string phone = Console.ReadLine()!;


            Patient patient = new(id, name, age, address, phone);
            Add(patient);
            Logger.LogInfo($"Paciente registrado exitosamente: {patient}");
        }

        public List<Patient> GetPatients()
        {
            return patients;
        }
    }
}
