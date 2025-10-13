using System;

namespace VetClinic.Models
{
    // Represents a medical appointment in the veterinary system
    public class Appointment
    {
        // Unique identifier for the appointment
        public int Id { get; set; }

        // Identifier of the patient who owns the pet
        public int PatientId { get; set; }

        // Identifier of the pet attending the appointment
        public int PetId { get; set; }

        // Scheduled date and time for the appointment
        public DateTime AppointmentDate { get; set; }

        // Reason or purpose of the visit
        public string Reason { get; set; }

        // Name of the assigned veterinarian
        public string VetName { get; set; }

        // Constructor that initializes all appointment properties
        public Appointment(int id, int patientId, int petId, DateTime date, string reason, string vetName)
        {
            Id = id;
            PatientId = patientId;
            PetId = petId;
            AppointmentDate = date;
            Reason = reason;
            VetName = vetName;
        }

        // Displays appointment details in a readable format
        public void ShowInfo()
        {
            Console.WriteLine($"📅 ID: {Id} | Patient ID: {PatientId} | Pet ID: {PetId} | Date: {AppointmentDate:g} | Reason: {Reason} | Veterinarian: {VetName}");
        }
    }
}
