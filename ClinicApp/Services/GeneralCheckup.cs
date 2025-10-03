using System;

namespace VetClinic.Models
{
    public class GeneralCheckup : VeterinaryService
    {
        public GeneralCheckup() : base("Chequeo general") { }

        public override void Attend()
        {
            Console.WriteLine("Realizar un chequeo general de salud...");
        }
    }
}
