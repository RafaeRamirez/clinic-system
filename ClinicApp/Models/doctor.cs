using System;
using VetClinic.Interfaces;

namespace VetClinic.Models
{
    public class Doctor : IRegistrable, INotifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Phone { get; private set; }

        public Doctor(int id, string name, string specialty, string phone)
        {
            Id = id;
            Name = name;
            Specialty = specialty;
            Phone = phone;
        }

        // Método para mostrar la información del doctor
        public void ShowInfo()
        {
            Console.WriteLine($"👨‍⚕️ Doctor ID: {Id}, Name: {Name}, Specialty: {Specialty}, Phone: [PROTECTED]");
        }

        // Implementación de IRegistrable
        public void Register()
        {
            Console.WriteLine($"✅ Doctor {Name} (ID: {Id}) registered successfully.");
        }

        // Implementación de INotifiable
        public void Notify(string message)
        {
            Console.WriteLine($"📢 Notification for Dr. {Name}: {message}");
        }
    }
}
