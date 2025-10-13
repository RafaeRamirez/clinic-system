using System;
using VetClinic.Interfaces;

namespace VetClinic.Models
{
    // Represents a veterinarian who works in the clinic
    public class Veterinarian : INotifiable
    {
        // Public properties used for identification and contact
        public int Id { get; set; }             // Unique identifier for the veterinarian
        public string Name { get; set; } = "";  // Veterinarian's full name
        public string Specialty { get; set; } = ""; // Medical specialty (e.g., surgery, dermatology)
        public string Phone { get; private set; } = ""; // Contact phone number (read-only outside the class)

        // Constructor used to create a new veterinarian record
        public Veterinarian(int id, string name, string specialty, string phone)
        {
            Id = id;
            Name = name;
            Specialty = specialty;
            Phone = phone;
        }

        // Displays key information about the veterinarian
        public void ShowInfo()
        {
            Console.WriteLine($"👨‍⚕️ Doctor ID: {Id}, Name: {Name}, Specialty: {Specialty}, Phone: [PROTECTED]");
        }

        // Allows secure phone number updates
        public void UpdatePhone(string newPhone)
        {
            if (string.IsNullOrWhiteSpace(newPhone))
            {
                Console.WriteLine("⚠ Invalid phone number.");
                return;
            }

            Phone = newPhone;
            Console.WriteLine($"📞 Dr. {Name}'s phone number updated successfully.");
        }

        // Sends an internal notification to the veterinarian
        public void Notify(string message)
        {
            Console.WriteLine($"📢 Notification for Dr. {Name}: {message}");
        }
    }
}
