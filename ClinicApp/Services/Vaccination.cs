using System;

namespace VetClinic.Models
{
    public class Vaccination : VeterinaryService
    {
        public Vaccination() : base("Vaccination") { }

        public override void Attend()
        {
            Console.WriteLine("Applying the vaccine to the pet...");
        }
    }
}
