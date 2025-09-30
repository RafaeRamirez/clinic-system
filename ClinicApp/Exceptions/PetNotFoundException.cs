using System;

namespace ClinicApp.Exceptions
{
    public class PetNotFoundException : Exception
    {
        public PetNotFoundException(string message) : base(message) { }
    }
}
