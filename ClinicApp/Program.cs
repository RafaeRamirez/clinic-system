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

        private static readonly List<Patient> patients = new List<Patient>();
        private static readonly PatientService patientService = new PatientService();

        public static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                ShowMenu();
                string option = Console.ReadLine()!;

                try
                {
                    switch (option)
                    {
                        case "1":
                            patientService.Register(); 
                  
                            break;

                        case "2":
                            // patientService.RegisterPetForPatientById(); 

                        case "3":
                            patientService.ShowPatients(patients);
                            break;

                        case "4":
                            //  patientService.FindPet();
                            break;

                        case "5":
                            //  patientService.MakePetsSounds();
                            break;

                        case "6":
                            //  patientService.ShowServices();
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
            Console.WriteLine("\n=== Veterinary Clinic Menu ===");
            Console.WriteLine("1. Register Patient");
            Console.WriteLine("2. Register Pet");
            Console.WriteLine("3. Show Patients");
            Console.WriteLine("4. Search Pet");
            Console.WriteLine("5. Make Pets Sounds");
            Console.WriteLine("6. Veterinary Services");
            Console.WriteLine("7. Exit");
            Console.Write(" Choose an option: ");
        }
    }
}
