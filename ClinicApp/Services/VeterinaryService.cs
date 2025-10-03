using System;

namespace VetClinic.Models
{
    public abstract class VeterinaryService
    {
        public string ServiceName { get; private set; }

        protected VeterinaryService(string serviceName)
        {
            ServiceName = serviceName;
        }

 
        public abstract void Attend();
    }
}
