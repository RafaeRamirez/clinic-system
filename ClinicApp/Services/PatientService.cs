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
        private readonly List<Patient> patients;
        private readonly List<Veterinarian> veterinarians;
        private readonly List<Appointment> appointments;
        private readonly List<Pet> pets;

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

        private void Add(Patient patient)
        {
            if (patients.Any(p => p.Id == patient.Id))
            {
                Console.WriteLine("⚠ Ya existe un paciente con esta identificación.");
                return;
            }

            patients.Add(patient);

            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);

            Console.WriteLine($" Paciente {patient.Id} {patient.Name} añadido exitosamente.");
            Logger.LogInfo($"Paciente {patient.Name}  añadido a la lista.");
        }

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

        public void Register()
        {
            Console.WriteLine("\n=== Registrar paciente ===");

            int id = ++count;

            Console.Write("Introducir nombre: ");
            string name = Console.ReadLine()!;

            Console.Write("Ingrese edad: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("⚠ Edad no válida. Registro cancelado.");
                return;
            }

            Console.Write("Introducir dirección: ");
            string address = Console.ReadLine()!;

            Console.Write("Ingresar teléfono: ");
            string phone = Console.ReadLine()!;

            Patient patient = new(id, name, age, address, phone);
            Add(patient);
            Logger.LogInfo($"Paciente registrado exitosamente: {patient.Id} {patient.Name}");
        }

        public List<Patient> GetPatients()
        {
            return patients;
        }

        public void EditPatientById()
        {
            Console.Write("\nIngrese el ID del paciente que desea editar: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("⚠ ID inválido, intente nuevamente.");
                return;
            }

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine("⚠ No se encontró ningún paciente con ese ID.");
                return;
            }

            Console.WriteLine($"\nEditando paciente: {patient.Name}");
            Console.WriteLine($"Edad actual: {patient.Age}, Dirección: {patient.Address}, Teléfono: [PROTEGIDO]");

            Console.Write("¿Desea cambiar el nombre? (s/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "s")
            {
                Console.Write("Nuevo nombre: ");
                patient.Name = Console.ReadLine()!;
            }

            Console.Write("¿Desea cambiar la edad? (s/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "s")
            {
                Console.Write("Nueva edad: ");
                if (int.TryParse(Console.ReadLine(), out int newAge))
                    patient.Age = newAge;
                else
                    Console.WriteLine("⚠ Edad no válida. Se mantiene la anterior.");
            }

            Console.Write("¿Desea cambiar la dirección? (s/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "s")
            {
                Console.Write("Nueva dirección: ");
                patient.Address = Console.ReadLine()!;
            }

            Console.Write("¿Desea cambiar el teléfono? (s/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "s")
            {
                Console.Write("Nuevo teléfono: ");
                patient.Phone = Console.ReadLine()!;
            }

            //  Guardar todos los datos actualizados
            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);

            Console.WriteLine($"\n Paciente {patient.Name} actualizado correctamente.");
            Console.WriteLine(" Los datos se han guardado en la base de datos simulada.");
            Logger.LogInfo($"Paciente {patient.Name} actualizado correctamente.");
        }

        public void DeletePatientById()
        {
            Console.Write("\nIngrese el ID del paciente que desea eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("ID inválido, intente nuevamente.");
                return;
            }
            var patient = patients.FirstOrDefault(p => p.Id == patientId);

            if (patient == null)
            {
                Console.WriteLine("No se encontró ningún paciente con ese ID.");
                return;
            }
            Console.WriteLine($"\n¿Está seguro de que desea eliminar al paciente {patient.Name}? (s/n): ");
            if (Console.ReadLine()?.Trim().ToLower() != "s")
            {
                Console.WriteLine("Operación cancelada. No se eliminó ningún registro.");
                return;
            }
            //  Eliminar al paciente y todas sus mascotas
            patients.Remove(patient);
            pets.RemoveAll(p => p.PatientId == patientId);
            appointments.RemoveAll(a => a.PatientId == patient.Id);

            // Guardar los cambios
            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);

            Console.WriteLine($" Paciente  {patient.Name} eliminado correctamente.");
            Logger.LogInfo($"Paciente  {patient.Name} y sus registros asociados fueron eliminados.");
        }
        public void FindPatientById()
        {
            Console.Write("Ingrese el ID del paciente a buscar: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("⚠ ID inválido. Intente nuevamente.");
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
                Console.WriteLine("⚠ No se encontró ningún paciente con ese ID.");
            }
        }
    }
}
