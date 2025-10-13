using System;

namespace VetClinic.Models
{
    // Represents a specific veterinary service: Vaccination
    public class Vaccination : VeterinaryService
    {
        // Constructor: defines this service as "Vaccination"
        public Vaccination() : base("Vacunación") { }

        // Executes the vaccination procedure logic
        public override void Attend()
        {
            Console.WriteLine("Aplicando la vacuna a la mascota...");
        }
    }
}
