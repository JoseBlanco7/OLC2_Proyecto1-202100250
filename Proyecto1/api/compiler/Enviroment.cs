public class Environment{
//TODO: parent environment

    public Dictionary<string, ValueWrapper> variables = new Dictionary<string, ValueWrapper>();
    public Environment? parent { get; private set; }

    public Environment(Environment? parent){
        this.parent = parent;
    }

    public ValueWrapper GetVariable(string id,Antlr4.Runtime.IToken token){
        if(variables.ContainsKey(id)){
            return variables[id];
        }if(parent != null){
            return parent.GetVariable(id,token); 
        }
        throw new SemanticError("Variable " + id + " Not found ", token);
    }

    public void DeclareVariable(string id, ValueWrapper value, Antlr4.Runtime.IToken token){
        if(variables.ContainsKey(id)){
            throw new SemanticError("Variable " + id + " Already declared ", token);
        }else{
            variables[id] = value;
        }
    }

    public ValueWrapper AssignVariable(string id, ValueWrapper value, Antlr4.Runtime.IToken token){
    if (variables.ContainsKey(id)){
        var oldValue = variables[id];

        // Conversión implícita de int a float64
        if (oldValue is FloatValue && value is IntValue intVal) {
            value = new FloatValue(intVal.Value);
        }
        
        // Verificar tipos después de intentar la conversión
        if (!(value is NilValue) && oldValue.GetType() != value.GetType()){
            throw new SemanticError($"No se puede asignar un {value.TypeName} a la variable '{id}' de tipo {oldValue.TypeName}.", token);
        }
        variables[id] = value;
        return value;   
    }
    if(parent != null){
        return parent.AssignVariable(id, value, token);
    }
    throw new SemanticError("Variable " + id + " Not found", token);
}

// Añadir a tu clase Environment:
public Dictionary<string, StructDefinition> structDefinitions = new Dictionary<string, StructDefinition>();

public void DeclareStruct(string id, StructDefinition definition, Antlr4.Runtime.IToken token)
{
    if (structDefinitions.ContainsKey(id))
    {
        throw new SemanticError($"Struct {id} ya fue declarado", token);
    }
    else
    {
        structDefinitions[id] = definition;
    }
}

public StructDefinition GetStructDefinition(string id, Antlr4.Runtime.IToken token)
{
    if (structDefinitions.ContainsKey(id))
    {
        return structDefinitions[id];
    }
    
    if (parent != null)
    {
        return parent.GetStructDefinition(id, token);
    }
    
    throw new SemanticError($"Struct {id} no encontrado", token);
}

public bool IsStructType(string typeName) {
    return structDefinitions.ContainsKey(typeName);
}

}