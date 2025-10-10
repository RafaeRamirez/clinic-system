using System;
using System.Collections.Generic;
using VetClinic.Interfaces;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public class VeterinarianService : IRegistrable
    {
        private int count = 0;
        private readonly List<Veterinarian> veterinarians = new List<Veterinarian>();

        public List<Veterinarian> GetVeterinarians() => veterinarians;

        public void Register()
        {
            Console.WriteLine("\n=== Registrarse como veterinario ===");

            int veterinarianId = ++count;

            Console.Write("Introduzca el nombre del veterinario: ");
            string name = Console.ReadLine()!;

            Console.Write("Ingrese la especialidad: ");
            string specialty = Console.ReadLine()!;

            Console.Write("Introduzca el número de teléfono: ");
            string phone = Console.ReadLine()!;

      
            Veterinarian veterinarian = new Veterinarian(veterinarianId, name, specialty, phone);


            veterinarians.Add(veterinarian);

            Console.WriteLine("\n ¡Veterinario registrado exitosamente!");
            veterinarian.ShowInfo();

            Logger.LogInfo($"Veterinario {veterinarian.Name} registrado con identificación {veterinarian.Id}.");
        }

        public void ShowVeterinarians()
        {
            Console.WriteLine("\n=== Lista de veterinarios ===");

            if (veterinarians.Count == 0)
            {
                Console.WriteLine("⚠ No hay veterinarios registrados.");
                return;
            }

            foreach (var vet in veterinarians)
            {
                vet.ShowInfo();
            }
        }
    }
}
