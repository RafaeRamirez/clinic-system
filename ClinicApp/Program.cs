using System;
using System.Collections.Generic;
using VetClinic.Models;
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic
{
    public class Program
    {
        public static void Main(string[] args)
        {
          // Load mock database
            var (patients, veterinarians, appointments, pets) = DatabaseSimulator.LoadData();

            // Initialize services
            var patientService = new PatientService(patients, veterinarians, appointments, pets);
            var veterinarianService = new VeterinarianService(patients, veterinarians, appointments, pets);
            var petService = new PetService(patientService);
            var appointmentService = new AppointmentService(veterinarianService);

            bool running = true;
            Console.WriteLine("\n✅ Sistema de Clínica Veterinaria listo para usar.");

            while (running)
            {
                Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Gestión de Pacientes");
                Console.WriteLine("2. Gestión de Mascotas");
                Console.WriteLine("3. Gestión de Veterinarios");
                Console.WriteLine("4. Gestión de Citas");
                Console.WriteLine("5. Guardar y salir");
                Console.Write("👉 Elige una opción: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        PatientMenu.Show(patientService);
                        break;

                    case "2":
                        PetMenu.Show(petService);
                        break;

                    case "3":
                        VeterinarianMenu.Show(veterinarianService);
                        break;

                    case "4":
                        AppointmentMenu.Show(appointmentService, patients);
                        break;

                    case "5":
                        Console.WriteLine("💾 Guardando datos...");
                        DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);
                        Console.WriteLine("👋 Saliendo del sistema...");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("⚠ Opción inválida. Intenta nuevamente.");
                        break;
                }
            }
        }
    }
}
