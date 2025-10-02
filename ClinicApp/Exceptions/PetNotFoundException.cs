using System;

namespace VetClinic.Exceptions
{
    public class PetNotFoundException : Exception
    {
        public PetNotFoundException(string message) : base(message) { }
    }
}
