using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using VetClinic.Models;

namespace VetClinic.Utils
{
    public static class DatabaseSimulator
    {
        private static readonly string filePath = "clinic_database.json";

        // Guarda pacientes, veterinarios, citas y mascotas
        public static void SaveData(
            List<Patient> patients,
            List<Veterinarian> veterinarians,
            List<Appointment> appointments,
            List<Pet> pets)
        {
            var data = new
            {
                Patients = patients,
                Veterinarians = veterinarians,
                Appointments = appointments,
                Pets = pets
            };

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }

        // Carga todos los datos del archivo JSON
        public static (List<Patient>, List<Veterinarian>, List<Appointment>, List<Pet>) LoadData()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("⚠ No se encontró la base de datos. Se creará una nueva.");
                return (new List<Patient>(), new List<Veterinarian>(), new List<Appointment>(), new List<Pet>());
            }

            var json = File.ReadAllText(filePath);
            var data = JsonSerializer.Deserialize<DatabaseData>(json);

            return (
                data?.Patients ?? new List<Patient>(),
                data?.Veterinarians ?? new List<Veterinarian>(),
                data?.Appointments ?? new List<Appointment>(),
                data?.Pets ?? new List<Pet>()
            );
        }

        // Clase interna para estructurar el JSON
        private class DatabaseData
        {
            public List<Patient>? Patients { get; set; }
            public List<Veterinarian>? Veterinarians { get; set; }
            public List<Appointment>? Appointments { get; set; }
            public List<Pet>? Pets { get; set; }
        }
    }
}
