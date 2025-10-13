using System;
using VetClinic.Services;

namespace VetClinic.Utils
{
    public static class VeterinarianMenu
    {
        // Displays the veterinarian management menu
        public static void Show(VeterinarianService veterinarianService)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n=== VETERINARIAN MENU ===");
                Console.WriteLine("1. Register veterinarian");
                Console.WriteLine("2. Show all veterinarians");
                Console.WriteLine("3. Edit veterinarian information");
                Console.WriteLine("4. Delete veterinarian record");
                Console.WriteLine("5. Return to main menu");
                Console.Write("👉 Option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // Register a new veterinarian
                        veterinarianService.Register();
                        break;

                    case "2":
                        // Display all registered veterinarians
                        veterinarianService.ShowVeterinarians();
                        break;

                    case "3":
                        // Edit veterinarian details
                        veterinarianService.EditVeterinarianById();
                        break;

                    case "4":
                        // Delete a veterinarian
                        veterinarianService.DeleteVeterinarianById();
                        break;

                    case "5":
                        // Go back to main menu
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
