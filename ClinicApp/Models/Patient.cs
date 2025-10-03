using System;
using System.Collections.Generic;
using VetClinic.Interfaces;

namespace VetClinic.Models
{
    public class Patient : IRegistrable, INotifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        private string Phone { get; set; }
        public List<Pet> Pets { get; set; } = new List<Pet>();

        public Patient(int id, string name, int age, string address, string phone)
        {
            Id = id;
            Name = name;
            Age = age;
            Address = address;
            Phone = phone;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"\n👤 Patient: {Name}, Age: {Age}, Address: {Address}, Phone: [PROTECTED]");
            if (Pets.Count == 0)
            {
                Console.WriteLine("⚠ No pets registered.");
            }
            else
            {
                Console.WriteLine("🐾 Pets:");
                foreach (var pet in Pets)
                {
                    pet.ShowInfo();
                }
            }
        }

        public void Register()
        {
            Console.WriteLine($"🔔 Patient {Name} has been registered.");
        }

        public void Notify(string message)
        {
            Console.WriteLine($"🔔 Notification to {Name}: {message}");
        }
    }
}
