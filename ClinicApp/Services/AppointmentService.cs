using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    // Manages medical appointments between patients, pets, and veterinarians
    public class AppointmentService
    {
        private readonly List<Appointment> appointments = new(); // Stores all appointments
        private int count = 0; // Tracks unique appointment IDs
        private readonly VeterinarianService veterinarianService; // Reference to the veterinarian service

        // Constructor: links this service to the veterinarian service
        public AppointmentService(VeterinarianService vetService)
        {
            veterinarianService = vetService;
        }

        // Creates and schedules a new appointment
        public void ScheduleAppointment(List<Patient> patients)
        {
            Console.WriteLine("\n=== Schedule Appointment ===");

            // Ensure there are patients in the system
            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No patients registered.");
                return;
            }

            // Ask for a valid patient ID
            Console.Write("Enter patient ID: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("⚠ Invalid patient ID.");
                return;
            }

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine("⚠ Patient not found.");
                return;
            }

            // Validate that the patient owns at least one pet
            if (patient.Pets.Count == 0)
            {
                Console.WriteLine("⚠ This patient has no registered pets.");
                return;
            }

            // Ask for a valid pet ID
            Console.Write("Enter pet ID: ");
            if (!int.TryParse(Console.ReadLine(), out int petId))
            {
                Console.WriteLine("⚠ Invalid pet ID.");
                return;
            }

            var pet = patient.Pets.FirstOrDefault(p => p.Id == petId);
            if (pet == null)
            {
                Console.WriteLine("⚠ Pet not found.");
                return;
            }

            // Verify that there are registered veterinarians
            var veterinarians = veterinarianService.GetVeterinarians();
            if (veterinarians.Count == 0)
            {
                Console.WriteLine("⚠ No veterinarians available. Please register one first.");
                return;
            }

            // Ask for a valid veterinarian ID
            Console.Write("Enter veterinarian ID: ");
            if (!int.TryParse(Console.ReadLine(), out int vetId))
            {
                Console.WriteLine("⚠ Invalid veterinarian ID.");
                return;
            }

            var vet = veterinarians.FirstOrDefault(v => v.Id == vetId);
            if (vet == null)
            {
                Console.WriteLine("⚠ Veterinarian not found.");
                return;
            }

            // Validate date and time format
            Console.Write("Enter appointment date and time (format: yyyy-MM-dd HH:mm): ");
            string inputDate = Console.ReadLine()!;
            if (!DateTime.TryParseExact(inputDate, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime appointmentDate))
            {
                Console.WriteLine("⚠ Invalid date format. Use: yyyy-MM-dd HH:mm");
                return;
            }

            if (appointmentDate < DateTime.Now)
            {
                Console.WriteLine("⚠ You cannot schedule an appointment in the past.");
                return;
            }

            // Prevent scheduling conflicts
            bool conflictingAppointment = appointments.Any(a =>
                a.AppointmentDate == appointmentDate &&
                (a.VetName == vet.Name || (a.PatientId == patientId && a.PetId == petId))
            );

            if (conflictingAppointment)
            {
                Console.WriteLine("⚠ There is already an appointment scheduled at that time for this veterinarian or pet.");
                return;
            }

            // Ask for the reason for the appointment
            Console.Write("Enter appointment reason: ");
            string reason = Console.ReadLine()!;

            // Create and add the new appointment
            var appointment = new Appointment(++count, patientId, petId, appointmentDate, reason, vet.Name);
            appointments.Add(appointment);

            Console.WriteLine("\n✅ Appointment successfully scheduled!");
            appointment.ShowInfo();

            // Automatic notification for the patient
            patient.Notify($"Appointment scheduled for {appointmentDate:g} with Dr. {vet.Name}");
            Logger.LogInfo($"Appointment scheduled: Patient {patient.Name}, Dr. {vet.Name}, {appointmentDate:g}.");
        }

        // Displays all scheduled appointments
        public void ShowAppointments()
        {
            Console.WriteLine("\n=== Appointments List ===");

            if (appointments.Count == 0)
            {
                Console.WriteLine("⚠ No appointments registered.");
                return;
            }

            foreach (var a in appointments)
            {
                a.ShowInfo();
            }
        }
    }
}
