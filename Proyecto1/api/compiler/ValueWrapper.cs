public abstract record ValueWrapper
{
    public abstract string TypeName { get; }
}

public record IntValue(int Value) : ValueWrapper
{
    public override string TypeName => "int";
}

public record FloatValue(double Value) : ValueWrapper
{
    public override string TypeName => "float64";
}

public record StringValue(string Value) : ValueWrapper
{
    public override string TypeName => "string";
}

public record BoolValue(bool Value) : ValueWrapper
{
    public override string TypeName => "bool";
}

public record RuneValue(char Value) : ValueWrapper
{
    public override string TypeName => "rune";
}

public record FunctionValue(Invocable invocable, string name) : ValueWrapper
{
    public override string TypeName => "function";
}

public record VoidValue : ValueWrapper
{
    public override string TypeName => "void";
}

public record NilValue : ValueWrapper
{
    public override string TypeName => "nil";
}

public record SliceValue : ValueWrapper
{
    public List<ValueWrapper> Elements { get; }
    public string ElementType { get; set; }

    public SliceValue(string elementType) {
        Elements = new List<ValueWrapper>();
        ElementType = elementType;
    }

    public SliceValue(string elementType, List<ValueWrapper> elements) {
        ElementType = elementType;
        Elements = elements;
    }

    public override string TypeName => $"[]" + ElementType;

    public override string ToString() {
        // Imprime los elementos internamente, por ejemplo [1, 2, 3]
        return $"[{string.Join(", ", Elements.Select(e => e.ToString()))}]";
    }
}


// Definición de struct - información de la estructura
public record StructDefinition
{
    public Dictionary<string, string> Fields { get; } = new Dictionary<string, string>();
    
    public StructDefinition() {}
    
    public void AddField(string fieldName, string fieldType)
    {
        Fields.Add(fieldName, fieldType);
    }
}

// Instancia de struct - valores reales
public record StructValue : ValueWrapper
{
    private string structTypeName;
    public Dictionary<string, ValueWrapper> Fields { get; } = new Dictionary<string, ValueWrapper>();
    
    public StructValue(string typeName)
    {
        structTypeName = typeName;
    }
    
    public override string TypeName => structTypeName;
    
    public override string ToString()
    {
        var fieldStrings = Fields.Select(f => $"{f.Key}: {f.Value}");
        return $"{structTypeName}{{{string.Join(", ", fieldStrings)}}}";
    }

    
}






