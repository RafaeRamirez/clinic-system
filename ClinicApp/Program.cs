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
                Console.WriteLine("=== Sistema de Clínicas Veterinarias ===");


                Console.Write("Ingrese el nombre del paciente: ");
                string patientName = Console.ReadLine()!;

                Console.Write("Ingrese la edad del paciente: ");
                int patientAge = int.Parse(Console.ReadLine()!);

                Console.Write("Introduzca la dirección del paciente: ");
                string patientAddress = Console.ReadLine()!;

                Console.Write("Ingrese el teléfono del paciente: ");
                string patientPhone = Console.ReadLine()!;

                Patient patient = new Patient(patientName, patientAge, patientAddress, patientPhone);
                patient.Register();

                Console.WriteLine("\n¿Cuántas mascotas tiene este paciente?");
                int petCount = int.Parse(Console.ReadLine()!);

                for (int i = 0; i < petCount; i++)
                {
                    Console.WriteLine($"\n--- Mascota {i + 1} ---");
                    Console.Write("Nombre de la mascota:");
                    string petName = Console.ReadLine()!;

                    Console.Write("Edad de la mascota: ");
                    int petAge = int.Parse(Console.ReadLine()!);

                    Console.Write("Especies de mascotas (perro, gato, pájaro...): ");
                    string petSpecies = Console.ReadLine()!;

                    Console.Write("Raza de mascota: ");
                    string petBreed = Console.ReadLine()!;

                    Pet pet = new Pet(petName, petAge, petSpecies, petBreed, patient.Name);
                    patient.AddPet(pet);
                    pet.Register();
                }

                Console.WriteLine("\n--- Información del paciente ---");
                patient.ShowInfo();

                Console.WriteLine("\n--- Sonidos de mascotas ---");
                foreach (var pet in patient.Pets)
                {
                    pet.MakeSound();
                }

                Console.WriteLine("\n--- Servicios veterinarios ---");
                VeterinaryService checkup = new GeneralCheckup();
                VeterinaryService vaccination = new Vaccination();
                checkup.Attend();
                vaccination.Attend();

      
                Console.Write("\nBuscar una mascota por nombre: ");
                string searchName = Console.ReadLine()!;

                Pet foundPet = patient.FindPet(searchName);
                Logger.LogInfo($"Mascota encontrada: {foundPet.Name}, Especies: {foundPet.Species}");
            }
            catch (PetNotFoundException ex)
            {
                Logger.LogError("La búsqueda de mascotas falló", ex);
            }
            catch (FormatException ex)
            {
                Logger.LogError("Formato de entrada no válido", ex);
            }
            catch (Exception ex)
            {
                Logger.LogError("Se produjo un error inesperado", ex);
            }
            finally
            {
                Logger.LogInfo("Programa terminado.");
            }
        }
    }
}
