using Assembler;
using StringTokenizerExtensions;
using SystemConsoleExtensions;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("-------------------");
Console.WriteLine("ROM Assembler 0.0.1");
Console.WriteLine("-------------------");

Console.In.NextToken().Expect(".rom",
    state => { Console.In.NextToken().Expect("\n", 
        state => { 
            state.IncrementLine();
            bool cont = true;

            while (cont) {
                Console.In.NextToken().PrintToken()
                    .TryMatch(s => s == ".params", state => {  })
                    .TryNextMatch(s => s == ".fetch", state => { })
                    .TryNextMatch(s => s == ".endrom", state => {
                        cont = false;
                        ConsoleExtensions.OutputString("assembly finished", state.GetCurrentLine());
                    }).Expect("\n", state => { state.IncrementLine(); }, 
                        (token, state) => { ConsoleExtensions.OutputError("unrecognized token", state.GetCurrentLine()); 
                    });
            }
        },
        (token, state) => { ConsoleExtensions.OutputFatal($"end of line expected, found {token}", state.GetCurrentLine()); });
    },
    (token, state) => { ConsoleExtensions.OutputFatal($"token '.rom' expected, found {token}", state.GetCurrentLine()); }
);

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