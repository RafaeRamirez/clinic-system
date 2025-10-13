# clinic-system
🏥 Clinic System

Clinic System is a console-based application written in C# (.NET) that simulates the management of a veterinary clinic.
It allows you to register, edit, delete, and search for patients, pets, veterinarians, and medical appointments.
All data is saved in a JSON file that acts as a simulated database.

🚀 Key Features

Patient Management
Register, edit, search, and delete patients.

Pet Management
Link pets to patients, edit or delete them, and make them produce sounds.

Veterinarian Management
Register, update, delete, and list veterinarians.

Appointment Management
Schedule and display appointments, checking for time conflicts.

Persistent Storage
All data is stored in a JSON file (clinic_database.json).

Logging System
Tracks actions and errors in a log file (vetclinic_log.txt).

🧩 Project Structure
VetClinic/
│
├── Models/                # Core data models
│   ├── Animal.cs
│   ├── Patient.cs
│   ├── Pet.cs
│   ├── Veterinarian.cs
│   ├── Appointment.cs
│   ├── VeterinaryService.cs
│   ├── GeneralCheckup.cs
│   └── Vaccination.cs
│
├── Services/              # Business logic and CRUD operations
│   ├── PatientService.cs
│   ├── PetService.cs
│   ├── VeterinarianService.cs
│   └── AppointmentService.cs
│
├── Utils/                 # Utility classes and helpers
│   ├── DatabaseSimulator.cs
│   ├── Logger.cs
│   ├── InputHelper.cs
│   ├── PatientMenu.cs
│   ├── PetMenu.cs
│   ├── VeterinarianMenu.cs
│   └── AppointmentMenu.cs
│
├── Interfaces/            # Reusable interface contracts
│   ├── IRegistrable.cs
│   ├── INotifiable.cs
│   └── IAttendable.cs
│
├── Exceptions/
│   └── PetNotFoundException.cs
│
├── Program.cs             # Application entry point
└── clinic_database.json   # Simulated JSON database

⚙️ Requirements

.NET SDK 8.0 or higher

C# 12

Works on Windows, Linux, and macOS

🖥️ How to Run

Clone the repository:

git clone https://github.com/RafaeRamirez/clinic-system.git


Navigate to the project folder:

cd clinic-system


Build the project:

dotnet build


Run the application:

dotnet run

📘 Main Menu

When you run the program, you’ll see a main menu that links to specific modules:

=== Veterinary Clinic Menu ===
1. Patients
2. Pets
3. Veterinarians
4. Appointments
5. Exit and Save Data


Each section has its own submenu:

Patients: Register, edit, delete, search, and list.

Pets: Register, edit, delete, and make sounds.

Veterinarians: Register, edit, delete, and list.

Appointments: Schedule and display appointments.

💾 Data Files

clinic_database.json → Stores all clinic data (patients, pets, vets, and appointments).

vetclinic_log.txt → Logs user actions and errors.

🧠 Concepts Used

Object-Oriented Programming (OOP)

Interfaces and dependency injection

JSON serialization/deserialization

Exception handling

Input validation

Persistent local data storage

👨‍💻 Author

Developed by: Rafael augusto ramirez bolaño 
clan: caiman
route C#
📧 Contact: rafar1129@gmail.com
📅 Year: 13/10/2025
