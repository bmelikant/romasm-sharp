using System.Text;

namespace Assembler;

static class StreamTokenizerExtensions {

    private static readonly char[] tokenSeparators = { ' ', '\t' };

    private static bool MatchIn(this char c, char[] arr) {
        foreach (char ch in arr) if (c == ch) return true;
        return false;
    }

    public static string NextToken(this TextReader textReader) {
        var tokenBuilder = new StringBuilder();
        char c = (char) textReader.Peek();

        if (c.MatchIn(tokenSeparators)) {
            while (c.MatchIn(tokenSeparators)) {
                textReader.Read();
                c = (char) textReader.Peek();
            }
        }

        while (!c.MatchIn(tokenSeparators)) {
            // if we are at the end of the line and there's nothing in the buffer, return the newline
            // otherwise return the buffer
            if (c == '\n') {
                if (tokenBuilder.Length == 0) tokenBuilder.Append((char) textReader.Read());
                break;
            } else if (c == '\r') {
                if (tokenBuilder.Length == 0) {
                    textReader.Read();
                    tokenBuilder.Append((char) textReader.Read());
                }
                break;
            }

            // otherwise consume the next character
            tokenBuilder.Append((char) textReader.Read());
            c = (char) textReader.Peek();
        }

        // we should probably skip to the next character that isn't whitespace
        while (c.MatchIn(tokenSeparators)) {
            textReader.Read();
            c = (char) textReader.Peek();
        }

        return tokenBuilder.ToString();
    }

    public static string NextToken(this TextReader textReader, Action<AssemblerState> onTokenFetch) {
        onTokenFetch(AssemblerStateProvider.GetAssemblerState());
        return textReader.NextToken();
    }
}