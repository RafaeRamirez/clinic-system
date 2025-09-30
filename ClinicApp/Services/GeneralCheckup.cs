using System;

namespace ClinicApp.Services
{
    public class GeneralCheckup : VeterinaryService
    {
        public GeneralCheckup() : base("Consulta General") { }

        public override void Attend()
        {
            Console.WriteLine("Realizando un chequeo general de salud.");
        }
    }
}
