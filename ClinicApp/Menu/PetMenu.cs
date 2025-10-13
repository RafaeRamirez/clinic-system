using System;
using VetClinic.Services;

namespace VetClinic.Utils
{
    // Provides the interactive menu for managing pets
    public static class PetMenu
    {
        // Displays the pet management menu and handles user options
        public static void Show(PetService petService)
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n=== PET MENU ===");
                Console.WriteLine("1. Register pet");
                Console.WriteLine("2. Find pet by ID");
                Console.WriteLine("3. Edit pet information");
                Console.WriteLine("4. Delete pet record");
                Console.WriteLine("5. Make all pets sound");
                Console.WriteLine("6. Return to main menu");
                Console.Write("👉 Option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // Register a new pet
                        petService.Register();
                        break;

                    case "2":
                        // Find a specific pet by its ID
                        petService.FindPetById();
                        break;

                    case "3":
                        // Edit existing pet data
                        petService.EditPetById();
                        break;

                    case "4":
                        // Remove a pet record
                        petService.DeletePetById();
                        break;

                    case "5":
                        // Make all pets produce their characteristic sound
                        petService.MakeAllPetsSound();
                        break;

                    case "6":
                        // Return to the main system menu
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
