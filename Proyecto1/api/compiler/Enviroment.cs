public class Environment{
//TODO: parent environment

    public Dictionary<string, ValueWrapper> variables = new Dictionary<string, ValueWrapper>();
    private Environment? parent;

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

}