using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Interfaces;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    // Handles all operations related to veterinarians: registration, editing, deletion, and listing
    public class VeterinarianService : IRegistrable
    {
        private int count = 0; // Tracks the number of registered veterinarians
        private readonly List<Patient> patients; // References patient list (for saving)
        private readonly List<Veterinarian> veterinarians; // Stores all registered veterinarians
        private readonly List<Appointment> appointments; // References appointments (for saving)
        private readonly List<Pet> pets; // References pets (for saving)

        // Constructor initializes with existing data lists
        public VeterinarianService(
            List<Patient> patients,
            List<Veterinarian> veterinarians,
            List<Appointment> appointments,
            List<Pet> pets)
        {
            this.patients = patients;
            this.veterinarians = veterinarians;
            this.appointments = appointments;
            this.pets = pets;
            count = veterinarians.Count;
        }

        // Returns all registered veterinarians
        public List<Veterinarian> GetVeterinarians() => veterinarians;

        // Registers a new veterinarian
        public void Register()
        {
            Console.WriteLine("\n=== Register Veterinarian ===");

            int veterinarianId = ++count;

            Console.Write("Introduzca el nombre del veterinario: ");
            string name = Console.ReadLine()!;

            Console.Write("Introduzca la especialidad: ");
            string specialty = Console.ReadLine()!;

            Console.Write("Introduzca el número de teléfono: ");
            string phone = Console.ReadLine()!;

            var veterinarian = new Veterinarian(veterinarianId, name, specialty, phone);
            veterinarians.Add(veterinarian);

            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);

            Console.WriteLine("\n ¡Veterinario registrado exitosamente!");
            veterinarian.ShowInfo();
            Logger.LogInfo($"Veterinario{veterinarian.Name} registrado con ID {veterinarian.Id}.");
        }

        // Edits a veterinarian’s information by ID
        public void EditVeterinarianById()
        {
            Console.WriteLine("\n=== Editar Veterinario===");
            Console.Write("Ingrese el ID del veterinario: ");

            if (!int.TryParse(Console.ReadLine(), out int vetId))
            {
                Console.WriteLine("ID no válida, por favor inténtelo de nuevo.");
                return;
            }

            var vetToEdit = veterinarians.FirstOrDefault(v => v.Id == vetId);
            if (vetToEdit == null)
            {
                Console.WriteLine(" Veterinario no encontrado.");
                return;
            }

            Console.WriteLine($"\nEdición del Dr. {vetToEdit.Name}");
            Console.WriteLine($"Especialidad: {vetToEdit.Specialty}, Teléfono: {vetToEdit.Phone}");

            Console.Write("¿Cambiar nombre? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nuevo nombre: ");
                vetToEdit.Name = Console.ReadLine()!;
            }

            Console.Write("¿Cambiar de especialidad? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nueva especialidad: ");
                vetToEdit.Specialty = Console.ReadLine()!;
            }

            Console.Write("¿Cambiar número de teléfono? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nuevo teléfono: ");
                string newPhone = Console.ReadLine()!;
                vetToEdit.UpdatePhone(newPhone); // Uses method inside Veterinarian model
            }

            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);

            Console.WriteLine($" Veterinario {vetToEdit.Name} actualizado exitosamente.");
            Logger.LogInfo($"Veterinario {vetToEdit.Name} actualizado exitosamente");
        }

        // Deletes a veterinarian by ID
        public void DeleteVeterinarianById()
        {
            Console.Write("\nIngrese el ID del veterinario a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int vetId))
            {
                Console.WriteLine(" ID no válido. Inténtalo de nuevo.");
                return;
            }

            var vetToDelete = veterinarians.FirstOrDefault(v => v.Id == vetId);
            if (vetToDelete == null)
            {
                Console.WriteLine(" No se encontró ningún veterinario con esa ID");
                return;
            }

            Console.WriteLine($"\nFound Dr. {vetToDelete.Name}, Especialidad: {vetToDelete.Specialty}");
            Console.Write("¿Está seguro de que desea eliminar este registro? (y/n): ");
            string confirm = Console.ReadLine()!.Trim().ToLower();

            if (confirm != "y")
            {
                Console.WriteLine("Operación cancelada.");
                return;
            }

            veterinarians.Remove(vetToDelete);

            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);

            Console.WriteLine($"Veterinario {vetToDelete.Name} eliminado exitosamente");
            Logger.LogInfo($"Veterinaria {vetToDelete.Name} eliminado de la base de datos.");
        }

        // Displays all registered veterinarians
        public void ShowVeterinarians()
        {
            Console.WriteLine("\n=== Lista de veterinarios ===");

            if (veterinarians.Count == 0)
            {
                Console.WriteLine("No hay veterinarios registrados.");
                return;
            }

            foreach (var vet in veterinarians)
            {
                vet.ShowInfo();
            }
        }
    }
}
