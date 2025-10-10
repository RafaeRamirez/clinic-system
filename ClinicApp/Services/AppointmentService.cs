using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public class AppointmentService
    {
        private readonly List<Appointment> appointments = new();
        private int count = 0;
        private readonly VeterinarianService veterinarianService;

        public AppointmentService(VeterinarianService vetService)
        {
            veterinarianService = vetService;
        }

        public void ScheduleAppointment(List<Patient> patients)
        {
            Console.WriteLine("\n=== Programar cita ===");

            if (patients.Count == 0)
            {
                Console.WriteLine("⚠ No hay pacientes registrados.");
                return;
            }

            Console.Write("Ingrese el ID del paciente: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("⚠ ID de paciente no válido.");
                return;
            }

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine("⚠ Paciente no encontrado.");
                return;
            }

            if (patient.Pets.Count == 0)
            {
                Console.WriteLine("⚠ Este paciente no tiene mascotas registradas.");
                return;
            }

            Console.Write("Ingrese el ID de la mascota: ");
            if (!int.TryParse(Console.ReadLine(), out int petId))
            {
                Console.WriteLine("⚠ ID de mascota no válido.");
                return;
            }

            var pet = patient.Pets.FirstOrDefault(p => p.Id == petId);
            if (pet == null)
            {
                Console.WriteLine("⚠ Mascota no encontrada.");
                return;
            }

            // ✅ Validar veterinario existente
            var veterinarians = veterinarianService.GetVeterinarians();
            if (veterinarians.Count == 0)
            {
                Console.WriteLine("⚠ No hay veterinarios registrados. Registre uno primero.");
                return;
            }

            Console.Write("Ingrese el ID del veterinario: ");
            if (!int.TryParse(Console.ReadLine(), out int vetId))
            {
                Console.WriteLine("⚠ ID de veterinario no válido.");
                return;
            }

            var vet = veterinarians.FirstOrDefault(v => v.Id == vetId);
            if (vet == null)
            {
                Console.WriteLine("⚠ Veterinario no encontrado.");
                return;
            }

            // ✅ Validar formato de fecha y hora
            Console.Write("Ingrese la fecha y hora de la cita (formato: yyyy-MM-dd HH:mm): ");
            string inputDate = Console.ReadLine()!;
            if (!DateTime.TryParseExact(inputDate, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime appointmentDate))
            {
                Console.WriteLine("⚠ Formato de fecha no válido. Use: yyyy-MM-dd HH:mm");
                return;
            }

            if (appointmentDate < DateTime.Now)
            {
                Console.WriteLine("⚠ No puedes programar una cita en el pasado.");
                return;
            }

            // 🚫 Validar si ya hay una cita en ese horario para ese veterinario o mascota
            bool conflictingAppointment = appointments.Any(a =>
                a.AppointmentDate == appointmentDate &&
                (a.VetName == vet.Name || (a.PatientId == patientId && a.PetId == petId))
            );

            if (conflictingAppointment)
            {
                Console.WriteLine("⚠ Ya existe una cita en esa fecha y hora con ese veterinario o para esa mascota.");
                return;
            }

            Console.Write("Introduzca el motivo de la cita: ");
            string reason = Console.ReadLine()!;

            // ✅ Crear y agregar la cita
            var appointment = new Appointment(++count, patientId, petId, appointmentDate, reason, vet.Name);
            appointments.Add(appointment);

            Console.WriteLine("\n✅ Cita programada con éxito!");
            appointment.ShowInfo();

            // ✅ Notificación automática
            patient.Notify($"Cita programada para {appointmentDate:g} con el Dr. {vet.Name}");
            Logger.LogInfo($"Cita programada para el paciente {patient.Name} con el Dr. {vet.Name} el {appointmentDate:g}.");
        }

        public void ShowAppointments()
        {
            Console.WriteLine("\n=== Lista de citas ===");

            if (appointments.Count == 0)
            {
                Console.WriteLine("⚠ No hay citas registradas.");
                return;
            }

            foreach (var a in appointments)
            {
                a.ShowInfo();
            }
        }
    }
}
