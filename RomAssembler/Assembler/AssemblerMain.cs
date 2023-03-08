namespace Assembler;

public static class AssemblerMain {
	public static void RunAssembler() {
		Console.In.NextToken().Expect(".rom",
			state => {
				Console.In.NextToken().Expect("\n", AssemblerLoop,
				(token, state) => ConsoleExtensions.OutputFatal($"end of line expected, found {token}", state.GetCurrentLine()));
			}, 
			(token, state) => ConsoleExtensions.OutputFatal($"token '.rom' expected, found {token}", state.GetCurrentLine()));
	}

	static void AssemblerLoop(AssemblerState state) {
		state.IncrementLine();
		bool cont = true;

		while (cont) {
			Console.In.NextToken().PrintToken()
				.TryMatch(s => s == ".params", state => {  })
				.TryNextMatch(s => s == ".bits", state => {
					Console.In.NextToken().Expect("\n",
					BitPositionLoop,
					(token, state) => ConsoleExtensions.OutputFatal($"end of line expected, found {token}", state.GetCurrentLine()));
				})
				.TryNextMatch(s => s == ".fetch", state => { })
				.TryNextMatch(s => s == ".endrom", state => {
					cont = false;
					ConsoleExtensions.OutputString("assembly finished", state.GetCurrentLine());
				}).Expect("\n",
					state => { state.IncrementLine(); }, 
					(token, state) => { ConsoleExtensions.OutputError("unrecognized token", state.GetCurrentLine()); 
				});
		}
	}

	static void BitPositionLoop(AssemblerState state) {
		state.IncrementLine();
		bool cont = true;

		while (cont) {
			Console.In.NextToken().PrintToken()
				.TryMatch(s => s == ".bitpos", ReadBitPosition)
				.TryNextMatch(s => s == ".end-bits", state => {
					cont = false;
					ConsoleExtensions.OutputString("finished reading bit positions");
				}).Expect("\n", 
					state => state.IncrementLine(), 
					(token, state) => ConsoleExtensions.OutputError($"expected newline, found {token}", state.GetCurrentLine())
				);
		}
	}

	static void ReadBitPosition(AssemblerState state) {
		Console.In.NextToken().AcceptAny((token, state) => {
			state.StoreBitPosition(token);
		});
	}
}