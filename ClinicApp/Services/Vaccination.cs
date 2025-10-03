using System;

namespace VetClinic.Models
{
    public class Vaccination : VeterinaryService
    {
        public Vaccination() : base("Vacunación") { }

        public override void Attend()
        {
            Console.WriteLine("Aplicación de la vacuna a la mascota...");
        }
    }
}
