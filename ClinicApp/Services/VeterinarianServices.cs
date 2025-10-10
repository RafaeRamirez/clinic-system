using System;
using System.Collections.Generic;
using VetClinic.Interfaces;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public class VeterinarianService : IRegistrable
    {
        private int count = 0;
        private readonly List<Patient> patients;
        private readonly List<Veterinarian> veterinarians;
        private readonly List<Appointment> appointments;
        private readonly List<Pet> pets;

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

        public List<Veterinarian> GetVeterinarians() => veterinarians;

        public void Register()
        {
            Console.WriteLine("\n=== Registrar veterinario ===");

            int veterinarianId = ++count;

            Console.Write("Ingrese el nombre del veterinario: ");
            string name = Console.ReadLine()!;

            Console.Write("Ingrese la especialidad: ");
            string specialty = Console.ReadLine()!;

            Console.Write("Ingrese el número de teléfono: ");
            string phone = Console.ReadLine()!;

            var veterinarian = new Veterinarian(veterinarianId, name, specialty, phone);

            veterinarians.Add(veterinarian);

            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);

            Console.WriteLine("\n✅ Veterinario registrado exitosamente!");
            veterinarian.ShowInfo();

            Logger.LogInfo($"Veterinario {veterinarian.Name} registrado con ID {veterinarian.Id}.");
        }

        public void ShowVeterinarians()
        {
            Console.WriteLine("\n=== Lista de veterinarios ===");

            if (veterinarians.Count == 0)
            {
                Console.WriteLine("⚠ No hay veterinarios registrados.");
                return;
            }

            foreach (var vet in veterinarians)
            {
                vet.ShowInfo();
            }
        }
    }
}
