using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

public class SemanticError : Exception
{
    private string message;
    private Antlr4.Runtime.IToken token;
    public SemanticError(string message, Antlr4.Runtime.IToken token){
        this.message = message;
        this.token = token;
    }
    public override string Message {
        get {
            return message + " LINEA " + token.Line + ", COLUMNA: " + token.Column;
        }
    }
}

public class LexicalErrorListener : BaseErrorListener, IAntlrErrorListener<int>
{
    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException ex)    {
        Console.WriteLine($"Error Lexico en linea {line} y columna {charPositionInLine}: {msg}");
        throw new ParseCanceledException($"Error Lexico en linea {line}: {charPositionInLine} - {msg}") ;
    }
}

public class SyntaxErrorListener : BaseErrorListener{
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException ex)
    {
        Console.WriteLine($"Error Sintactico en linea {line} y columna {charPositionInLine}: {msg}");
        throw new ParseCanceledException($"Error Sintactico en linea {line}: {charPositionInLine} - {msg}"); 
    }
}