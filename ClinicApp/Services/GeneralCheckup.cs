using System;

namespace VetClinic.Models
{
    // This class represents a specific type of veterinary service: a general health checkup.
    // It inherits from the base class 'VeterinaryService'.
    public class GeneralCheckup : VeterinaryService
    {
        // Constructor: initializes the base class with the service name "General Checkup".
        public GeneralCheckup() : base("Chequeo general") { }

        // Overrides the abstract method 'Attend' from the base class.
        // Defines what happens when this service is performed.
        public override void Attend()
        {
            Console.WriteLine("Realizando un chequeo general de salud..");
        }
    }
}
