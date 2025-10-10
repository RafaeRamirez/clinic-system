using System;
using System.Collections.Generic;
using VetClinic.Models;
using VetClinic.Services;
using VetClinic.Exceptions;
using VetClinic.Utils;

namespace VetClinic
{
    public class Program
    {
        // Listas cargadas desde la base de datos simulada
        public static List<Patient> patients = new();
        public static List<Veterinarian> veterinarians = new();
        public static List<Appointment> appointments = new();
        public static List<Pet> pets = new();

        // Servicios
        public static PatientService patientService;
        public static PetService petService;
        public static VeterinarianService veterinarianService;
        public static AppointmentService appointmentService;

        public static void Main(string[] args)
        {
            // Cargar datos simulados desde JSON o crear nuevos
            (patients, veterinarians, appointments, pets) = DatabaseSimulator.LoadData();

            // Inicializar servicios con los datos cargados
            patientService = new PatientService(patients, veterinarians, appointments, pets);
            veterinarianService = new VeterinarianService(patients, veterinarians, appointments, pets);
            petService = new PetService(patientService);
            appointmentService = new AppointmentService(veterinarianService);

            Console.WriteLine("\n✅ Sistema de Clínica Veterinaria listo para usar.");

            bool running = true;

            while (running)
            {
                ShowMenu();
                string? option = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(option))
                {
                    Console.WriteLine("⚠ Por favor ingrese una opción válida (1-12).");
                    continue;
                }

                try
                {
                    switch (option)
                    {
                        case "1":
                            patientService.Register();
                            break;

                        case "2":
                            petService.Register();
                            break;

                        case "3":
                            patientService.ShowPatients();
                            break;

                        case "4":
                            petService.FindPetById();
                            break;

                        case "5":
                            petService.MakeAllPetsSound();
                            break;

                        case "6":
                            veterinarianService.Register();
                            break;

                        case "7":
                            ShowDoctors();
                            break;

                        case "8":
                            appointmentService.ScheduleAppointment(patients);
                            break;

                        case "9":
                            appointmentService.ShowAppointments();
                            break;

                        case "10":
                            patientService.EditPatientById();
                            break;
                        case "11":
                            patientService.DeletePatientById();
                            break;

                        case "12":
                            Console.WriteLine("💾 Guardando datos...");
                            DatabaseSimulator.SaveData(patients, veterinarians, appointments, pets);
                            Console.WriteLine("👋 Saliendo del sistema...");
                            running = false;
                            break;

                        default:
                            Console.WriteLine("⚠ Opción inválida, intenta de nuevo.");
                            break;
                    }
                }
                catch (PetNotFoundException ex)
                {
                    Logger.LogError("Mascota no encontrada", ex);
                }
                catch (FormatException ex)
                {
                    Logger.LogError("Formato de entrada no válido", ex);
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

        private static void ShowMenu()
        {
            Console.WriteLine("\n=== Menú de la Clínica Veterinaria ===");
            Console.WriteLine("1. Registrar paciente");
            Console.WriteLine("2. Registrar mascota");
            Console.WriteLine("3. Mostrar pacientes");
            Console.WriteLine("4. Buscar mascota");
            Console.WriteLine("5. Hacer sonidos de mascotas");
            Console.WriteLine("6. Registrar doctor");
            Console.WriteLine("7. Mostrar doctores");
            Console.WriteLine("8. Agendar cita médica");
            Console.WriteLine("9. Mostrar citas médicas");
            Console.WriteLine("10. Editar paciente");
            Console.WriteLine("11. Eliminar paciente");
            Console.WriteLine("12. Salir y guardar datos");
            Console.Write("👉 Elige una opción: ");
        }

        private static void ShowDoctors()
        {
            var doctors = veterinarianService.GetVeterinarians();

            if (doctors.Count == 0)
            {
                Console.WriteLine("⚠ No hay doctores registrados aún.");
                return;
            }

            Console.WriteLine("\n=== Lista de Doctores Registrados ===");
            foreach (var doctor in doctors)
            {
                doctor.ShowInfo();
            }
        }
    }
}
