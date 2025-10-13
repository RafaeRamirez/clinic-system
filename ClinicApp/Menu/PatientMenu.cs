using System;
using VetClinic.Services;

namespace VetClinic.Utils
{
    public static class PatientMenu
    {
        // Displays the patient management menu
        public static void Show(PatientService patientService)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n=== PATIENT MENU ===");
                Console.WriteLine("1. Register new patient");
                Console.WriteLine("2. Show all patients");
                Console.WriteLine("3. Find patient by ID");
                Console.WriteLine("4. Edit patient information");
                Console.WriteLine("5. Delete patient record");
                Console.WriteLine("6. Return to main menu");
                Console.Write("👉 Option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // Register a new patient
                        patientService.Register();
                        break;

                    case "2":
                        // Show all patients
                        patientService.ShowPatients();
                        break;

                    case "3":
                        // Find patient by ID
                        patientService.FindPatientById();
                        break;

                    case "4":
                        // Edit an existing patient
                        patientService.EditPatientById();
                        break;

                    case "5":
                        // Delete a patient record
                        patientService.DeletePatientById();
                        break;

                    case "6":
                        // Exit to main menu
                        back = true;
                        break;

                    default:
                        Console.WriteLine("⚠ Invalid option.");
                        break;
                }
            }
        }
    }
}
