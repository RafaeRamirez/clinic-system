using VetClinic.Models; 

namespace VetClinic.Interfaces
{
    // Defines a contract for classes that support registration functionality
    public interface IRegistrable
    {
        // Method to register a new entity (patient, pet, veterinarian, etc.)
        void Register();
    }
}
