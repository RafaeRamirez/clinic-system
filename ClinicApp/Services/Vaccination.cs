using System;

namespace ClinicApp.Services
{
    public class Vaccination : VeterinaryService
    {
        public Vaccination() : base("Vaccination") { }

        public override void Attend()
        {
            Console.WriteLine("Applying the corresponding vaccine to the pet.");
        }
    }
}

