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
        private readonly PatientService patientService;
        private int count = 0;

        public PetService(PatientService patientService)
        {
            this.patientService = patientService;
        }

        public void Register()
        {
            Console.WriteLine("\n=== Registrar mascota para paciente (por ID de paciente) ===");

            var patients = patientService.GetPatients();
            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No hay pacientes disponibles. Por favor, registre un paciente primero.");
                return;
            }

            Console.Write("Ingrese el ID del paciente: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("⚠ ID inválido.");
                return;
            }

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine("⚠ Paciente no encontrado.");
                return;
            }

            count++;
            int petId = count;

            bool petExists = patients.Any(p => p.Pets.Any(pet => pet.Id == petId));
            if (petExists)
            {
                Console.WriteLine("⚠ Ya existe una mascota con ese ID en el sistema.");
                return;
            }

            Console.Write("Ingrese el nombre de la mascota: ");
            string petName = Console.ReadLine()!;

            Console.Write("Ingrese la edad de la mascota: ");
            if (!int.TryParse(Console.ReadLine(), out int petAge))
            {
                Console.WriteLine("⚠ Edad no válida para la mascota.");
                return;
            }

            Console.Write("Ingrese la especie de la mascota: ");
            string species = Console.ReadLine()!;

            Console.Write("Ingrese la raza de la mascota: ");
            string breed = Console.ReadLine()!;

            Console.Write("Ingrese el síntoma (opcional): ");
            string? symptomInput = Console.ReadLine();
            string? symptom = string.IsNullOrWhiteSpace(symptomInput) ? null : symptomInput.ToLower();

            var pet = new Pet(petId, petName, petAge, species, breed, patient.Id, symptom);
            patient.Pets.Add(pet);

            //  Recolectar todos los datos para guardar correctamente
            var allPatients = patientService.GetPatients();
            var allPets = allPatients.SelectMany(p => p.Pets).ToList();
            var allVets = new List<Veterinarian>();
            var allAppointments = new List<Appointment>();

            DatabaseSimulator.SaveData(allPatients, allVets, allAppointments, allPets);

            Console.WriteLine("\n✅ Mascota registrada correctamente:");
            pet.ShowInfo();
            Logger.LogInfo($"Mascota {pet.Id} ({pet.Name}) registrada para el paciente {patient.Name}.");
        }

        public void MakeAllPetsSound()
        {
            Console.WriteLine("\n=== Sonidos de todas las mascotas ===");

            var patients = patientService.GetPatients();
            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No hay pacientes registrados.");
                return;
            }

            bool hasPets = false;

            foreach (var patient in patients)
            {
                if (patient.Pets.Count == 0) continue;

                Console.WriteLine($"\n👤 Paciente: {patient.Name}");
                foreach (var pet in patient.Pets)
                {
                    Console.Write($"🐾 {pet.Name} dice: ");
                    pet.MakeSound();
                }

                hasPets = true;
            }

            if (!hasPets)
            {
                Console.WriteLine("⚠ No hay mascotas registradas.");
            }
        }
        
        public void FindPetById()
        {
            var patients = patientService.GetPatients();

            Console.Write("Ingrese el ID de la mascota a buscar: ");
            if (!int.TryParse(Console.ReadLine(), out int petId))
            {
                Console.WriteLine("⚠ ID inválido. Intente nuevamente.");
                return;
            }

            foreach (var patient in patients)
            {
                var pet = patient.Pets.FirstOrDefault(p => p.Id == petId);
                if (pet != null)
                {
                    Console.WriteLine("\n🐾 Mascota encontrada:");
                    pet.ShowInfo();
                    return;
                }
            }

            Console.WriteLine("⚠ No se encontró ninguna mascota con ese ID.");
        }
    }
}
