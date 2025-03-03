using System.Collections.Generic;
using Antlr4.Runtime;

public class AppendFunction : Invocable
{
    public int Arity() => -1; // append acepta al menos 2 argumentos (el slice y uno o más elementos a añadir)
    
    public ValueWrapper Invoke(List<ValueWrapper> args, CompilerVisitor visitor)
    {
        if (args.Count < 2)
            throw new SemanticError("La función append requiere al menos 2 argumentos", new ParserRuleContext().Start);
        
        ValueWrapper sliceArg = args[0];
        
        if (!(sliceArg is SliceValue slice))
            throw new SemanticError("El primer argumento de append debe ser un slice", new ParserRuleContext().Start);
        
        // Crear una copia del slice original
        var newSlice = new SliceValue(slice.ElementType, new List<ValueWrapper>(slice.Elements));
        
        // Añadir cada elemento adicional
        for (int i = 1; i < args.Count; i++)
        {
            ValueWrapper element = args[i];
            
            // Verificar que el elemento sea compatible con el tipo del slice
            if (!visitor.IsCompatibleType(element, slice.ElementType))
                throw new SemanticError($"No se puede agregar un {element.TypeName} a un slice de tipo {slice.ElementType}", new ParserRuleContext().Start);
            
            newSlice.Elements.Add(element);
        }
        
        return newSlice;
    }
}