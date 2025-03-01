public class Embeded{
    public static void Generate(Environment env){
        env.DeclareVariable("time",new FunctionValue(new TimeEmbeded(),"time"), null);

    }
}

//funcion enbebida del tiempo
public class TimeEmbeded: Invocable{
    public int Arity(){
        return 0;
    }
    public ValueWrapper Invoke(List<ValueWrapper> args, CompilerVisitor visitor){
        return new StringValue(DateTime.Now.ToString());
    }
}