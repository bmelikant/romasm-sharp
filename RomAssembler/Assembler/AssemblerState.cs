namespace Assembler;

class AssemblerStateProvider {

    private static Lazy<AssemblerState> _assemblerStateLazyObject = new Lazy<AssemblerState>(new AssemblerState());
    public static AssemblerState GetAssemblerState() => _assemblerStateLazyObject.Value;
}

class AssemblerState {

    private int _lineNumber = 0;
    private bool _error = false;

    /// <summary>
    /// Increment the current line number from the assembler
    /// </summary>
    public AssemblerState IncrementLine() { _lineNumber++; return this; }
    public int GetCurrentLine() => _lineNumber;


    public AssemblerState SetErrorState() { _error = true; return this; }
}