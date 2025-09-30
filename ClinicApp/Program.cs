using System;
using ClinicApp.Models;     
using ClinicApp.Services;   
using ClinicApp.Exceptions; 
using ClinicApp.Utils;      
namespace ClinicApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== Veterinary Clinic System ===");

                // Ingreso de datos del paciente
                Console.Write("Enter patient name: ");
                string patientName = Console.ReadLine()!;

                Console.Write("Enter patient age: ");
                int patientAge = int.Parse(Console.ReadLine()!);

                Console.Write("Enter patient address: ");
                string patientAddress = Console.ReadLine()!;

                Console.Write("Enter patient phone: ");
                string patientPhone = Console.ReadLine()!;

                Patient patient = new Patient(patientName, patientAge, patientAddress, patientPhone);
                patient.Register();

                Console.WriteLine("\nHow many pets does this patient have?");
                int petCount = int.Parse(Console.ReadLine()!);

                for (int i = 0; i < petCount; i++)
                {
                    Console.WriteLine($"\n--- Pet {i + 1} ---");
                    Console.Write("Pet name: ");
                    string petName = Console.ReadLine()!;

                    Console.Write("Pet age: ");
                    int petAge = int.Parse(Console.ReadLine()!);

                    Console.Write("Pet species (Dog, Cat, Bird...): ");
                    string petSpecies = Console.ReadLine()!;

                    Console.Write("Pet breed: ");
                    string petBreed = Console.ReadLine()!;

                    Pet pet = new Pet(petName, petAge, petSpecies, petBreed, patient.Name);
                    patient.AddPet(pet);
                    pet.Register();
                }

                Console.WriteLine("\n--- Patient Information ---");
                patient.ShowInfo();

                Console.WriteLine("\n--- Pet Sounds (Polymorphism) ---");
                foreach (var pet in patient.Pets)
                {
                    pet.MakeSound();
                }

                Console.WriteLine("\n--- Veterinary Services ---");
                VeterinaryService checkup = new GeneralCheckup();
                VeterinaryService vaccination = new Vaccination();
                checkup.Attend();
                vaccination.Attend();

                // Ejemplo de búsqueda de mascota
                Console.Write("\nSearch a pet by name: ");
                string searchName = Console.ReadLine()!;

                Pet foundPet = patient.FindPet(searchName);
                Logger.LogInfo($"Pet found: {foundPet.Name}, Species: {foundPet.Species}");
            }
            catch (PetNotFoundException ex)
            {
                Logger.LogError("Pet search failed", ex);
            }
            catch (FormatException ex)
            {
                Logger.LogError("Invalid input format", ex);
            }
            catch (Exception ex)
            {
                Logger.LogError("Unexpected error occurred", ex);
            }
            finally
            {
                Logger.LogInfo("Program finished.");
            }
        }
    }
}
