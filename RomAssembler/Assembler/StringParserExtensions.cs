namespace Assembler;

static class StringParserExtensions {

    public static (bool, string) Expect(this string token, string match, Action<AssemblerState> success, Action<string, AssemblerState> fail) {
        if (token == match) {
            success(AssemblerStateProvider.GetAssemblerState());
            return (true, token);
        } else {
            fail(token, AssemblerStateProvider.GetAssemblerState());
            return (false, token);
        }
    }

    public static (bool, string) Expect(this (bool, string) token, string match, Action<AssemblerState> success, Action<string, AssemblerState> fail) {
        // we passed down from another matching case
        if (token.Item1 && token.Item2 == string.Empty) return (true, string.Empty);
        return token.Item2.Expect(match, success, fail);
    }

    public static (bool, string) TryMatch(this string token, Func<string, bool> predicate, Action<AssemblerState> success) {
        if (predicate(token)) {
            success(AssemblerStateProvider.GetAssemblerState());
            return (true, string.Empty);
        }

        return (false, token);
    }

    public static (bool, string) TryMatch(this string token, Func<string, bool> predicate, Action<string, AssemblerState> success) {
        if (predicate(token)) {
            success(token, AssemblerStateProvider.GetAssemblerState());
            return (true, string.Empty);
        }

        return (false, token);
    }

    public static (bool, string) TryNextMatch(this (bool, string) result, Func<string, bool> predicate, Action<AssemblerState> success) {
        // if the previous result was a match, pass over this method
        if (result.Item1 && result.Item2 == string.Empty) {
            return (true, string.Empty);
        }

        return result.Item2.TryMatch(predicate, success);
    }

    public static (bool, string) AcceptAny(this string token, Action<string, AssemblerState> fn) {
        // this literally always returns accepted result and calls the function
        fn(token, AssemblerStateProvider.GetAssemblerState());
        return (true, string.Empty);
    }
}