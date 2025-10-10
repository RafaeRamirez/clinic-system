using System;
using System.Collections.Generic;
using VetClinic.Interfaces;

namespace VetClinic.Models
{
    public class Patient : INotifiable
    {
        // Constructor sin parámetros (necesario para deserializar)
        public Patient() { }

        

        // Constructor principal
        public Patient(int id, string name, int age, string address, string phone)
        {
            Id = id;
            Name = name;
            Age = age;
            Address = address;
            Phone = phone;
            Pets = new List<Pet>();
        }

        // Propiedades públicas con get/set (requeridas para JSON)
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public List<Pet> Pets { get; set; } = new();

        public void ShowInfo()
        {
            Console.WriteLine($"\n👤 Paciente: {Name}, Edad: {Age}, Dirección: {Address}, Teléfono: {Phone}");
            if (Pets.Count == 0)
            {
                Console.WriteLine("⚠ No se registran mascotas.");
            }
            else
            {
                Console.WriteLine("🐾 Mascotas:");
                foreach (var pet in Pets)
                {
                    pet.ShowInfo();
                }
            }
        }

        public void Notify(string message)
        {
            Console.WriteLine($"🔔 Notificación a {Name}: {message}");
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Name}, Edad: {Age}, Dirección: {Address}, Teléfono: {Phone}, Nº de mascotas: {Pets.Count}";
        }
    }
}
