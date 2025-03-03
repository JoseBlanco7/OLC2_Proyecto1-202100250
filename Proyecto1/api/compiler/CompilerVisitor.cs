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
    if (context.printArgs() == null) {
        // Caso fmt.Println() sin argumentos
        output += "\n";
        return defaultVoid;
    }

    // Procesar cada argumento
    for (int mas = 0; mas < context.printArgs().expr().Length; mas++) {
        ValueWrapper value = Visit(context.printArgs().expr(mas));
        
        // Formatear según el tipo
        output += value switch {
            IntValue i => i.Value.ToString(),
            FloatValue f => string.Format("{0:0.0#####}", f.Value),
            StringValue s => s.Value,
            BoolValue b => b.Value.ToString(),
            RuneValue r => r.Value.ToString(),
            VoidValue v => "void",
            FunctionValue fn => "<function " + fn.name + ">", 
            NilValue _ => "nil",
            _ => throw new SemanticError($"Tipo no soportado: {value.GetType()}", context.Start)
        };
        
        // Añadir espacio entre argumentos, excepto para el último
        if (mas < context.printArgs().expr().Length - 1) {
            output += " ";
        }
    }
    
    // Añadir salto de línea al final
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

    // Verificación de división por cero
    if (op == "/" && ((right is IntValue intVal && intVal.Value == 0) || 
                    (right is FloatValue floatVal && floatVal.Value == 0))) {
        throw new SemanticError("Division por cero no permitida", context.Start);
    }
    return (left, right, op) switch{
        (IntValue l, IntValue r, "*") => new IntValue(l.Value * r.Value),               // int * int = int
        (IntValue l, FloatValue r, "*") => new FloatValue(l.Value * r.Value),           // int * float = float
        (FloatValue l, FloatValue r, "*") => new FloatValue(l.Value * r.Value),         // float * float = float
        (FloatValue l, IntValue r, "*") => new FloatValue(l.Value * r.Value),           // float * int = float


        (IntValue l, IntValue r, "/") => new IntValue(l.Value / r.Value),               // int / int = int
        (IntValue l, FloatValue r, "/") => new FloatValue(l.Value / r.Value),           // int / float = float
        (FloatValue l, FloatValue r, "/") => new FloatValue(l.Value / r.Value),         // float / float = float
        (FloatValue l, IntValue r, "/") => new FloatValue(l.Value / r.Value),           // float / int = float

        (IntValue l, IntValue r, "%") => new IntValue(l.Value % r.Value),               // int % int = int
        //(StringValue l, IntValue r, "*") => new StringValue(string.Concat(Enumerable.Repeat(l.Value, r.Value))),
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
            (IntValue l, IntValue r, "+") => new IntValue(l.Value + r.Value),               // int + int = int
            (IntValue l, FloatValue r, "+") => new FloatValue(l.Value + r.Value),           // int + float = float
            (FloatValue l, FloatValue r, "+") => new FloatValue(l.Value + r.Value),         // float + float = float
            (FloatValue l, IntValue r, "+") => new FloatValue(l.Value + r.Value),           // float + int = float
            (StringValue l, StringValue r, "+") => new StringValue(l.Value + r.Value),      // string + string = string


            (IntValue l, IntValue r, "-") => new IntValue(l.Value - r.Value),               // int - int = int
            (IntValue l, FloatValue r, "-") => new FloatValue(l.Value - r.Value),           // int - float = float
            (FloatValue l, FloatValue r, "-") => new FloatValue(l.Value - r.Value),         // float - float = float
            (FloatValue l, IntValue r, "-") => new FloatValue(l.Value - r.Value),           // float - int = float

            //(IntValue l, StringValue r, "+") => new StringValue(l.Value.ToString() + r.Value),
            //(StringValue l, IntValue r, "+") => new StringValue(l.Value + r.Value.ToString()),
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
            (FloatValue l, FloatValue r, "<") => new BoolValue(l.Value < r.Value),
            (IntValue l, FloatValue r, "<") => new BoolValue(l.Value < r.Value),
            (FloatValue l, IntValue r, "<") => new BoolValue(l.Value < r.Value),
            (RuneValue l, RuneValue r, "<") => new BoolValue(l.Value < r.Value),
            //(StringValue l, StringValue r, "<") => new BoolValue(l.Value.CompareTo(r.Value) < 0),

            (IntValue l, IntValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (FloatValue l, FloatValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (IntValue l, FloatValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (FloatValue l, IntValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (RuneValue l, RuneValue r, "<=") => new BoolValue(l.Value <= r.Value),
            //(StringValue l, StringValue r, "<=") => new BoolValue(l.Value.CompareTo(r.Value) <= 0),

            (IntValue l, IntValue r, ">") => new BoolValue(l.Value > r.Value),
            (FloatValue l, FloatValue r, ">") => new BoolValue(l.Value > r.Value),
            (IntValue l, FloatValue r, ">") => new BoolValue(l.Value > r.Value),
            (FloatValue l, IntValue r, ">") => new BoolValue(l.Value > r.Value),
            (RuneValue l, RuneValue r, ">") => new BoolValue(l.Value > r.Value),
            //(StringValue l, StringValue r, ">") => new BoolValue(l.Value.CompareTo(r.Value) > 0),

            (IntValue l, IntValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (FloatValue l, FloatValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (IntValue l, FloatValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (FloatValue l, IntValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (RuneValue l, RuneValue r, ">=") => new BoolValue(l.Value >= r.Value),
            //(StringValue l, StringValue r, ">=") => new BoolValue(l.Value.CompareTo(r.Value) >= 0),



            _ => throw new SemanticError($"Operacion no soportada: {left.GetType()} {op} {right.GetType()}", context.Start)
        };
    }   
    //VisitAssign
    public override ValueWrapper VisitAssign(LanguageParser.AssignContext context){
        string id = context.ID().GetText();
        ValueWrapper value = Visit(context.expr());
        return Currentenvironment.AssignVariable(id, value,context.Start);}  
    //VisitAssignAdd 
    //VisitAssignAdd 
public override ValueWrapper VisitAssignAdd(LanguageParser.AssignAddContext context)
{
    string id = context.ID().GetText();
    ValueWrapper rightValue = Visit(context.expr());
    
    // Obtener el valor actual de la variable
    ValueWrapper currentValue = Currentenvironment.GetVariable(id, context.Start);
    
    // Realizar la suma según el tipo de las variables
    ValueWrapper result;
    
    if (currentValue is IntValue leftInt){
        if (rightValue is IntValue rightInt){
            // int += int -> int
            result = new IntValue(leftInt.Value + rightInt.Value);}
        else if (rightValue is FloatValue rightFloat){
            // int += float64 -> float64 (conversión implícita)
            result = new FloatValue(leftInt.Value + rightFloat.Value);}
        else{
            throw new SemanticError($"No se puede aplicar += entre {currentValue.TypeName} y {rightValue.TypeName}", context.Start);}
    }
    else if (currentValue is FloatValue leftFloat)
    {
        if (rightValue is IntValue rightInt)
        {
            // float64 += int -> float64
            result = new FloatValue(leftFloat.Value + rightInt.Value);
        }
        else if (rightValue is FloatValue rightFloat){
            // float64 += float64 -> float64
            result = new FloatValue(leftFloat.Value + rightFloat.Value);}
        else{
            throw new SemanticError($"No se puede aplicar += entre {currentValue.TypeName} y {rightValue.TypeName}", context.Start);}
    }
    else if (currentValue is StringValue leftString) {
        if (rightValue is StringValue rightString) {
            // string += string -> string (concatenación)
            result = new StringValue(leftString.Value + rightString.Value);
        }
        else{
            throw new SemanticError($"No se puede aplicar += entre {currentValue.TypeName} y {rightValue.TypeName}", context.Start);}
    }
    else{
        throw new SemanticError($"El operador += solo es aplicable a tipos numéricos (int, float64) y strings", context.Start);
    }
    // Asignar el resultado a la variable
    Currentenvironment.AssignVariable(id, result, context.Start);
    return result;
}
//VisitAssignSub 
public override ValueWrapper VisitAssignSub(LanguageParser.AssignSubContext context)
{
    string id = context.ID().GetText();
    ValueWrapper rightValue = Visit(context.expr());
    
    // Obtener el valor actual de la variable
    ValueWrapper currentValue = Currentenvironment.GetVariable(id, context.Start);
    
    // Realizar la resta según el tipo de las variables
    ValueWrapper result;
    
    if (currentValue is IntValue leftInt){
        if (rightValue is IntValue rightInt){
            // int -= int -> int
            result = new IntValue(leftInt.Value - rightInt.Value);
        }
        else if (rightValue is FloatValue rightFloat){
            // int -= float64 -> ERROR (no se puede asignar float64 a int)
            throw new SemanticError($"No se puede asignar un float64 a la variable '{id}' de tipo int después de la operación -=", context.Start);
        }
        else{
            throw new SemanticError($"No se puede aplicar -= entre {currentValue.TypeName} y {rightValue.TypeName}", context.Start);
        }
    }
    else if (currentValue is FloatValue leftFloat)
    {
        if (rightValue is IntValue rightInt)
        {
            // float64 -= int -> float64
            result = new FloatValue(leftFloat.Value - rightInt.Value);
        }
        else if (rightValue is FloatValue rightFloat){
            // float64 -= float64 -> float64
            result = new FloatValue(leftFloat.Value - rightFloat.Value);
        }
        else{
            throw new SemanticError($"No se puede aplicar -= entre {currentValue.TypeName} y {rightValue.TypeName}", context.Start);
        }
    }
    else{
        throw new SemanticError($"El operador -= solo es aplicable a tipos numéricos (int, float64)", context.Start);
    }
    
    // Asignar el resultado a la variable
    Currentenvironment.AssignVariable(id, result, context.Start);
    return result;
}


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
            (FloatValue l, FloatValue r, "==") => new BoolValue(l.Value == r.Value),
            (IntValue l, FloatValue r, "==") => new BoolValue(l.Value == r.Value),
            (FloatValue l, IntValue r, "==") => new BoolValue(l.Value == r.Value),
            (BoolValue l, BoolValue r, "==") => new BoolValue(l.Value == r.Value),
            (StringValue l, StringValue r, "==") => new BoolValue(l.Value == r.Value),
            (RuneValue l, RuneValue r, "==") => new BoolValue(l.Value == r.Value),

            (IntValue l, IntValue r, "!=") => new BoolValue(l.Value != r.Value),
            (FloatValue l, FloatValue r, "!=") => new BoolValue(l.Value != r.Value),
            (IntValue l, FloatValue r, "!=") => new BoolValue(l.Value != r.Value),
            (FloatValue l, IntValue r, "!=") => new BoolValue(l.Value != r.Value),
            (BoolValue l, BoolValue r, "!=") => new BoolValue(l.Value != r.Value),
            (StringValue l, StringValue r, "!=") => new BoolValue(l.Value != r.Value),
            (RuneValue l, RuneValue r, "!=") => new BoolValue(l.Value != r.Value),


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
    
    //VisitNot
    public override ValueWrapper VisitNot(LanguageParser.NotContext context) {
    ValueWrapper expr = Visit(context.expr());
    if (expr is NilValue) {
        throw new SemanticError("Operacion no soportada en valor nil", context.Start);
    }if (expr is BoolValue boolValue) {
        return new BoolValue(!boolValue.Value);
    }
    throw new SemanticError($"El operador ! solo es aplicable a expresiones booleanas, no a {expr.TypeName}", context.Start);
}
    //VisitAnd
    public override ValueWrapper VisitAnd(LanguageParser.AndContext context) {
    ValueWrapper left = Visit(context.expr(0));
    
    // Evaluación de cortocircuito: si la izquierda es false, no evaluamos la derecha
    if (left is BoolValue leftBool && !leftBool.Value) {
        return new BoolValue(false);
    }
    
    ValueWrapper right = Visit(context.expr(1));
    
    if (left is NilValue || right is NilValue) {
        throw new SemanticError("Operacion no soportada en valor nil", context.Start);
    }
    
    if (left is BoolValue leftVal && right is BoolValue rightVal) {
        return new BoolValue(leftVal.Value && rightVal.Value);
    }
    
    throw new SemanticError($"El operador && requiere operandos booleanos, no {left.TypeName} y {right.TypeName}", context.Start);
}
    //VisitOr
    public override ValueWrapper VisitOr(LanguageParser.OrContext context) {
    ValueWrapper left = Visit(context.expr(0));
    
    // Evaluación de cortocircuito: si la izquierda es true, no evaluamos la derecha
    if (left is BoolValue leftBool && leftBool.Value) {
        return new BoolValue(true);
    }
    
    ValueWrapper right = Visit(context.expr(1));
    
    if (left is NilValue || right is NilValue) {
        throw new SemanticError("Operacion no soportada en valor nil", context.Start);
    }
    
    if (left is BoolValue leftVal && right is BoolValue rightVal) {
        return new BoolValue(leftVal.Value || rightVal.Value);
    }
    
    throw new SemanticError($"El operador || requiere operandos booleanos, no {left.TypeName} y {right.TypeName}", context.Start);
}
    
}