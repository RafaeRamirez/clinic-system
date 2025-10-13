using System;

namespace VetClinic.Models
{
    // Base abstract class for all veterinary services (e.g., Vaccination, GeneralCheckup)
    public abstract class VeterinaryService(string serviceName)
    {
        // Name of the veterinary service (e.g., "Vaccination", "General Checkup")
        public string ServiceName { get; private set; } = serviceName;

        // Abstract method that defines the action to perform during the service
        // Must be implemented by all derived classes
        public abstract void Attend();
    }
}
