using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Interfaces;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    // Handles all patient management operations
    public class PatientService : IRegistrable
    {
        private int count = 0; // Tracks the number of patients
        private readonly List<Patient> patients; // List of all patients
        private readonly List<Veterinarian> veterinarians; // Reference to all veterinarians
        private readonly List<Appointment> appointments; // Reference to all appointments
        private readonly List<Pet> pets; // Reference to all pets

        // Constructor initializes patient service with existing lists
        public PatientService(
            List<Patient> patients,
            List<Veterinarian> veterinarians,
            List<Appointment> appointments,
            List<Pet> pets)
        {
            this.patients = patients;
            this.veterinarians = veterinarians;
            this.appointments = appointments;
            this.pets = pets;
            count = patients.Count;
        }

        // Adds a new patient and saves to simulated database
        private void Add(Patient patient)
        {
            if (patients.Any(p => p.Id == patient.Id))
            {
                Console.WriteLine("⚠ Un paciente con esta identificación ya existe..");
                return;
            }

            patients.Add(patient);
            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);

            Console.WriteLine($"Paciente {patient.Id} {patient.Name} añadido exitosamente");
            Logger.LogInfo($"Paciente {patient.Name} añadido a la lista.");
        }

        // Displays all registered patients
        public void ShowPatients()
        {
            Console.WriteLine("\n=== Lista de pacientes ===");

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

        // Registers a new patient via console input
        public void Register()
        {
            Console.WriteLine("\n=== Registrar nuevo paciente ===");

            int id = ++count;

            Console.Write("Introduzca el nombre: ");
            string name = Console.ReadLine()!;

            Console.Write("Introduzca la edad:");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("⚠ Edad no válida. Registro cancelado..");
                return;
            }

            Console.Write("Introducir dirección: ");
            string address = Console.ReadLine()!;

            Console.Write("Introduzca el número de teléfono: ");
            string phone = Console.ReadLine()!;

            Patient patient = new(id, name, age, address, phone);
            Add(patient);
            Logger.LogInfo($"Paciente registrado con éxito: {patient.Id} {patient.Name}");
        }

        // Returns list of all patients (used by other services)
        public List<Patient> GetPatients() => patients;

        // Edits patient data by ID
        public void EditPatientById()
        {
            Console.Write("\nIntroduzca el ID del paciente a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("⚠ ID no válida, por favor inténtelo de nuevo.");
                return;
            }

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine("⚠ Ningún paciente encontrado con esa identificación.");
                return;
            }

            Console.WriteLine($"\nEdición de paciente: {patient.Name}");
            Console.WriteLine($"Edad actual: {patient.Age}, DIRECCIÓN:{patient.Address}, Teléfono: [PROTECTED]");

            Console.Write("¿Cambiar nombre? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nuevo nombre: ");
                patient.Name = Console.ReadLine()!;
            }

            Console.Write("¿Cambiar de edad? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nueva edad ");
                if (int.TryParse(Console.ReadLine(), out int newAge))
                    patient.Age = newAge;
                else
                    Console.WriteLine("⚠ Edad no válida. Se mantiene el valor anterior.");
            }

            Console.Write("¿Cambiar de dirección? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nueva dirección: ");
                patient.Address = Console.ReadLine()!;
            }

            Console.Write("¿Cambiar número de teléfono? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nuevo teléfono:");
                patient.Phone = Console.ReadLine()!;
            }

            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);

            Console.WriteLine($"\nPaciente{patient.Name}actualizado exitosamente");
            Logger.LogInfo($"Paciente{patient.Name}actualizado exitosamente");
        }

        // Deletes a patient and their related data
        public void DeletePatientById()
        {
            Console.Write("\nIntroduzca el ID del paciente a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("⚠ ID no válida, por favor inténtelo de nuevo.");
                return;
            }

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine("Ningún paciente encontrado con esa identificación.");
                return;
            }

            Console.Write($"\n¿Estás seguro de que quieres eliminar al paciente? {patient.Name}? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() != "y")
            {
                Console.WriteLine("Operación cancelada. No se eliminaron registros.");
                return;
            }

            // Remove patient, pets, and appointments
            patients.Remove(patient);
            pets.RemoveAll(p => p.PatientId == patientId);
            appointments.RemoveAll(a => a.PatientId == patient.Id);

            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);

            Console.WriteLine($"Paciente {patient.Name} eliminado exitosamente");
            Logger.LogInfo($"Paciente{patient.Name} y registros relacionados eliminados.");
        }

        // Finds and displays patient by ID
        public void FindPatientById()
        {
            Console.Write("Introduzca el ID del paciente para buscar: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("⚠ ID no válido. Inténtalo de nuevo.");
                return;
            }

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient != null)
            {
                Console.WriteLine("\n👤 Paciente encontrado:");
                patient.ShowInfo();
            }
            else
            {
                Console.WriteLine("⚠ Ningún paciente encontrado con esa identificación.");
            }
        }
    }
}
