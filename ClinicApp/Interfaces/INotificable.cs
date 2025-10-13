namespace VetClinic.Interfaces
{
    // Defines a contract for classes that can send or receive notifications
    public interface INotifiable
    {
        // Sends a notification message to the target class or user
        void Notify(string message);
    }
}
