using System;

namespace VetClinic.Models
{
    public class GeneralCheckup : VeterinaryService
    {
        public GeneralCheckup() : base("General Checkup") { }

        public override void Attend()
        {
            Console.WriteLine("Performing a general health checkup...");
        }
    }
}
