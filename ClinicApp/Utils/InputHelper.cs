namespace VetClinic.Utils
{
    // Utility class that provides safe and reusable input methods for console interaction
    public static class InputHelper
    {
        // Reads an integer from the console and validates the input
        // Keeps asking until a valid number is entered
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value))
                    return value;

                Console.WriteLine("⚠ Invalid input. Please enter a number.");
            }
        }

        // Reads a non-empty string from the console
        // Keeps asking until the user provides a valid (non-empty) value
        public static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("⚠ Value cannot be empty. Please try again.");
            }
        }

        // Reads an optional string from the console
        // Allows empty or null input (useful for optional fields)
        public static string? ReadOptional(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
