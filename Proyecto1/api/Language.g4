//compilar gramatica
//$env:PATH += ";C:\antlr4"
//antlr4 -Dlanguage=CSharp -o analyzer -package analyzer -visitor *.g4
//dotnet watch run
grammar Language;

program: declaraciones*;

declaraciones: varDeclaciones 
    | stmt;

varDeclaciones: 
    'var' ID typeClause '=' expr ';'?
    | 'var' ID typeClause ';'?
    | ID ':=' expr ';'?;       


typeClause
    : 'int'
    | 'float64'
    | 'bool'
    | 'string'
    // Agrega otros tipos que requieras
    ;
stmt : expr ';'?                                         #ExprStmt
    | 'fmt.print(' expr ')' ';'?                         #PrintStmt
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
    | expr call+                                        #Callee
    | expr op=('*' | '/') expr                          #MulDiv
    | expr op=('+' | '-') expr                          #AddSub
    | expr op=('>' | '<' | '>=' | '<=') expr            #Relational
    | expr op=('==' | '!=') expr                        #Equality
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
ID: [a-zA-Z_][a-zA-Z0-9_]*;
COMMENT: '//' ~[\r\n]* -> skip;
ML_COMMENT: '/*' .*? '*/' -> skip;