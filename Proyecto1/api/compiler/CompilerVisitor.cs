using analyzer;

public class CompilerVisitor : LanguageBaseVisitor<ValueWrapper>{

    private ValueWrapper defaultVoid= new VoidValue();

    public string output = "";
    private Environment Currentenvironment = new Environment(null);

    //VisitProgram
    public override ValueWrapper VisitProgram(LanguageParser.ProgramContext context){
        foreach(var declaraciones in context.declaraciones()){
            Visit(declaraciones);
        }
        return defaultVoid;
    }

    //VisitVarDeclaciones
    public override ValueWrapper VisitVarDeclaciones(LanguageParser.VarDeclacionesContext context){
        string id = context.ID().GetText();
        ValueWrapper value = Visit(context.expr());
        Currentenvironment.DeclareVariable(id, value);
        return defaultVoid;
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
            FloatValue f => f.Value.ToString(),
            StringValue s => s.Value,
            BoolValue b => b.Value.ToString(),
            VoidValue v => "void",
            _ => throw new Exception($"Tipo no soportado: {value.GetType()}")
        } + "\n";
        output += "\n";
        return defaultVoid;
    }

    //VisitId
    public override ValueWrapper VisitId(LanguageParser.IdContext context){
        string id = context.ID().GetText();
        return Currentenvironment.GetVariable(id);
        
        
    }


    //VisitParens
    public override ValueWrapper VisitParens(LanguageParser.ParensContext context){
        return Visit(context.expr());
    }

    //VisitNegate
    public override ValueWrapper VisitNegate(LanguageParser.NegateContext context){
        //return -Visit(context.expr());
        ValueWrapper value = Visit(context.expr());
        return value switch{
            IntValue i => new IntValue(-i.Value),
            FloatValue f => new FloatValue(-f.Value),
            _ => throw new Exception($"Operacion no soportada: -{value.GetType()}")
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
        var op = context.op.Text;
        return (left, right, op) switch{
            (IntValue l, IntValue r, "*") => new IntValue(l.Value * r.Value),
            (IntValue l, IntValue r, "/") => new IntValue(l.Value / r.Value),
            (FloatValue l, FloatValue r, "*") => new FloatValue(l.Value * r.Value),
            (FloatValue l, FloatValue r, "/") => new FloatValue(l.Value / r.Value),
            (StringValue l, IntValue r, "*") => new StringValue(string.Concat(Enumerable.Repeat(l.Value, r.Value))),
            _ => throw new Exception($"Operacion no soportada: {left.GetType()} {op} {right.GetType()}")
        };
    }
    //VisitAddSub
    public override ValueWrapper VisitAddSub(LanguageParser.AddSubContext context){
        ValueWrapper left = Visit(context.expr(0));
        ValueWrapper right = Visit(context.expr(1));
        var op = context.op.Text;
        return (left, right, op) switch{
            (IntValue l, IntValue r, "+") => new IntValue(l.Value + r.Value),
            (IntValue l, IntValue r, "-") => new IntValue(l.Value - r.Value),
            (FloatValue l, FloatValue r, "+") => new FloatValue(l.Value + r.Value),
            (FloatValue l, FloatValue r, "-") => new FloatValue(l.Value - r.Value),
            (StringValue l, StringValue r, "+") => new StringValue(l.Value + r.Value),
            (IntValue l, StringValue r, "+") => new StringValue(l.Value.ToString() + r.Value),
            (StringValue l, IntValue r, "+") => new StringValue(l.Value + r.Value.ToString()),
            _ => throw new Exception($"Operacion no soportada: {left.GetType()} {op} {right.GetType()}")
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
    public override ValueWrapper VisitString(LanguageParser.StringContext context){
        return new StringValue(context.STRING().GetText());
    }
    //VisitRelational
    public override ValueWrapper VisitRelational(LanguageParser.RelationalContext context){
        ValueWrapper left = Visit(context.expr(0));
        ValueWrapper right = Visit(context.expr(1));
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
            _ => throw new Exception($"Operacion no soportada: {left.GetType()} {op} {right.GetType()}")
        };
    }   
    //VisitAssign
    public override ValueWrapper VisitAssign(LanguageParser.AssignContext context){
        string id = context.ID().GetText();
        ValueWrapper value = Visit(context.expr());
        return Currentenvironment.AssignVariable(id, value);}   
    //VisitEquality
    public override ValueWrapper VisitEquality(LanguageParser.EqualityContext context){
        ValueWrapper left = Visit(context.expr(0));
        ValueWrapper right = Visit(context.expr(1));
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
            _ => throw new Exception($"Operacion no soportada: {left.GetType()} {op} {right.GetType()}")
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
    
    
}