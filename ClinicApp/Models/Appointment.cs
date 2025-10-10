using System;

namespace VetClinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }              
        public int PatientId { get; set; }        
        public int PetId { get; set; }             
        public DateTime AppointmentDate { get; set; } 
        public string Reason { get; set; }       
        public string VetName { get; set; }      

        public Appointment(int id, int patientId, int petId, DateTime date, string reason, string vetName)
        {
            Id = id;
            PatientId = patientId;
            PetId = petId;
            AppointmentDate = date;
            Reason = reason;
            VetName = vetName;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"📅 ID: {Id} | Paciente ID: {PatientId} | Mascota ID: {PetId} | Fecha: {AppointmentDate:g} | Motivo: {Reason} | Veterinario: {VetName}");
        }
    }
}
