using System;
using ClinicApp.Models;
using ClinicApp.Services;
using ClinicApp.Exceptions;
using ClinicApp.Utils;

namespace ClinicApp
{
    public class Program
    {
        private static Patient? patient;

        public static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                MostrarMenu();
                string option = Console.ReadLine()!;

                try
                {
                    switch (option)
                    {
                        case "1":
                            patient = CrearPaciente();
                            break;

                        case "2":
                            if (patient != null) PetService.AddPet(patient);
                            else Console.WriteLine("⚠ Primero registre un paciente.");
                            break;

                        case "3":
                            patient?.ShowInfo();
                            break;

                        case "4":
                            if (patient != null)
                            {
                                Console.WriteLine("1. Buscar por ID");
                                Console.WriteLine("2. Buscar por nombre");
                                string opt = Console.ReadLine()!;
                                if (opt == "1") PetService.FindPetById(patient);
                                else if (opt == "2") PetService.FindPetByName(patient);
                                else Console.WriteLine("⚠ Opción inválida.");
                            }
                            else
                            {
                                Console.WriteLine("⚠ No hay paciente registrado.");
                            }
                            break;

                        case "5":
                            VeterinaryService checkup = new GeneralCheckup();
                            VeterinaryService vaccination = new Vaccination();
                            checkup.Attend();
                            vaccination.Attend();
                            break;

                        case "6":
                            running = false;
                            Console.WriteLine("👋 Saliendo del sistema...");
                            break;

                        default:
                            Console.WriteLine("⚠ Opción no válida.");
                            break;
                    }
                }
                catch (PetNotFoundException ex)
                {
                    Logger.LogError("Mascota no encontrada", ex);
                }
                catch (FormatException ex)
                {
                    Logger.LogError("Formato de entrada incorrecto", ex);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error inesperado", ex);
                }
                finally
                {
                    Logger.LogInfo("Operación finalizada.");
                }
            }
        }

        private static Patient CrearPaciente()
        {
            Console.Write("ID del paciente: ");
            int id = int.Parse(Console.ReadLine()!);

            Console.Write("Nombre: ");
            string name = Console.ReadLine()!;

            Console.Write("Edad: ");
            int age = int.Parse(Console.ReadLine()!);

            Console.Write("Dirección: ");
            string address = Console.ReadLine()!;

            Console.Write("Teléfono: ");
            string phone = Console.ReadLine()!;

            Patient patient = new Patient(id, name, age, address, phone);
            patient.Register();
            return patient;
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("\n=== Menú Principal ===");
            Console.WriteLine("1. Registrar paciente");
            Console.WriteLine("2. Agregar mascota");
            Console.WriteLine("3. Mostrar información del paciente");
            Console.WriteLine("4. Buscar mascota");
            Console.WriteLine("5. Servicios veterinarios");
            Console.WriteLine("6. Salir");
            Console.Write("👉 Elige una opción: ");
        }
    }
}
