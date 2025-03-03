using System.Collections.Generic;
using Antlr4.Runtime;

public class LenFunction : Invocable
{
    public int Arity() => 1; // len requiere 1 argumento
    
    public ValueWrapper Invoke(List<ValueWrapper> args, CompilerVisitor visitor)
    {
        if (args.Count != 1)
            throw new SemanticError("La función len requiere exactamente 1 argumento", new ParserRuleContext().Start);
        
        ValueWrapper arg = args[0];
        
        if (arg is SliceValue slice)
        {
            return new IntValue(slice.Elements.Count);
        }
        else if (arg is StringValue str)
        {
            return new IntValue(str.Value.Length);
        }
        
        throw new SemanticError($"La función len no soporta argumentos de tipo {arg.TypeName}", new ParserRuleContext().Start);
    }
}