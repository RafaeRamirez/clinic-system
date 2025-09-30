using System;
using ClinicApp.Models;
using ClinicApp.Exceptions;

namespace ClinicApp.Services
{
    public static class PetService
    {
        public static void AddPet(Patient patient)
        {
            Console.Write("ID de la mascota: ");
            int petId = int.Parse(Console.ReadLine()!);

            Console.Write("Nombre: ");
            string name = Console.ReadLine()!;

            Console.Write("Edad: ");
            int age = int.Parse(Console.ReadLine()!);

            Console.Write("Especie: ");
            string species = Console.ReadLine()!;

            Console.Write("Raza: ");
            string breed = Console.ReadLine()!;

            Console.Write("ID del paciente dueño: ");
            int ownerId = int.Parse(Console.ReadLine()!);

            if (ownerId != patient.PatientId)
            {
                Console.WriteLine($"⚠ El ID ingresado ({ownerId}) no corresponde al paciente activo (ID: {patient.PatientId}).");
                return;
            }

            Pet pet = new Pet(petId, name, age, species, breed, ownerId, patient.Name);
            patient.AddPet(pet);
            pet.Register();
        }

        public static void FindPetById(Patient patient)
        {
            Console.Write("Ingrese el ID de la mascota a buscar: ");
            int petId = int.Parse(Console.ReadLine()!);

            var pet = patient.FindPetById(petId);
            pet.ShowInfo();
        }

        public static void FindPetByName(Patient patient)
        {
            Console.Write("Ingrese el nombre de la mascota a buscar: ");
            string name = Console.ReadLine()!;

            var pet = patient.FindPetByName(name);
            pet.ShowInfo();
        }
    }
}
