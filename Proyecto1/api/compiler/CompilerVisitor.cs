using System.Net.WebSockets;
using analyzer;

public class CompilerVisitor : LanguageBaseVisitor<ValueWrapper>{

    private ValueWrapper defaultVoid= new VoidValue();

    public string output = "";
    private Environment Currentenvironment;

    private int loopDepth = 0;  // Profundidad de bucles anidados
private int switchDepth = 0; // Profundidad de switches anidados

// Métodos auxiliares para verificar el contexto
private bool InLoop() {
    return loopDepth > 0;
}

private bool InLoopOrSwitch() {
    return loopDepth > 0 || switchDepth > 0;
}

    public CompilerVisitor(){
        Currentenvironment = new Environment(null);
        Embeded.Generate(Currentenvironment);

        var dummyToken = new Antlr4.Runtime.CommonToken(1);

        Currentenvironment.DeclareVariable("slices", new SlicesModuleValue(), null);
        Currentenvironment.DeclareVariable("strings", new StringsModuleValue(), null);

        Currentenvironment.DeclareVariable("len", new FunctionValue(new LenFunction(), "len"), dummyToken);
    Currentenvironment.DeclareVariable("append", new FunctionValue(new AppendFunction(), "append"), dummyToken);
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
    // Caso 2: var <id> <tipo> (sin valor)
else if (context.typeClause() != null && context.expr() == null)
{
    string id = context.ID().GetText();
    string declaredType = context.typeClause().GetText();
    
    ValueWrapper defaultValue;
    
    // Comprobar si es un slice
    if (declaredType.StartsWith("[]"))
    {
        // Es un slice, crear uno vacío
        string elementType = GetSliceElementType(context.typeClause());
        defaultValue = new SliceValue(elementType);
    }
    else
    {
        // No es un slice, manejar como antes
        defaultValue = declaredType switch {
            "int" => new IntValue(0),
            "float64" => new FloatValue(0.0),
            "bool" => new BoolValue(false),
            "string" => new StringValue(""),
            "rune" => new RuneValue(' '),   
            _ => throw new SemanticError($"Tipo no soportado: {declaredType}", context.Start)
        };
    }
    
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
            SliceValue slice => $"[{string.Join(", ", slice.Elements.Select(e => e switch {
                        IntValue i => i.Value.ToString(),
                        FloatValue f => string.Format("{0:0.0#####}", f.Value),
                        StringValue s => $"\"{s.Value}\"",
                        BoolValue b => b.Value.ToString().ToLower(),
                        RuneValue r => $"'{r.Value}'",
                        NilValue _ => "nil",
                        SliceValue sv => sv.ToString(),
                        _ => e.ToString()
                    }))}]",
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
   public override ValueWrapper VisitIfStmt(LanguageParser.IfStmtContext context) {
    // Evalúa la condición principal del if
    ValueWrapper condition = Visit(context.expr());
    if (condition is not BoolValue) {
        throw new SemanticError("Condición no booleana", context.Start);
    }
    
    // Si la condición es verdadera, ejecuta el bloque if
    if ((condition as BoolValue).Value) {
        return Visit(context.stmt());
    } 
    
    // Procesar bloques else if en orden
    var elseifStmts = context.elseifStmt();
    if (elseifStmts != null) {
        for (int i = 0; i < elseifStmts.Length; i++) {
            var elseifStmt = elseifStmts[i];
            ValueWrapper elseifCondition = Visit(elseifStmt.expr());
            
            if (elseifCondition is not BoolValue) {
                throw new SemanticError("Condición no booleana en else if", elseifStmt.Start);
            }
            
            if ((elseifCondition as BoolValue).Value) {
                return Visit(elseifStmt.stmt());
            }
        }
    }
    
    // Si no se cumplió ninguna condición y hay un else, ejecútalo
    if (context.elseStmt() != null) {
        return Visit(context.elseStmt());
    }
    
    return defaultVoid;
}

    //visitSwitchStmt
public override ValueWrapper VisitSwitchStmt(LanguageParser.SwitchStmtContext context) {
    switchDepth++; // Incrementar contador de switch
    
    try {
        // Evaluar la expresión del switch
        ValueWrapper switchValue = Visit(context.expr());
        
        bool matched = false;
        
        try {
            // Recorrer todos los cases
            if (context.caseStmt() != null) {
                foreach (var caseContext in context.caseStmt()) {
                    // Evaluar la expresión del case
                    ValueWrapper caseValue = Visit(caseContext.expr());
                    
                    // Comparar los valores (solo si aún no ha habido una coincidencia)
                    if (!matched && AreEqual(switchValue, caseValue)) {
                        matched = true;
                        
                        // Ejecutar las declaraciones de este case
                        Environment previousEnvironment = Currentenvironment;
                        Currentenvironment = new Environment(previousEnvironment);
                        
                        try {
                            foreach (var decl in caseContext.declaraciones()) {
                                Visit(decl);
                            }
                        } catch (BreakException) {
                            // Si hay un break, sale del switch
                            Currentenvironment = previousEnvironment;
                            return defaultVoid;
                        }
                        
                        Currentenvironment = previousEnvironment;
                    }
                }
            }
            
            // Si no hubo coincidencia y hay default, ejecutarlo
            if (!matched && context.defaultStmt() != null) {
                Environment previousEnvironment = Currentenvironment;
                Currentenvironment = new Environment(previousEnvironment);
                
                try {
                    foreach (var decl in context.defaultStmt().declaraciones()) {
                        Visit(decl);
                    }
                } catch (BreakException) {
                    // Si hay un break, sale del switch
                    Currentenvironment = previousEnvironment;
                    return defaultVoid;
                }
                
                Currentenvironment = previousEnvironment;
            }
            
            return defaultVoid;
        } catch (BreakException) {
            // Si hay break, sale del switch
            return defaultVoid;
        }
    }
    finally {
        switchDepth--; // Decrementar contador de switch
    }
}

// Método auxiliar para comparar valores
public bool AreEqual(ValueWrapper value1, ValueWrapper value2) {
    if (value1 is IntValue v1Int && value2 is IntValue v2Int) {
        return v1Int.Value == v2Int.Value;
    } 
    else if (value1 is FloatValue v1Float && value2 is FloatValue v2Float) {
        return v1Float.Value == v2Float.Value;
    }
    else if (value1 is IntValue v1IntMix && value2 is FloatValue v2FloatMix) {
        return v1IntMix.Value == v2FloatMix.Value;
    }
    else if (value1 is FloatValue v1FloatMix && value2 is IntValue v2IntMix) {
        return v1FloatMix.Value == v2IntMix.Value;
    }
    else if (value1 is StringValue v1Str && value2 is StringValue v2Str) {
        return v1Str.Value == v2Str.Value;
    }
    else if (value1 is BoolValue v1Bool && value2 is BoolValue v2Bool) {
        return v1Bool.Value == v2Bool.Value;
    }
    else if (value1 is RuneValue v1Rune && value2 is RuneValue v2Rune) {
        return v1Rune.Value == v2Rune.Value;
    }
    // Valores de diferentes tipos nunca son iguales
    return false;
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
    public override ValueWrapper VisitForStmt(LanguageParser.ForStmtContext context) {
    loopDepth++; // Incrementar contador de bucles
    
    try {
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
    finally {
        loopDepth--; // Decrementar contador de bucles
    }
}

    //VisitForConditionStmt
    public override ValueWrapper VisitForConditionStmt(LanguageParser.ForConditionStmtContext context) {
    loopDepth++; // Incrementar contador de bucles
    
    try {
        // Crear nuevo entorno
        Environment previousEnvironment = Currentenvironment;
        Currentenvironment = new Environment(previousEnvironment);
        
        // Evalúa la condición
        ValueWrapper condition = Visit(context.expr());
        if (condition is not BoolValue) {
            throw new SemanticError("Condición no booleana en for", context.Start);
        }
        
        try {
            // Mientras la condición sea verdadera
            while ((condition as BoolValue).Value) {
                try {
                    // Ejecuta el cuerpo del bucle
                    Visit(context.stmt());
                    
                    // Reevalúa la condición
                    condition = Visit(context.expr());
                    if (condition is not BoolValue) {
                        throw new SemanticError("Condición no booleana en for", context.Start);
                    }
                } catch (ContinueException) {
                    // Si hay un continue, simplemente reevalúa la condición
                    condition = Visit(context.expr());
                    if (condition is not BoolValue) {
                        throw new SemanticError("Condición no booleana en for", context.Start);
                    }
                }
            }
        } catch (BreakException) {
            // Si hay un break, sale del bucle
        }
        
        // Restaurar entorno
        Currentenvironment = previousEnvironment;
        return defaultVoid;
    }
    finally {
        loopDepth--; // Decrementar contador de bucles
    }
}

    public void VisitForBody(LanguageParser.ForStmtContext context) {
    ValueWrapper condition = Visit(context.expr(0));
    var lastEnvironment = Currentenvironment;
    
    if (condition is not BoolValue) {
        throw new SemanticError("Condición inválida", context.Start);
    }
    
    try {
        while (condition is BoolValue boolCondition && boolCondition.Value) {
            try {
                Visit(context.stmt());
                Visit(context.expr(1));
                condition = Visit(context.expr(0));
            } catch (ContinueException) {
                //Si se lanza una excepción de tipo ContinueException, continuamos con la siguiente iteración
                Visit(context.expr(1));
                condition = Visit(context.expr(0));
            }
        }
    } catch (BreakException) {
        //Si se lanza una excepción de tipo BreakException, salimos del ciclo
    }
}

    //VisitForRangeStmt
    public override ValueWrapper VisitForRangeStmt(LanguageParser.ForRangeStmtContext context) {
    loopDepth++; // Incrementar contador de bucles
    
    try {
        // Evaluar la expresión que proporciona el slice
        ValueWrapper rangeExpr = Visit(context.expr());
        
        // Asegurarse de que sea un slice
        if (!(rangeExpr is SliceValue sliceValue)) {
            throw new SemanticError("La expresión range debe ser un slice", context.expr().Start);
        }
        
        // Crear nuevo entorno
        Environment previousEnvironment = Currentenvironment;
        Currentenvironment = new Environment(previousEnvironment);
        
        // Obtener nombres de variables
        string indexVar = context.ID(0).GetText();
        string valueVar = context.ID(1).GetText();
        
        try {
    // Inicializar con valores compatibles con el tipo del slice
    Currentenvironment.DeclareVariable(indexVar, new IntValue(0), context.ID(0).Symbol);
    
    // Crear un valor predeterminado según el tipo del slice
    ValueWrapper defaultValue;
    
    // Comprobar si el ElementType es otro slice (comienza con [])
    if (sliceValue.ElementType.StartsWith("[]")) {
        // Es un slice anidado, crear un slice vacío con el tipo correcto
        defaultValue = new SliceValue(sliceValue.ElementType);
    } else {
        // No es un slice anidado, usar la lógica actual
        switch (sliceValue.ElementType) {
            case "int":
                defaultValue = new IntValue(0);
                break;
            case "float64":
                defaultValue = new FloatValue(0.0);
                break;
            case "string":
                defaultValue = new StringValue("");
                break;
            case "bool":
                defaultValue = new BoolValue(false);
                break;
            case "rune":
                defaultValue = new RuneValue(' ');
                break;
            default:
                defaultValue = new NilValue();
                break;
        }
    }
    
    Currentenvironment.DeclareVariable(valueVar, defaultValue, context.ID(1).Symbol);
    
    // Resto del código igual...
            // Iterar por el slice
            for (int i = 0; i < sliceValue.Elements.Count; i++) {
                // ASIGNAR valores a las variables ya declaradas
                Currentenvironment.AssignVariable(indexVar, new IntValue(i), context.ID(0).Symbol);
                Currentenvironment.AssignVariable(valueVar, sliceValue.Elements[i], context.ID(1).Symbol);
                
                try {
                    // Ejecutar el cuerpo del bucle
                    Visit(context.stmt());
                } catch (ContinueException) {
                    // Continúa con la siguiente iteración
                    continue;
                }
            }
        } catch (BreakException) {
            // Si hay un break, sale del bucle
        }
        
        // Restaurar entorno
        Currentenvironment = previousEnvironment;
        return defaultVoid;
    }
    finally {
        loopDepth--; // Decrementar contador de bucles
    }
}
    //VisitForInit
    //visitBreakStmt
    public override ValueWrapper VisitBreakStmt(LanguageParser.BreakStmtContext context) {
    // Verificar que estamos dentro de un bucle o switch
    if (!InLoopOrSwitch()) {
        throw new SemanticError("La sentencia break solo puede usarse dentro de un bucle o switch", context.Start);
    }
    
    // Lanzar la excepción de break
    throw new BreakException();
}
    //visitContinueStmt
    public override ValueWrapper VisitContinueStmt(LanguageParser.ContinueStmtContext context) {
    // Verificar que estamos dentro de un bucle
    if (!InLoop()) {
        throw new SemanticError("La sentencia continue solo puede usarse dentro de un bucle", context.Start);
    }
    
    // Lanzar la excepción de continue
    throw new ContinueException();
}
    //visitReturnStmt
    public override ValueWrapper VisitReturnStmt(LanguageParser.ReturnStmtContext context) {
    // Obtener el valor de retorno si está presente
    ValueWrapper returnValue = context.expr() != null 
        ? Visit(context.expr()) 
        : defaultVoid;
    
    // Lanzar la excepción de return con el valor
    throw new ReturnException(returnValue);
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
    
    // Modificar esta validación para manejar funciones variadicas
    int arity = invocable.Arity();
    if (arity >= 0 && arguments.Count != arity){
        throw new SemanticError("Numero de argumentos incorrecto", context.Start);
    } 
    else if (arity < 0 && arguments.Count < Math.Abs(arity)) {
        // Para funciones variadicas, verificar que al menos tenga |arity| argumentos
        throw new SemanticError($"Se requieren al menos {Math.Abs(arity)} argumentos", context.Start);
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


//VisitIncrement

public override ValueWrapper VisitIncrement(LanguageParser.IncrementContext context) {
    // Obtener el identificador
    string id = context.ID().GetText();
    
    // Obtener su valor actual
    ValueWrapper value = Currentenvironment.GetVariable(id, context.Start);
    
    // Incrementar según el tipo
    if (value is IntValue intVal) {
        IntValue newValue = new IntValue(intVal.Value + 1);
        Currentenvironment.AssignVariable(id, newValue, context.Start);
        return intVal; // Devuelve el valor original (post-incremento)
    } 
    else if (value is FloatValue floatVal) {
        FloatValue newValue = new FloatValue(floatVal.Value + 1);
        Currentenvironment.AssignVariable(id, newValue, context.Start);
        return floatVal; // Devuelve el valor original (post-incremento)
    } 
    else {
        throw new SemanticError("El operador ++ solo puede aplicarse a valores numéricos", context.Start);
    }
}
//VisitDecrement

public override ValueWrapper VisitDecrement(LanguageParser.DecrementContext context) {
    // Obtener el identificador
    string id = context.ID().GetText();
    
    // Obtener su valor actual
    ValueWrapper value = Currentenvironment.GetVariable(id, context.Start);
    
    // Decrementar según el tipo
    if (value is IntValue intVal) {
        IntValue newValue = new IntValue(intVal.Value - 1);
        Currentenvironment.AssignVariable(id, newValue, context.Start);
        return intVal; // Devuelve el valor original (post-decremento)
    } 
    else if (value is FloatValue floatVal) {
        FloatValue newValue = new FloatValue(floatVal.Value - 1);
        Currentenvironment.AssignVariable(id, newValue, context.Start);
        return floatVal; // Devuelve el valor original (post-decremento)
    } 
    else {
        throw new SemanticError("El operador -- solo puede aplicarse a valores numéricos", context.Start);
    }
}

// VisitSliceType

//VisitSliceLiteral
public override ValueWrapper VisitSliceLiteral(LanguageParser.SliceLiteralContext context)
{
    // 1. Obtener el tipo, por ejemplo: "int", "[]int", "[][]float64", etc.
    string elementType = GetSliceElementType(context.typeClause());

    // 2. Construir la lista de elementos
    var elements = new List<ValueWrapper>();
    if (context.arrayItems() != null)  // Cambiado de expressionList a arrayItems
    {
        // Cada arrayItem puede ser un literal primitivo o un slice literal
        foreach (var arrayItemCtx in context.arrayItems().arrayItem())  // Ajustado para arrayItems
        {
            ValueWrapper value = Visit(arrayItemCtx.expr());  // Acceder a expr() desde arrayItem

            // 3. Manejar tipos para slices multidimensionales
            // Si elementType comienza con "[]" (por ejemplo "[]int") y el valor es un SliceValue 
            // sin tipo (creado por VisitArrayLiteral), asignar el tipo adecuado
            if (elementType.StartsWith("[]") && value is SliceValue sliceVal && string.IsNullOrEmpty(sliceVal.ElementType))
            {
                // Asignar el tipo adecuado (quitar el "[]" inicial del elementType)
                string innerType = elementType.Substring(2);
                sliceVal.ElementType = innerType;

                // Si hay más niveles anidados, procesar recursivamente
                if (innerType.StartsWith("[]"))
                {
                    foreach (var innerElement in sliceVal.Elements)
                    {
                        if (innerElement is SliceValue innerSlice && string.IsNullOrEmpty(innerSlice.ElementType))
                        {
                            innerSlice.ElementType = innerType.Substring(2);
                        }
                    }
                }
            }

            elements.Add(value);
        }
    }

    // 4. Crear el SliceValue con su tipo y elementos
    return new SliceValue(elementType, elements);
}

// Método para armar la cadena de tipo, como []int o [][]float64
private string GetSliceElementType(LanguageParser.TypeClauseContext ctx)
{
    // Si la definición inicia con "[]" se repite recursivamente
    if (ctx.ChildCount == 3 
        && ctx.GetChild(0).GetText() == "[" 
        && ctx.GetChild(1).GetText() == "]")
    {
        // El tercer hijo es otro typeClause
        var innerType = ctx.GetChild(2) as LanguageParser.TypeClauseContext;
        return "[]" + GetSliceElementType(innerType);
    }
    // Caso base: int, float64, bool, string, etc.
    return ctx.GetText();
}


//VisitIndexAccess
// siendo un SliceValue, continuamos. Esto ya es suficiente para "[][]…".
public override ValueWrapper VisitIndexAccess(LanguageParser.IndexAccessContext context)
{
    ValueWrapper baseSlice = Visit(context.expr(0));
    ValueWrapper index = Visit(context.expr(1));

    if (baseSlice is not SliceValue sliceValue)
    {
        throw new SemanticError("El operador [] solo puede aplicarse a slices", context.Start);
    }
    if (index is not IntValue intIndex)
    {
        throw new SemanticError("El índice debe ser un número entero", context.expr(1).Start);
    }

    if (intIndex.Value < 0 || intIndex.Value >= sliceValue.Elements.Count)
    {
        throw new SemanticError($"Índice {intIndex.Value} fuera de rango", context.Start);
    }

    // Retornar el elemento (puede ser primitivo o SliceValue si es multidimensional)
    return sliceValue.Elements[intIndex.Value];
}
    //VisitIndexAssign
    
// Para asignaciones tipo mtx[x][y] = valor
public override ValueWrapper VisitIndexAssign(LanguageParser.IndexAssignContext context)
{
    ValueWrapper baseSlice = Visit(context.expr(0));
    if (baseSlice is not SliceValue sliceValue)
    {
        throw new SemanticError("El operador [] solo puede aplicarse a slices", context.Start);
    }

    ValueWrapper index = Visit(context.expr(1));
    if (index is not IntValue intIndex)
    {
        throw new SemanticError("El índice debe ser un número entero", context.expr(1).Start);
    }

    ValueWrapper newValue = Visit(context.expr(2));

    // Verificar rango
    if (intIndex.Value < 0)
        throw new SemanticError($"Índice {intIndex.Value} fuera de rango", context.Start);

    // Si el índice es mayor que el tamaño, opcionalmente podrías agrandar:
    while (intIndex.Value >= sliceValue.Elements.Count)
    {
        // Insertar NilValue o lógica para expandir
        sliceValue.Elements.Add(new NilValue());
    }

    // Verificar compatibilidad de tipo (si deseas)
    if (!IsCompatibleType(newValue, sliceValue.ElementType))
    {
        throw new SemanticError($"No se puede asignar {newValue.TypeName} a un slice de tipo {sliceValue.ElementType}", context.Start);
    }

    sliceValue.Elements[intIndex.Value] = newValue;
    return newValue;
}

// Si typeName empieza con "[]", verificamos que 'value' también sea un SliceValue.
public bool IsCompatibleType(ValueWrapper value, string typeName)
{
    // Caso: el typeName aún tiene notación "[]..."
    if (typeName.StartsWith("[]"))
    {
        // value debe ser de tipo SliceValue, y su ElementType debe coincidir
        if (value is SliceValue sliceVal)
        {
            // "[]int" -> recortamos "[]" y comparamos con sliceVal.ElementType
            string nestedType = typeName.Substring(2);
            return sliceVal.ElementType == nestedType 
                || nestedType.StartsWith("[]"); // o comparación más fina si quieres
        }
        return false;
    }
    
    // Caso base: tipos primitivos
    return (typeName == "int" && value is IntValue) ||
           (typeName == "float64" && (value is FloatValue || value is IntValue)) ||
           (typeName == "bool" && value is BoolValue) ||
           (typeName == "string" && value is StringValue) ||
           (typeName == "rune" && value is RuneValue) ||
           (typeName == "nil" && value is NilValue);
}


    //VisitDotCallee    
public override ValueWrapper VisitDotCallee(LanguageParser.DotCalleeContext context) {
    // Obtener el valor base (ej: 'slices' en slices.Index)
    ValueWrapper baseValue = Visit(context.expr());
    
    // Obtener el nombre del método (ej: 'Index' en slices.Index)
    string methodName = context.ID().GetText();
    
    // Verificar si es un módulo con funciones
    if (baseValue is SlicesModuleValue slicesModule)
    {
        Invocable function = slicesModule.GetFunction(methodName);
        if (function != null)
        {
            // Llamar a la función con los argumentos
            return VisitCall(function, context.call().arg());
        }
    }
    else if (baseValue is StringsModuleValue stringsModule)
    {
        Invocable function = stringsModule.GetFunction(methodName);
        if (function != null)
        {
            // Llamar a la función con los argumentos
            return VisitCall(function, context.call().arg());
        }
    }
    
    throw new SemanticError($"El método {methodName} no existe en {baseValue.TypeName}", context.Start);
}



public override ValueWrapper VisitArrayLiteral(LanguageParser.ArrayLiteralContext context)
{
    // Crea un slice sin tipo específico
    var elements = new List<ValueWrapper>();
    
    if (context.expressionList() != null)
    {
        foreach (var exprCtx in context.expressionList().expr())
        {
            elements.Add(Visit(exprCtx));
        }
    }
    
    // Esto será interpretado como una fila de una matriz
    return new SliceValue("", elements);  // El tipo se inferirá según el contexto
}

}