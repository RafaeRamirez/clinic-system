namespace VetClinic.Utils

{
    public static class InputHelper
    {
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
    
        public static string? ReadOptional(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
   

    }
}
