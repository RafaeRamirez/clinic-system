using System;
using System.Collections.Generic;
using VetClinic.Models;
using VetClinic.Services;

namespace VetClinic.Utils
{
    public static class AppointmentMenu
    {
        // Displays the appointment management menu
        public static void Show(AppointmentService appointmentService, List<Patient> patients)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n=== APPOINTMENT MENU ===");
                Console.WriteLine("1. Schedule new appointment");
                Console.WriteLine("2. Show all appointments");
                Console.WriteLine("3. Return to main menu");
                Console.Write("👉 Option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // Schedule a new appointment
                        appointmentService.ScheduleAppointment(patients);
                        break;

                    case "2":
                        // Display all registered appointments
                        appointmentService.ShowAppointments();
                        break;

                    case "3":
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
