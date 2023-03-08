namespace Assembler;

class AssemblerStateProvider {

    private static Lazy<AssemblerState> _assemblerStateLazyObject = new Lazy<AssemblerState>(new AssemblerState());
    public static AssemblerState GetAssemblerState() => _assemblerStateLazyObject.Value;
}

class AssemblerState {

    private static readonly Dictionary<string, int> bitPositions = new Dictionary<string, int>();

    private int _lineNumber = 0;
    private bool _error = false;

    /// <summary>
    /// Increment the current line number from the assembler
    /// </summary>
    public AssemblerState IncrementLine() { _lineNumber++; return this; }

    public AssemblerState StoreBitPosition(string label, int position = -1) { bitPositions[label] = position; return this; }

    public AssemblerState SetErrorState() { _error = true; return this; }

    public int GetCurrentLine() => _lineNumber;
}