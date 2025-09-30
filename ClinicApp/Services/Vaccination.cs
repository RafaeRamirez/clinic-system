using System;

namespace ClinicApp.Services
{
    public class Vaccination : VeterinaryService
    {
        public Vaccination() : base("Vacunación") { }

        public override void Attend()
        {
            Console.WriteLine("Aplicando la vacuna correspondiente.");
        }
    }
}
