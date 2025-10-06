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
        public static DoctorService doctorService = new DoctorService(); 

        public static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                ShowMenu();
                string? option = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(option))
                {
                    Console.WriteLine("⚠ Por favor ingrese una opción válida (1-8).");
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
                            patientService.FindPatientById();
                            break;

                        case "4":
                           petService.FindPetById();
                            break;

                        case "5":
                            petService.MakeAllPetsSound();
                            break;

                        case "6":
                            doctorService.Register(); 
                            break;

                        case "7":
                            ShowDoctors(); 
                            break;

                        case "8":
                            running = false;
                            Console.WriteLine("👋 Saliendo del sistema...");
                            break;

                        default:
                            Console.WriteLine("⚠ Opción inválida, intenta de nuevo.");
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
                    Logger.LogInfo("Operación finalizada.");
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
            Console.WriteLine("6. Registrar doctor");       
            Console.WriteLine("7. Mostrar doctores");      
            Console.WriteLine("8. Salir");
            Console.Write("👉 Elige una opción: ");
        }

        private static void ShowDoctors()
        {
            var doctors = doctorService.GetDoctors();

            if (doctors.Count == 0)
            {
                Console.WriteLine("⚠ No hay doctores registrados aún.");
                return;
            }

            Console.WriteLine("\n=== Lista de Doctores Registrados ===");
            foreach (var doctor in doctors)
            {
                doctor.ShowInfo();
            }
        }
    }
}
