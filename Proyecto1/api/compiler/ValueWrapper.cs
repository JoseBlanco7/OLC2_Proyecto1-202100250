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
    public string ElementType { get; }

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


