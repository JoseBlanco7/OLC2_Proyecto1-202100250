//compilar gramatica
//antlr4 -Dlanguage=CSharp -o analyzer -package analyzer -visitor *.g4
grammar Language;

program: declaraciones*;

declaraciones: varDeclaciones 
    | stmt;

varDeclaciones: 'var' ID '=' expr ';';       

stmt : expr ';'                                         #ExprStmt
    | 'print(' expr ')' ';'                             #PrintStmt
    | '{' declaraciones* '}'                            #BlockStmt;


expr:
    '-' expr                                            #Negate
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

INT: [0-9]+;
BOOL: 'true' | 'false';
FLOAT: [0-9]+ '.' [0-9]+;
STRING: '"' ~'"'* '"';
WS: [ \t\r\n]+ -> skip;
ID: [a-zA-Z]+;
