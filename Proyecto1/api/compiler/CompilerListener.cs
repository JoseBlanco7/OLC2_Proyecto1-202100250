using analyzer;
using Microsoft.OpenApi.Models;

public class CompilerListener : LanguageBaseListener{
    private Stack<int> stack = new Stack<int>();

    public int GetResult(){
        return stack.Peek();
    }

    //ExitInt
    public override void ExitInt(LanguageParser.IntContext context){
        Console.WriteLine("Pushing: " + context.GetText());
        stack.Push(int.Parse(context.GetText()));
    }

    //ExitMulDiv
    public override void ExitMulDiv(LanguageParser.MulDivContext context){
        int right = stack.Pop();
        Console.WriteLine("Result of:" +right);
        int left = stack.Pop();
        Console.WriteLine("Result of:" +left);
        var result = context.op.Text == "*" ? left * right : left / right;
        Console.WriteLine("Realizando la operacion:" + context.op.Text +"=" + result);
        stack.Push(result);
    }


    //ExitAddSub
    public override void ExitAddSub(LanguageParser.AddSubContext context){
        int right = stack.Pop();
        Console.WriteLine("Result of:" +right);
        int left = stack.Pop();
        Console.WriteLine("Result of:" +left);
        var result = context.op.Text == "+" ? left + right : left - right;
        Console.WriteLine("Realizando la operacion:" + context.op.Text +"=" + result);
        stack.Push(result);
    }
    
}