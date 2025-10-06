using System;
using System.Collections.Generic;
using VetClinic.Models;
using VetClinic.Services;
using VetClinic.Exceptions;
using VetClinic.Utils;

namespace VetClinic
{
    public class Program
    {
        public static PatientService patientService = new PatientService();
        public static PetService petService = new PetService(patientService);

        public static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                ShowMenu();
                string? option = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(option))
                {
                    Console.WriteLine("⚠ Por favor ingrese una opción válida (1-7).");
                    continue;
                }

                try
                {
                    switch (option)
                    {
                        case "1":
                             patientService.Register();
                            break;

                        case "2":
                            petService.Register();
                            break;

                        case "3":
                            patientService.ShowPatients();
                            break;

                        case "4":
                            // Implementar búsqueda de mascota
                            break;

                        case "5":
                            // Implementar sonidos de mascotas
                            break;

                        case "6":
                            // Implementar servicios veterinarios
                            break;

                        case "7":
                            running = false;
                            Console.WriteLine("👋 Exiting system...");
                            break;

                        default:
                            Console.WriteLine("⚠ Invalid option, please try again.");
                            break;
                    }
                }
                catch (PetNotFoundException ex)
                {
                    Logger.LogError("Pet not found", ex);
                }
                catch (FormatException ex)
                {
                    Logger.LogError("Invalid input format", ex);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Unexpected error", ex);
                }
                finally
                {
                    Logger.LogInfo("Operation finished.");
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n=== Menú de la Clínica Veterinaria ===");
            Console.WriteLine("1. Registrar paciente");
            Console.WriteLine("2. Registrar mascota");
            Console.WriteLine("3. Mostrar pacientes");
            Console.WriteLine("4. Buscar mascota");
            Console.WriteLine("5. Hacer sonidos de mascotas");
            Console.WriteLine("6. Servicios veterinarios");
            Console.WriteLine("7. Exit");
            Console.Write("👉 Choose an option: ");
        }
    }
}
