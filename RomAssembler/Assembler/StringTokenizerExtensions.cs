using System.Text;
using Assembler;
using SystemConsoleExtensions;

namespace StringTokenizerExtensions;

static class StringTokenizerExtensionClass {

    public enum TokenType {
        None        
    }

    public class TokenPacket {
        public string Token { get; set; } = "";
        public string Line { get; set; } = "";
        public TokenType Type { get; set; } = TokenType.None;
    }

    private static string [] commentDelimiters = { "//", "#", ";" };

    public static TokenPacket NextToken(this string line) {
        StringBuilder tokenBuilder = new StringBuilder();

        for (int i = 0; i < line.Length; i++) {
            // should we allow string literals? it's a pretty common use case...
            if (line[i] == ' ' || line[i] == '\t') break;

            tokenBuilder.Append(line[i]);
        }

        return new TokenPacket {
            Token = tokenBuilder.ToString(),
            Line = line
        };
    }

    public static TokenPacket NextToken(this TokenPacket packet) => packet.Line.NextToken();

    public static (bool, TokenPacket) Expect(this TokenPacket tokenPacket, string token, Action<TokenPacket, AssemblerState> success, Action<AssemblerState> fail) {
        if (tokenPacket.Token == token) success(tokenPacket, AssemblerStateProvider.GetAssemblerState());
        else fail(AssemblerStateProvider.GetAssemblerState());

        return (true, tokenPacket);
    }

    public static TokenPacket Match(this TokenPacket tokenPacket, string token, Action<TokenPacket, AssemblerState> success) {
        if (tokenPacket.Token == token) success(tokenPacket, AssemblerStateProvider.GetAssemblerState());

        return tokenPacket;
    }
}