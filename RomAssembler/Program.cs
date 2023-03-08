using Assembler;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("-------------------");
Console.WriteLine("ROM Assembler 0.0.1");
Console.WriteLine("-------------------");

AssemblerMain.RunAssembler();

/*
string currentLine = ConsoleExtensions.ReadConsoleString();
currentLine.NextToken().Expect(".rom", 
    (packet, state) => { 
        packet.NextToken()
            .Match()
    },
    state => { state.SetErrorState(); ConsoleExtensions.OutputFatal("token expected: .rom", state.GetCurrentLine()); Environment.Exit(-1); }
);

bool finished = false;
while (!finished) {
    currentLine = ConsoleExtensions.ReadConsoleString();
    
    // check to see if we need to terminate assembly
    currentLine.NextToken()
        .Match(".end rom",  (packet, state) => { ConsoleExtensions.OutputString("found end of assembly token", state.GetCurrentLine()); finished = true; } );
}
*/