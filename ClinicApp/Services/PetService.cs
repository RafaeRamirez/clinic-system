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
        private int count = 0;
        private List<Patient> patients = patientServices.GetPatients();

        public void Register()
        {
            Console.WriteLine("\n=== Register Pet for Patient (by Patient ID) ===");

            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No hay pacientes disponibles. Por favor, registre a un paciente primero..");
                return;
            }

            Console.Write("Ingrese el ID del paciente:");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine(" Identificación inválida.");
                return;
            }

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine("⚠ Paciente no encontrada.");
                return;
            }

            // Console.Write("Ingrese el ID de la mascota: ");
            // if (!int.TryParse(Console.ReadLine(), out int petId))
            // {
            //     Console.WriteLine(" Identificación de mascota no válida.");
            //     return;
            // }
             int petId = count + 1;
            count++;

            if (patient.Pets.Any(p => p.Id == petId))
            {
                Console.WriteLine("⚠ Esta paciente ya tiene una mascota con esa identificación.");
                return;
            }

            Console.Write("Ingrese el nombre de la mascota:");
            string petName = Console.ReadLine()!;

            Console.Write("Ingrese la edad de la mascota: ");
            if (!int.TryParse(Console.ReadLine(), out int petAge))
            {
                Console.WriteLine(" Edad no válida para la mascota.");
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

            Console.WriteLine("\n Mascota registrada:");
            pet.ShowInfo();
            Logger.LogInfo($"Mascota {pet.Name} Registrada para la paciente {patient.Name}.");
        }
    }
}
