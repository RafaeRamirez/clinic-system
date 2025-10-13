using System;

namespace VetClinic.Exceptions
{
    // Custom exception class for handling cases when a pet is not found
    public class PetNotFoundException : Exception
    {
        // Constructor that passes the error message to the base Exception class
        public PetNotFoundException(string message) : base(message) { }
    }
}
