using System;

namespace ClinicApp.Services
{

    public class GeneralCheckup : VeterinaryService
    {
        public GeneralCheckup() : base("General Checkup") { }

        public override void Attend()
        {
            Console.WriteLine("Performing a general checkup on the pet.");
        }
    }
}
