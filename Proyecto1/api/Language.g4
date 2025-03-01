//compilar gramatica
//$env:PATH += ";C:\antlr4"
//antlr4 -Dlanguage=CSharp -o analyzer -package analyzer -visitor *.g4
//dotnet watch run
grammar Language;

program: declaraciones*;

declaraciones: varDeclaciones 
    | stmt;

varDeclaciones: 'var' ID '=' expr ';';       

stmt : expr ';'                                         #ExprStmt
    | 'print(' expr ')' ';'                             #PrintStmt
    | '{' declaraciones* '}'                            #BlockStmt
    | 'if' '(' expr ')' stmt ('else' stmt)?             #IfStmt
    | 'while' '(' expr ')' stmt                         #WhileStmt
    | 'for' '(' forInit expr ';' expr ')' stmt          #ForStmt
    | 'break' ';'                                       #BreakStmt
    | 'continue' ';'                                    #ContinueStmt
    | 'return' expr? ';'                                #ReturnStmt;
 

forInit: varDeclaciones
    | expr ';';
 

expr:
    '-' expr                                            #Negate
    | expr call+                                         #Callee
    | expr op=('*' | '/') expr                          #MulDiv
    | expr op=('+' | '-') expr                          #AddSub
    | expr op=('>' | '<' | '>=' | '<=') expr            #Relational
    | expr op=('==' | '!=') expr                        #Equality
    | ID '=' expr                                       #Assign
    | BOOL                                              #Bool
    | FLOAT                                             #Float
    | STRING                                            #String
    | INT                                               #Int   
    | ID                                                #Id
    | '(' expr ')'                                      #Parens;

call: '(' arg? ')';
arg: expr (',' expr)*;

INT: [0-9]+;
BOOL: 'true' | 'false';
FLOAT: [0-9]+ '.' [0-9]+;
STRING: '"' ~'"'* '"';
WS: [ \t\r\n]+ -> skip;
ID: [a-zA-Z]+;
COMMENT: '//' ~[\r\n]* -> skip;
ML_COMMENT: '/*' .*? '*/' -> skip;