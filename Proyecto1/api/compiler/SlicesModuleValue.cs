using System.Collections.Generic;
using Antlr4.Runtime;

// Debe ser record si ValueWrapper es record
public record SlicesModuleValue : ValueWrapper
{
    public override string TypeName => "Module";

    // Almacena las funciones del "módulo"
    private Dictionary<string, Invocable> functions = new Dictionary<string, Invocable> {
        { "Index", new SlicesIndexFunction() }
    };

    // Permite acceder a sus "métodos" (ej. slices.Index)
    public Invocable? GetFunction(string name)
    {
        if (functions.ContainsKey(name))
            return functions[name];
        return null;
    }
}

// Función slices.Index
public class SlicesIndexFunction : Invocable
{
    public int Arity() => 2; // slices.Index requiere 2 argumentos
    
    public ValueWrapper Invoke(List<ValueWrapper> args, CompilerVisitor visitor)
    {
        ValueWrapper sliceArg = args[0];
        ValueWrapper valueArg = args[1];

        if (sliceArg is SliceValue slice)
        {
            for(int i=0; i<slice.Elements.Count; i++)
            {
                if (visitor.AreEqual(slice.Elements[i], valueArg))
                {
                    return new IntValue(i);
                }
            }
            return new IntValue(-1);
        }
        throw new SemanticError("El primer argumento de slices.Index debe ser un slice", new Antlr4.Runtime.ParserRuleContext().Start);
    }
}