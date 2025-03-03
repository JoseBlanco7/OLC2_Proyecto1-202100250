//compilar gramatica
//$env:PATH += ";C:\antlr4"
//antlr4 -Dlanguage=CSharp -o analyzer -package analyzer -visitor *.g4
//dotnet watch run
grammar Language;

program: declaraciones*;

declaraciones: varDeclaciones 
    | stmt;

varDeclaciones: 
    VAR ID typeClause '=' expr ';'?
    | VAR ID typeClause ';'?
    | ID ':=' expr ';'?;      


typeClause
    : 'int'
    | 'float64'
    | 'bool'
    | 'string'
    | 'rune'
    | 'nil'
    // Agrega otros tipos que requieras
    ;
stmt : expr ';'?                                         #ExprStmt
    | 'fmt.Println(' printArgs ')' ';'?                         #PrintStmt
    | '{' declaraciones* '}'                            #BlockStmt
    | IF '(' expr ')' stmt (ELSE stmt)?                 #IfStmt
    | WHILE '(' expr ')' stmt                           #WhileStmt
    | FOR '(' forInit expr ';' expr ')' stmt           #ForStmt
    | BREAK ';'                                         #BreakStmt
    | CONTINUE ';'                                      #ContinueStmt
    | RETURN expr? ';'                                  #ReturnStmt;


forInit: varDeclaciones
    | expr ';';

printArgs: expr (',' expr)*;

expr:
    '-' expr                                            #Negate
    | '!' expr                                          #Not
    | expr call+                                        #Callee
    | expr op=('*' | '/' | '%') expr                    #MulDiv
    | expr op=('+' | '-') expr                          #AddSub
    | expr op=('>' | '<' | '>=' | '<=') expr            #Relational
    | expr op=('==' | '!=') expr                        #Equality
    | expr '&&' expr                                    #And
    | expr '||' expr                                    #Or
    | ID '+=' expr                                      #AssignAdd
    | ID '-=' expr                                      #AssignSub
    | ID '=' expr                                       #Assign
    | BOOL                                              #Bool
    | FLOAT                                             #Float
    | STRING                                            #String
    | INT                                               #Int  
    | RUNE                                              #Rune 
    | 'nil'                                             #Nil
    | ID                                                #Id
    | '(' expr ')'                                      #Parens;

call: '(' arg? ')';
arg: expr (',' expr)*;

INT: [0-9]+;
BOOL: 'true' | 'false';
FLOAT: [0-9]+ '.' [0-9]+;
STRING: '"' (ESC | ~["\\])* '"';
fragment ESC: '\\' [btnfr"\\];
RUNE: '\'' . '\'';
WS: [ \t\r\n]+ -> skip;


VAR: 'var';
FOR: 'for';
IF: 'if';
ELSE: 'else';
WHILE: 'while';
BREAK: 'break';
CONTINUE: 'continue';
RETURN: 'return';
NIL: 'nil';


ID: [a-zA-Z_][a-zA-Z0-9_]*;
COMMENT: '//' ~[\r\n]* -> skip;
ML_COMMENT: '/*' .*? '*/' -> skip;