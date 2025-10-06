using System;
using System.Collections.Generic;
using VetClinic.Interfaces;

namespace VetClinic.Models
{
    public class Patient(int id, string name, int age, string address, string phone): INotifiable
    {
        private readonly int id = id;
        private string name = name;
        private int age = age;
        private string address = address;
        private string phone = phone;
        private List<Pet> pets = [];

        public int Id => id;
        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
        public string Address { get => address; set => address = value; }
        private string Phone { get => phone; set => phone = value; }
        public List<Pet> Pets { get => pets; set => pets = value; }
        public void ShowInfo()
        {
            Console.WriteLine($"\n👤 Paciente: {name}, Edad: {age}, DIRECCIÓN: {address}, Teléfono: {phone} [PROTEGIDO]");
            if (pets.Count == 0)
            {
                Console.WriteLine("⚠ No se registran mascotas.");
            }
            else
            {
                Console.WriteLine("🐾 Mascotas:");
                foreach (var pet in pets)
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
