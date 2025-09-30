namespace ClinicApp.Services
{

    public abstract class VeterinaryService
    {
        public string ServiceName { get; set; }

        protected VeterinaryService(string serviceName)
        {
            ServiceName = serviceName;
        }

        public abstract void Attend();
    }
}
