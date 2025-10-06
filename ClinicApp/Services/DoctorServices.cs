using System;
using System.Collections.Generic;
using VetClinic.Interfaces;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public class DoctorService : IRegistrable
    {
        private int count = 0;
        private readonly List<Doctor> doctors = [];

        public List<Doctor> GetDoctors() => doctors;

        public void Register()
        {
            Console.WriteLine("\n=== Registro de Doctor ===");

            int doctorId = ++count;

            Console.Write("Ingrese el nombre del doctor: ");
            string name = Console.ReadLine()!;

            Console.Write("Ingrese la especialidad: ");
            string specialty = Console.ReadLine()!;

            Console.Write("Ingrese el número de teléfono: ");
            string phone = Console.ReadLine()!;

            Doctor doctor = new(doctorId, name, specialty, phone);
            doctors.Add(doctor);

            Console.WriteLine("\n Doctor registrado:");
            doctor.ShowInfo();
            Logger.LogInfo($"Doctor {doctor.Name} registrado con ID {doctor.Id}.");
        }

        
    }
}
