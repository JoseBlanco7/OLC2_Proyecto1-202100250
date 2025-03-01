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
        throw new SemanticError("Variable" + id + "Not found", token);
    }

    public void DeclareVariable(string id, ValueWrapper value, Antlr4.Runtime.IToken token){
        if(variables.ContainsKey(id)){
            throw new SemanticError("Variable" + id + "Already declared", token);
        }else{
            variables[id] = value;
        }
    }

    public ValueWrapper AssignVariable(string id, ValueWrapper value, Antlr4.Runtime.IToken token){
        if(variables.ContainsKey(id)){
            variables[id] = value;
            return value;
        }if(parent != null){
            return parent.AssignVariable(id, value, token);
        }
        throw new SemanticError("Variable" + id + "Not found", token);
    }

}