using System;

namespace VetClinic.Models
{
    public abstract class VeterinaryService(string serviceName)
    {
        public string ServiceName { get; private set; } = serviceName;

        public abstract void Attend();
    }
}
