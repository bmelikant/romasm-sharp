namespace Assembler;

static class ConsoleExtensions {

    public static string PrintToken(this string token) {
        Console.WriteLine($"(current token) {token}");
        return token;
    }
    /// <summary>
    /// Read a string from the console. Returns an empty string if no input is available
    /// </summary>
    public static string ReadConsoleString() {
        AssemblerStateProvider.GetAssemblerState().IncrementLine();
        return Console.ReadLine() ?? string.Empty;
    }

    public static void OutputString(string message, int? lineNumber = null) {
        string lineNumberString = (lineNumber is not null) ? $" (line {lineNumber})" : "";
        Console.WriteLine($"message{lineNumberString}: {message}");
    }

    /// <summary>
    /// Print a warning message to the console
    /// </summary>
    /// <param name="message">The message to display as a warning</param>
    public static void OutputWarning(string message, int? lineNumber = null) {
        string lineNumberString = (lineNumber is not null) ? $" (line {lineNumber})" : "";
        Console.WriteLine($"warning{lineNumberString}: {message}");
    }

    /// <summary>
    /// Print an error message to the console
    /// </summary>
    /// <param name="message">The message to display as an error</param>
    public static void OutputError(string message, int? lineNumber = null) {
        string lineNumberString = (lineNumber is not null) ? $" (line {lineNumber})" : "";
        Console.WriteLine($"error{lineNumberString}: {message}");
    }

    /// <summary>
    /// Print a fatal message to the console
    /// </summary>
    /// <param name="message">The message to display as a fatal error</param>
    public static void OutputFatal(string message, int? lineNumber = null) {
        string lineNumberString = (lineNumber is not null) ? $" (line {lineNumber})" : "";
        Console.WriteLine($"fatal error{lineNumberString}: {message}"); 
    }
}