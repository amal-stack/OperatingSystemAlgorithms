using static System.Console;

public static class InputHelpers
{
    /// <summary>
    /// Reads input from the console until a valid integer is entered.
    /// </summary>
    /// <returns>The input parsed as an <see langword="int"/>.</returns>
    public static int ReadInteger()
    {
        int value;
        while (!int.TryParse(ReadLine(), out value))
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine("Invalid input. Try again");
            ResetColor();
        }
        return value;
    }

    /// <summary>
    /// Reads and parses string of space-separated integers from the console
    /// </summary>
    /// <returns>The parsed values as an <see langword="int"/></returns>
    public static int[] ReadIntegers() => ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray() ?? Array.Empty<int>();
}

