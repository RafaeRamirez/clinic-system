using System;
using ClinicApp.Interfaces;

namespace ClinicApp.Models
{
    public class Pet : Animal, IRegistrable
    {
        public int PetId { get; private set; }
        public int OwnerId { get; private set; }
        public string Breed { get; set; }
        public string OwnerName { get; set; }

        public Pet(int id, string name, int age, string species, string breed, int ownerId, string ownerName)
            : base(name, age, species)
        {
            PetId = id;
            Breed = breed;
            OwnerId = ownerId;
            OwnerName = ownerName;
        }

        public override void MakeSound()
        {
            switch (Species.ToLower())
            {
                case "perro": Console.WriteLine($"{Name} dice Guau!"); break;
                case "gato": Console.WriteLine($"{Name} dice Miau!"); break;
                case "pajaro": Console.WriteLine($"{Name} dice Pío!"); break;
                default: Console.WriteLine($"{Name} hace un sonido desconocido."); break;
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"PetID: {PetId}, Nombre: {Name}, Especie: {Species}, Raza: {Breed}, DueñoID: {OwnerId}, Dueño: {OwnerName}");
        }

        public void Register()
        {
            Console.WriteLine($"Mascota {Name} (ID: {PetId}) registrada para paciente {OwnerName} (ID: {OwnerId}).");
        }
    }
}
