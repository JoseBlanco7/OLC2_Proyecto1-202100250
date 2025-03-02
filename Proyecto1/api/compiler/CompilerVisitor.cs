using System.Net.WebSockets;
using analyzer;

public class CompilerVisitor : LanguageBaseVisitor<ValueWrapper>{

    private ValueWrapper defaultVoid= new VoidValue();

    public string output = "";
    private Environment Currentenvironment;

    public CompilerVisitor(){
        Currentenvironment = new Environment(null);
        Embeded.Generate(Currentenvironment);
    }

    //VisitProgram
    public override ValueWrapper VisitProgram(LanguageParser.ProgramContext context){
        foreach(var declaraciones in context.declaraciones()){
            Visit(declaraciones);
        }
        return defaultVoid;
    }

    //VisitVarDeclaciones
    public override ValueWrapper VisitVarDeclaciones(LanguageParser.VarDeclacionesContext context)
{
    // Caso 1: var <id> <tipo> = expr
if (context.typeClause() != null && context.expr() != null && context.GetText().Contains("="))
{
    string id = context.ID().GetText();
    string declaredType = context.typeClause().GetText();
    ValueWrapper value = Visit(context.expr());
    
    // Validar compatibilidad de tipos
    if (declaredType == "int" && value is FloatValue) {
        throw new SemanticError($"No se puede asignar un float64 a la variable '{id}' de tipo int.", context.Start);
    }
    
    // Realizar conversión implícita de int a float64 si es necesario
    if (declaredType == "float64" && value is IntValue intVal)
    {
        value = new FloatValue(intVal.Value);
    }
    // Verificar que los tipos sean compatibles
    else if ((declaredType == "int" && !(value is IntValue)) ||
             (declaredType == "float64" && !(value is FloatValue || value is IntValue)) ||
             (declaredType == "bool" && !(value is BoolValue)) ||
             (declaredType == "string" && !(value is StringValue))||
             (declaredType == "rune" && !(value is RuneValue)))
    {
        throw new SemanticError($"No se puede asignar un valor de tipo {value.TypeName} a una variable de tipo {declaredType}.", context.Start);
    }
    
    Currentenvironment.DeclareVariable(id, value, context.Start);
    return defaultVoid;
}
    // Caso 2: var <id> <tipo> (sin valor)
    else if (context.typeClause() != null && context.expr() == null)
    {
        string id = context.ID().GetText();
        string declaredType = context.typeClause().GetText();
        
        // Asignar un valor por defecto según el tipo
        ValueWrapper defaultValue = declaredType switch {
            "int" => new IntValue(0),
            "float64" => new FloatValue(0.0),
            "bool" => new BoolValue(false),
            "string" => new StringValue(""),
            "rune" => new RuneValue(' '),   
            _ => throw new SemanticError($"Tipo no soportado: {declaredType}", context.Start)
        };
        
        Currentenvironment.DeclareVariable(id, defaultValue, context.Start);
        return defaultVoid;
    }
    // Caso 3: <id> := expr (inferencia de tipo)
    else if (context.GetText().Contains(":="))
    {
        string id = context.ID().GetText();
        ValueWrapper value = Visit(context.expr());
        
        Currentenvironment.DeclareVariable(id, value, context.Start);
        return defaultVoid;
    }
    else
    {
        throw new SemanticError("Formato de declaración de variable no reconocido.", context.Start);
    }
}
    //VisitExprStmt
    public override ValueWrapper VisitExprStmt(LanguageParser.ExprStmtContext context){
        return Visit(context.expr());
    }

    //VisitPrintStmt
    public override ValueWrapper VisitPrintStmt(LanguageParser.PrintStmtContext context){
        ValueWrapper value = Visit(context.expr());
        //output += value + "\n";
        output += value switch{
            IntValue i => i.Value.ToString(),
            FloatValue f => string.Format("{0:0.0#####}", f.Value),
            StringValue s => s.Value,
            BoolValue b => b.Value.ToString(),
            RuneValue r => r.Value.ToString(),
            VoidValue v => "void",
            FunctionValue fn => "<function " + fn.name + ">", 
            NilValue _ => "nil",
            _ => throw new SemanticError($"Tipo no soportado: {value.GetType()}", context.Start)
        } + "";
        output += "\n";
        return defaultVoid;
    }

    //VisitId
    public override ValueWrapper VisitId(LanguageParser.IdContext context){
        string id = context.ID().GetText();
        return Currentenvironment.GetVariable(id, context.Start);
        
        
    }


    //VisitParens
    public override ValueWrapper VisitParens(LanguageParser.ParensContext context){
        return Visit(context.expr());
    }

    //VisitNegate
    public override ValueWrapper VisitNegate(LanguageParser.NegateContext context){
    ValueWrapper value = Visit(context.expr());
    if (value is NilValue) {
        throw new SemanticError("Operacion no soportada en valor nil", context.Start);
    }
    return value switch{
        IntValue i => new IntValue(-i.Value),
        FloatValue f => new FloatValue(-f.Value),
        _ => throw new SemanticError($"Operacion no soportada: -{value.GetType()}", context.Start)
    };
}


    //VisitInt
    public override ValueWrapper VisitInt(LanguageParser.IntContext context){
        return new IntValue(int.Parse(context.INT().GetText()));
    }
    //VisitMulDiv
    public override ValueWrapper VisitMulDiv(LanguageParser.MulDivContext context){
    ValueWrapper left = Visit(context.expr(0));
    ValueWrapper right = Visit(context.expr(1));
    if (left is NilValue || right is NilValue) {
        throw new SemanticError("Operacion no soportada en valor nil", context.Start);
    }
    var op = context.op.Text;
    return (left, right, op) switch{
        (IntValue l, IntValue r, "*") => new IntValue(l.Value * r.Value),
        (IntValue l, IntValue r, "/") => new IntValue(l.Value / r.Value),
        (FloatValue l, FloatValue r, "*") => new FloatValue(l.Value * r.Value),
        (FloatValue l, FloatValue r, "/") => new FloatValue(l.Value / r.Value),
        (StringValue l, IntValue r, "*") => new StringValue(string.Concat(Enumerable.Repeat(l.Value, r.Value))),
        _ => throw new SemanticError($"Operacion no soportada: {left.GetType()} {op} {right.GetType()}", context.Start)
    };
}
    //VisitAddSub
    public override ValueWrapper VisitAddSub(LanguageParser.AddSubContext context){
        ValueWrapper left = Visit(context.expr(0));
        ValueWrapper right = Visit(context.expr(1));
        if (left is NilValue || right is NilValue) {
        throw new SemanticError("Operacion no soportada en valor nil", context.Start);
    }
        var op = context.op.Text;
        return (left, right, op) switch{
            (IntValue l, IntValue r, "+") => new IntValue(l.Value + r.Value),
            (IntValue l, IntValue r, "-") => new IntValue(l.Value - r.Value),
            (FloatValue l, FloatValue r, "+") => new FloatValue(l.Value + r.Value),
            (FloatValue l, FloatValue r, "-") => new FloatValue(l.Value - r.Value),
            (StringValue l, StringValue r, "+") => new StringValue(l.Value + r.Value),
            (IntValue l, StringValue r, "+") => new StringValue(l.Value.ToString() + r.Value),
            (StringValue l, IntValue r, "+") => new StringValue(l.Value + r.Value.ToString()),
            _ => throw new SemanticError($"Operacion no soportada: {left.GetType()} {op} {right.GetType()}", context.Start)
        };
    }

    //VisitFloat
    public override ValueWrapper VisitFloat(LanguageParser.FloatContext context){
        return new FloatValue(float.Parse(context.FLOAT().GetText()));
    }
    //VisitBool
    public override ValueWrapper VisitBool(LanguageParser.BoolContext context){
        return new BoolValue(bool.Parse(context.BOOL().GetText()));
    }
    //VisitString
    public override ValueWrapper VisitString(LanguageParser.StringContext context) {
    string rawString = context.STRING().GetText();
    // Eliminar la primera y última comilla
    string unquoted = rawString.Substring(1, rawString.Length - 2);
    //remplazar las secuencias de escape
    string processedString = unquoted
        .Replace("\\\"", "\"")
        .Replace("\\\\", "\\")
        .Replace("\\n", "\n")
        .Replace("\\r", "\r")
        .Replace("\\t", "\t");
    return new StringValue(processedString);
}

    //VisitRune
    public override ValueWrapper VisitRune(LanguageParser.RuneContext context) {
    string rawRune = context.RUNE().GetText();
    // Eliminar las comillas simples
    char runeValue = rawRune[1];
    return new RuneValue(runeValue);
}
    
    //VisitRelational
    public override ValueWrapper VisitRelational(LanguageParser.RelationalContext context){
        ValueWrapper left = Visit(context.expr(0));
        ValueWrapper right = Visit(context.expr(1));
        if (left is NilValue || right is NilValue) {
        throw new SemanticError("Operacion no soportada en valor nil", context.Start);
        }   
        var op = context.op.Text;
        return (left, right, op) switch{
            (IntValue l, IntValue r, "<") => new BoolValue(l.Value < r.Value),
            (IntValue l, IntValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (IntValue l, IntValue r, ">") => new BoolValue(l.Value > r.Value),
            (IntValue l, IntValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (FloatValue l, FloatValue r, "<") => new BoolValue(l.Value < r.Value),
            (FloatValue l, FloatValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (FloatValue l, FloatValue r, ">") => new BoolValue(l.Value > r.Value),
            (FloatValue l, FloatValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (StringValue l, StringValue r, "<") => new BoolValue(l.Value.CompareTo(r.Value) < 0),
            (StringValue l, StringValue r, "<=") => new BoolValue(l.Value.CompareTo(r.Value) <= 0),
            (StringValue l, StringValue r, ">") => new BoolValue(l.Value.CompareTo(r.Value) > 0),
            (StringValue l, StringValue r, ">=") => new BoolValue(l.Value.CompareTo(r.Value) >= 0),
            _ => throw new SemanticError($"Operacion no soportada: {left.GetType()} {op} {right.GetType()}", context.Start)
        };
    }   
    //VisitAssign
    public override ValueWrapper VisitAssign(LanguageParser.AssignContext context){
        string id = context.ID().GetText();
        ValueWrapper value = Visit(context.expr());
        return Currentenvironment.AssignVariable(id, value,context.Start);}   
    //VisitEquality
    public override ValueWrapper VisitEquality(LanguageParser.EqualityContext context){
        ValueWrapper left = Visit(context.expr(0));
        ValueWrapper right = Visit(context.expr(1));
        if (left is NilValue || right is NilValue) {
        throw new SemanticError("Operacion no soportada en valor nil", context.Start);
        }
        var op = context.op.Text;
        return (left, right, op) switch{
            (IntValue l, IntValue r, "==") => new BoolValue(l.Value == r.Value),
            (IntValue l, IntValue r, "!=") => new BoolValue(l.Value != r.Value),
            (FloatValue l, FloatValue r, "==") => new BoolValue(l.Value == r.Value),
            (FloatValue l, FloatValue r, "!=") => new BoolValue(l.Value != r.Value),
            (StringValue l, StringValue r, "==") => new BoolValue(l.Value == r.Value),
            (StringValue l, StringValue r, "!=") => new BoolValue(l.Value != r.Value),
            (BoolValue l, BoolValue r, "==") => new BoolValue(l.Value == r.Value),
            (BoolValue l, BoolValue r, "!=") => new BoolValue(l.Value != r.Value),
            _ => throw new SemanticError($"Operacion no soportada: {left.GetType()} {op} {right.GetType()}", context.Start)
        };
    }


    //VisitBlockStmt
    public override ValueWrapper VisitBlockStmt(LanguageParser.BlockStmtContext context){
        Environment previousEnvironment = Currentenvironment;
        Currentenvironment = new Environment(previousEnvironment);
        foreach(var statement in context.declaraciones()){
            Visit(statement);
        }
        Currentenvironment = previousEnvironment;
        return defaultVoid;
    }   
    
    //VisitIfStmt
    public override ValueWrapper VisitIfStmt(LanguageParser.IfStmtContext context){
        ValueWrapper condition = Visit(context.expr());
        if(condition is not BoolValue){
            throw new SemanticError("Condicion no booleana", context.Start);
        }
        if ((condition as BoolValue).Value){
            Visit(context.stmt(0));
    }else if(context.stmt().Length > 1){
        Visit(context.stmt(1));
    }
    return defaultVoid;
    }

    //VisitWhileStmt
    public override ValueWrapper VisitWhileStmt(LanguageParser.WhileStmtContext context){
        ValueWrapper condition = Visit(context.expr());
        if(condition is not BoolValue){
            throw new SemanticError("Condicion no booleana", context.Start);
        }
        while((condition as BoolValue).Value){
            Visit(context.stmt());
            condition = Visit(context.expr());
            if(condition is not BoolValue){
                throw new SemanticError("Condicion no booleana", context.Start);
            }
        }
        return defaultVoid;
    }

    //VisitForStmt
    public override ValueWrapper VisitForStmt(LanguageParser.ForStmtContext context){
        //Guardamos el entorno anterior:
        Environment previousEnvironment = Currentenvironment;
        //Creamos un nuevo entorno:
        Currentenvironment = new Environment(Currentenvironment);
        //Visitamos la inicializacion:
        Visit(context.forInit());
        //VisitForBody
        VisitForBody(context);
        //Restauramos el entorno anterior:
        Currentenvironment = previousEnvironment;
        return defaultVoid;
    }  

    public void VisitForBody(LanguageParser.ForStmtContext context){
        ValueWrapper condition = Visit(context.expr(0));
var lastEnvironment = Currentenvironment;
        
        if (condition is not BoolValue){
            throw new SemanticError("Condicion invalida", context.Start);
        }  
        try{
            while(condition is BoolValue boolCondition && boolCondition.Value){
                Visit(context.stmt());
                Visit(context.expr(1));
                condition = Visit(context.expr(0));
            }
        }catch(BreakException){
            //Si se lanza una excepcion de tipo BreakException, salimos del ciclo
            Currentenvironment = lastEnvironment;
        }
        catch(ContinueException){
            //Si se lanza una excepcion de tipo ContinueException, continuamos con la siguiente iteracion
            Currentenvironment = lastEnvironment;
            Visit(context.expr(1));
            VisitForBody(context);
        }    
}

    
    //VisitForInit
    //visitBreakStmt
    public override ValueWrapper VisitBreakStmt(LanguageParser.BreakStmtContext context){
        throw new BreakException();
    }
    //visitContinueStmt
    public override ValueWrapper VisitContinueStmt(LanguageParser.ContinueStmtContext context){
        throw new ContinueException();
    }
    //visitReturnStmt
    public override ValueWrapper VisitReturnStmt(LanguageParser.ReturnStmtContext context){
        ValueWrapper value = defaultVoid;
        if (context.expr() != null){
            value = Visit(context.expr());
        } 
        throw new ReturnException(value);
    }

    //visitCallee
    public override ValueWrapper VisitCallee(LanguageParser.CalleeContext context){
        ValueWrapper callee = Visit(context.expr());
        foreach (var call in context.call()){ 
            if(callee is FunctionValue functionValue){
                callee = VisitCall(functionValue.invocable, call.arg());
            }else{
                throw new SemanticError("No se puede invocar a un valor que no es una funcion", context.Start);
            }
        }
        return callee;
    
    }

    public ValueWrapper VisitCall(Invocable invocable, LanguageParser.ArgContext context){
        List<ValueWrapper> arguments = new List<ValueWrapper>();
        if(context != null){
            foreach(var expr in context.expr()){
                arguments.Add(Visit(expr));
            }
        }
        if (context != null && arguments.Count != invocable.Arity()){
            throw new SemanticError("Numero de argumentos incorrecto", context.Start);
        } 
        return invocable.Invoke(arguments, this);

    }

    // VisitNil
    public override ValueWrapper VisitNil(LanguageParser.NilContext context){
        return new NilValue();
    }
    

    
}