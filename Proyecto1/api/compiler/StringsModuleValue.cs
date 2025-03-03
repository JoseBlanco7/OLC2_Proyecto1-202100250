using System.Collections.Generic;
using Antlr4.Runtime;

// Debe ser record si ValueWrapper es record
public record StringsModuleValue : ValueWrapper
{
    public override string TypeName => "Module";

    // Almacena las funciones del "módulo"
    private Dictionary<string, Invocable> functions = new Dictionary<string, Invocable> {
        { "Join", new StringsJoinFunction() }
    };

    // Permite acceder a sus "métodos" (ej. strings.Join)
    public Invocable? GetFunction(string name)
    {
        if (functions.ContainsKey(name))
            return functions[name];
        return null;
    }
}

// Función strings.Join
public class StringsJoinFunction : Invocable
{
    public int Arity() => 2; // strings.Join requiere 2 argumentos
    
    public ValueWrapper Invoke(List<ValueWrapper> args, CompilerVisitor visitor)
    {
        ValueWrapper sliceArg = args[0];
        ValueWrapper separatorArg = args[1];

        if (sliceArg is SliceValue slice && separatorArg is StringValue separator)
        {
            // Verificar que el slice sea de tipo string
            if (slice.ElementType != "string")
            {
                throw new SemanticError("El primer argumento de strings.Join debe ser un slice de strings", new Antlr4.Runtime.ParserRuleContext().Start);
            }

            // Convertir cada elemento a string y unir
            List<string> strings = new List<string>();
            foreach (var element in slice.Elements)
            {
                if (element is StringValue stringVal)
                {
                    strings.Add(stringVal.Value);
                }
                else
                {
                    throw new SemanticError("Todos los elementos del slice deben ser strings", new Antlr4.Runtime.ParserRuleContext().Start);
                }
            }

            return new StringValue(string.Join(separator.Value, strings));
        }
        throw new SemanticError("strings.Join requiere un slice de strings y un separador string", new Antlr4.Runtime.ParserRuleContext().Start);
    }
}