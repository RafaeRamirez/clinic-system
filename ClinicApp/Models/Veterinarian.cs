using System;
using VetClinic.Interfaces;

namespace VetClinic.Models
{
    public class Veterinarian : INotifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Phone { get; private set; }

        public Veterinarian(int id, string name, string specialty, string phone)
        {
            Id = id;
            Name = name;
            Specialty = specialty;
            Phone = phone;
        }

  
        public void ShowInfo()
        {
            Console.WriteLine($"👨‍⚕️ Doctor ID: {Id}, Name: {Name}, Specialty: {Specialty}, Phone: [PROTECTED]");
        }

      
    
        public void Notify(string message)
        {
            Console.WriteLine($"📢 Notification for Dr. {Name}: {message}");
        }
    }
}
