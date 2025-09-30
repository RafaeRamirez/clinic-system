using System;
using System.Collections.Generic;
using ClinicApp.Interfaces;
using ClinicApp.Exceptions;

namespace ClinicApp.Models
{
    public class Patient : IRegistrable, INotificable
    {
        private string name;
        private int age;
        private string address;
        private string phone = string.Empty; 
        private List<Pet> pets;

        public string Name { get => name; set => name = value; }
        public int Age { get => age; set { if (value >= 0) age = value; } }
        public string Address { get => address; set => address = value; }

        // Teléfono protegido
        public string Phone => "Protected (confidential)";

        public List<Pet> Pets => pets;

        public Patient(string name, int age, string address, string phone)
        {
            Name = name;
            Age = age;
            Address = address;
            this.phone = phone;
            pets = new List<Pet>();
        }

        public void AddPet(Pet pet)
        {
            pets.Add(pet);
        }

        public Pet FindPet(string petName)
        {
            foreach (var pet in pets)
            {
                if (pet.Name.ToLower() == petName.ToLower())
                    return pet;
            }
            throw new PetNotFoundException($"The pet '{petName}' was not found for patient {Name}.");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Patient: {Name}, Age: {Age}, Address: {Address}, Phone: {Phone}");
            Console.WriteLine("Pets:");
            foreach (var pet in pets)
            {
                pet.ShowInfo();
            }
        }

        // IRegistrable
        public void Register()
        {
            Console.WriteLine($"Patient {Name} registered.");
        }

        // INotificable
        public void SendNotification(string message)
        {
            Console.WriteLine($"Notification to {Name}: {message}");
        }

        // Método interno para acceder al teléfono real
        public string GetPhoneInternal()
        {
            return phone;
        }
    }
}
