using System;
using System.Collections.Generic;
using ClinicApp.Interfaces;
using ClinicApp.Exceptions;

namespace ClinicApp.Models
{
    public class Patient : IRegistrable, INotificable
    {
        public int PatientId { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        private string phone;
        public List<Pet> Pets { get; private set; } = new List<Pet>();

        public string Phone => "Protegido (confidencial)";

        public Patient(int id, string name, int age, string address, string phone)
        {
            PatientId = id;
            Name = name;
            Age = age;
            Address = address;
            this.phone = phone;
        }

        public void Register()
        {
            Console.WriteLine($"Paciente {Name} (ID: {PatientId}) registrado.");
        }

        public void AddPet(Pet pet)
        {
            if (pet.OwnerId != PatientId)
            {
                Console.WriteLine($"⚠ La mascota {pet.Name} tiene un OwnerId distinto al paciente.");
                return;
            }
            Pets.Add(pet);
        }

        public Pet FindPetById(int petId)
        {
            foreach (var pet in Pets)
            {
                if (pet.PetId == petId)
                    return pet;
            }
            throw new PetNotFoundException($"La mascota con ID {petId} no fue encontrada.");
        }

        public Pet FindPetByName(string name)
        {
            foreach (var pet in Pets)
            {
                if (pet.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return pet;
            }
            throw new PetNotFoundException($"La mascota '{name}' no fue encontrada.");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"\nPacienteID: {PatientId}, Nombre: {Name}, Edad: {Age}, Dirección: {Address}, Teléfono: {Phone}");
            Console.WriteLine("Mascotas:");
            foreach (var pet in Pets)
            {
                pet.ShowInfo();
            }
        }

        public void SendNotification(string message)
        {
            Console.WriteLine($"Notificación para {Name}: {message}");
        }
    }
}
