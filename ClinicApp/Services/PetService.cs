using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Interfaces;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public class PetService(PatientService patientServices) : IRegistrable
    {
        private int count = 0;  // contador único para todas las mascotas
        private List<Patient> patients = patientServices.GetPatients();

        public void Register()
        {
            Console.WriteLine("\n=== Registrar mascota para paciente (por ID de paciente) ===");

            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No hay pacientes disponibles. Por favor, registre a un paciente primero.");
                return;
            }

            Console.Write("Ingrese el ID del paciente: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine("⚠ Paciente no encontrado.");
                return;
            }

            // Incrementar el contador global y asignar ese valor como ID de la mascota
            count++;
            int petId = count;

            // Verificar que ninguna mascota en todo el sistema tenga ese ID (solo por seguridad)
            bool petExists = patients.Any(p => p.Pets.Any(pet => pet.Id == petId));
            if (petExists)
            {
                Console.WriteLine("⚠ Ya existe una mascota con ese ID en el sistema. Intente de nuevo.");
                return;
            }

            Console.Write("Ingrese el nombre de la mascota: ");
            string petName = Console.ReadLine()!;

            Console.Write("Ingrese la edad de la mascota: ");
            if (!int.TryParse(Console.ReadLine(), out int petAge))
            {
                Console.WriteLine("Edad no válida para la mascota.");
                return;
            }

            Console.Write("Introduzca la especie de mascota: ");
            string species = Console.ReadLine()!;

            Console.Write("Ingrese la raza de su mascota: ");
            string breed = Console.ReadLine()!;

            Console.Write("Ingrese el síntoma (opcional): ");
            string? symptomInput = Console.ReadLine();
            string? symptom = string.IsNullOrWhiteSpace(symptomInput) ? null : symptomInput.ToLower();

            Pet pet = new(petId, petName, petAge, species, breed, patient.Id, symptom);
            patient.Pets.Add(pet);

            Console.WriteLine("\nMascota registrada:");
            pet.ShowInfo();
            Logger.LogInfo($"Mascota {pet.Id} {pet.Name} registrada para el paciente {patient.Name}.");


        }


        public void MakeAllPetsSound()
        {
            Console.WriteLine("\n=== Sonidos de todas las mascotas ===");

            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No hay pacientes registrados.");
                return;
            }

            bool hasPets = false;

            foreach (var patient in patients)
            {
                if (patient.Pets.Count == 0) continue;

                Console.WriteLine($"\n👨 Paciente: {patient.Name}");

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
            var patients = patientServices.GetPatients();

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