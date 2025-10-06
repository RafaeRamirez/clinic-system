using System;

namespace VetClinic.Models
{
    public class Pet(int id, string name, int age, string species, string breed, int patientId, string? symptom = null) : Animal(name, age, species, symptom)
    {
        private readonly int id = id;
        private readonly string breed = breed;
        private readonly int patientId = patientId;

        public int Id => id;
        public string Breed => breed;
        public int PatientId => patientId;

        public override void MakeSound()
        {
            switch (Species)
            {
                case "Gato":
                    Console.WriteLine("Miau");
                    break;
                case "Perro":
                    Console.WriteLine("Guau");
                    break;
                default:
                    Console.WriteLine("Algún sonido");
                    break;
            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"🐶 Mascota: {Name}, Especie: {Species}, Raza: {Breed}, Edad: {Age}, Síntoma: {Symptom ?? "Ninguno"}");
        }
    }
}
