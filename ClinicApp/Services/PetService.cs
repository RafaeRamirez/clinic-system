using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Interfaces;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    // Handles registration, editing, searching, and deletion of pets
    public class PetService : IRegistrable
    {
        private readonly PatientService patientService; // Link to patient service
        private int count = 0; // Tracks pet IDs

        // Constructor receives the PatientService reference
        public PetService(PatientService patientService)
        {
            this.patientService = patientService;
        }

        // Registers a new pet and associates it with an existing patient
        public void Register()
        {
            Console.WriteLine("\n=== Registrar mascota para paciente existente ===");

            var patients = patientService.GetPatients();
            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No hay pacientes disponibles. Por favor, registre a un paciente primero.");
                return;
            }

            Console.Write("Introduzca el ID del paciente:");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("⚠ Identificación inválida.");
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
                Console.WriteLine("⚠ Ya existe una mascota con esta ID en el sistema.");
                return;
            }

            Console.Write("Introduzca el nombre de la mascota: ");
            string petName = Console.ReadLine()!;

            Console.Write("Ingrese la edad de la mascota:");
            if (!int.TryParse(Console.ReadLine(), out int petAge))
            {
                Console.WriteLine("⚠ Edad no válida.");
                return;
            }

            Console.Write("Introduzca la especie de mascota: ");
            string species = Console.ReadLine()!;

            Console.Write("Introduzca la raza de la mascota: ");
            string breed = Console.ReadLine()!;

            Console.Write("Introduzca el síntoma (opcional): ");
            string? symptomInput = Console.ReadLine();
            string? symptom = string.IsNullOrWhiteSpace(symptomInput) ? null : symptomInput.ToLower();

            var pet = new Pet(petId, petName, petAge, species, breed, patient.Id, symptom);
            patient.Pets.Add(pet);

            // Save all updated data
            var allPatients = patientService.GetPatients();
            var allPets = allPatients.SelectMany(p => p.Pets).ToList();
            DatabaseSimulator.SaveData(allPatients, new List<Veterinarian>(), new List<Appointment>(), allPets);

            Console.WriteLine("\nMascota registrado exitosamente:");
            pet.ShowInfo();
            Logger.LogInfo($"Mascota {pet.Id} ({pet.Name}) Registrado para el paciente{patient.Name}.");
        }

        // Makes every registered pet "speak"
        public void MakeAllPetsSound()
        {
            Console.WriteLine("\n=== Sonidos de mascotas===");

            var patients = patientService.GetPatients();
            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No hay pacientes registrados..");
                return;
            }

            bool hasPets = false;
            foreach (var patient in patients)
            {
                if (patient.Pets.Count == 0) continue;

                Console.WriteLine($"\n👤 Paciente:  {patient.Name}");
                foreach (var pet in patient.Pets)
                {
                    Console.Write($"🐾 {pet.Name} dice: ");
                    pet.MakeSound();
                }
                hasPets = true;
            }

            if (!hasPets)
                Console.WriteLine("⚠ No se registran mascotas.");
        }

        // Edits pet data by its ID
        public void EditPetById()
        {
            var patients = patientService.GetPatients();

            Console.Write("\nIntroduzca el ID de la mascota a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int petId))
            {
                Console.WriteLine("⚠ ID no válido. Inténtalo de nuevo.");
                return;
            }

            Pet? petToEdit = null;
            Patient? owner = null;

            // Search the pet in all patients
            foreach (var patient in patients)
            {
                var foundPet = patient.Pets.FirstOrDefault(p => p.Id == petId);
                if (foundPet != null)
                {
                    petToEdit = foundPet;
                    owner = patient;
                    break;
                }
            }

            if (petToEdit == null || owner == null)
            {
                Console.WriteLine("⚠ No se encontró ninguna mascota con esa identificación.");
                return;
            }

            Console.WriteLine($"\nEdición de mascota: {petToEdit.Name}");
            Console.WriteLine($"Especies actuales: {petToEdit.Species}, Raza {petToEdit.Breed}, Edad: {petToEdit.Age}, Síntoma: {petToEdit.Symptom ?? "None"}");

            Console.Write("¿Cambiar nombre? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nuevo nombre: ");
                petToEdit.Name = Console.ReadLine()!;
            }

            Console.Write("¿Cambiar de edad? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("nueva edad : ");
                if (int.TryParse(Console.ReadLine(), out int newAge))
                    petToEdit.Age = newAge;
                else
                    Console.WriteLine("⚠ Edad no válida. Se mantiene el valor anterior..");
            }

            Console.Write("¿Cambiar de especie? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nueva especie: ");
                petToEdit.Species = Console.ReadLine()!;
            }

            Console.Write("¿Cambiar de raza? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nueva raza: ");
                string newBreed = Console.ReadLine()!;
                var field = typeof(Pet).GetField("breed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                field?.SetValue(petToEdit, newBreed);
            }

            Console.Write("¿Cambiar síntoma? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Write("Nuevo síntoma (déjelo en blanco para eliminarlo): ");
                string? newSymptom = Console.ReadLine();
                petToEdit.Symptom = string.IsNullOrWhiteSpace(newSymptom) ? null : newSymptom;
            }

            // Save updated data
            DatabaseSimulator.SaveData(
                patientService.GetPatients(),
                new List<Veterinarian>(),
                new List<Appointment>(),
                patients.SelectMany(p => p.Pets).ToList()
            );

            Console.WriteLine($"\nMascota {petToEdit.Name} ({petToEdit.Breed}) actualizado exitosamente.");
            Logger.LogInfo($"Mascota {petToEdit.Name} propiedad de {owner.Name} actualizado exitosamente");
        }

        // Deletes a pet by ID
        public void DeletePetById()
        {
            var patients = patientService.GetPatients();

            Console.Write("\nIntroduzca el ID de la mascota a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int petId))
            {
                Console.WriteLine("ID no válido, por favor inténtelo de nuevo.");
                return;
            }

            Pet? petToDelete = null;
            Patient? owner = null;

            // Find the pet
            foreach (var patient in patients)
            {
                var foundPet = patient.Pets.FirstOrDefault(p => p.Id == petId);
                if (foundPet != null)
                {
                    petToDelete = foundPet;
                    owner = patient;
                    break;
                }
            }

            if (petToDelete == null || owner == null)
            {
                Console.WriteLine(" No se encontró ninguna mascota con esa identificación.");
                return;
            }

            Console.Write($"\n¿Estás seguro de que quieres eliminar la mascota? '{petToDelete.Name}' del paciente'{owner.Name}'? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() != "y")
            {
                Console.WriteLine("Operación cancelada.");
                return;
            }

            // Remove the pet
            owner.Pets.Remove(petToDelete);

            DatabaseSimulator.SaveData(
                patientService.GetPatients(),
                new List<Veterinarian>(),
                new List<Appointment>(),
                patients.SelectMany(p => p.Pets).ToList()
            );

            Console.WriteLine($"\nMascota '{petToDelete.Name}' eliminado exitosamente");
            Logger.LogInfo($"Mascota'{petToDelete.Name}' del paciente'{owner.Name}' eliminado exitosamente.");
        }

        // Finds and displays pet information by its ID
        public void FindPetById()
        {
            var patients = patientService.GetPatients();

            Console.Write("Introduzca el ID de la mascota a buscar:");
            if (!int.TryParse(Console.ReadLine(), out int petId))
            {
                Console.WriteLine("ID no válido. Inténtalo de nuevo.");
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

            Console.WriteLine(" No se encontró ninguna mascota con esa identificación.");
        }
    }
}
