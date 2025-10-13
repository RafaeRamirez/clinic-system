using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using VetClinic.Models;

namespace VetClinic.Utils
{
    public static class DatabaseSimulator
    {
        // Path of the JSON file used as the database
        private static readonly string filePath = "clinic_database.json";

        // Saves patients, veterinarians, appointments, and pets to a JSON file
        public static void SaveData(
            List<Patient> patients,
            List<Veterinarian> veterinarians,
            List<Appointment> appointments,
            List<Pet> pets)
        {
            // Create an anonymous object to store all data
            var data = new
            {
                Patients = patients,
                Veterinarians = veterinarians,
                Appointments = appointments,
                Pets = pets
            };

            // Convert data to JSON with indentation
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Write JSON content to file
            File.WriteAllText(filePath, json);
        }

        // Loads all data from the JSON file
        public static (List<Patient>, List<Veterinarian>, List<Appointment>, List<Pet>) LoadData()
        {
            // Check if the JSON file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("⚠ Database file not found. A new one will be created.");
                return (new List<Patient>(), new List<Veterinarian>(), new List<Appointment>(), new List<Pet>());
            }

            // Read JSON content from the file
            var json = File.ReadAllText(filePath);

            // Deserialize JSON into the DatabaseData structure
            var data = JsonSerializer.Deserialize<DatabaseData>(json);

            // Return lists or new empty ones if null
            return (
                data?.Patients ?? new List<Patient>(),
                data?.Veterinarians ?? new List<Veterinarian>(),
                data?.Appointments ?? new List<Appointment>(),
                data?.Pets ?? new List<Pet>()
            );
        }

        // Internal class that defines the structure of the stored data
        private class DatabaseData
        {
            public List<Patient>? Patients { get; set; }
            public List<Veterinarian>? Veterinarians { get; set; }
            public List<Appointment>? Appointments { get; set; }
            public List<Pet>? Pets { get; set; }
        }
    }
}
