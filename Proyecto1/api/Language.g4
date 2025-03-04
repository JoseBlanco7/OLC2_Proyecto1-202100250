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
    | '[' ']' typeClause
    // Agrega otros tipos que requieras
    ;
stmt : expr ';'?                                                #ExprStmt
    | 'fmt.Println(' printArgs ')' ';'?                         #PrintStmt
    | '{' declaraciones* '}'                                    #BlockStmt
    | IF '('? expr ')'? stmt elseifStmt* elseStmt?              #IfStmt
    | SWITCH expr '{' caseStmt* defaultStmt? '}'                #SwitchStmt
    | WHILE '(' expr ')' stmt                                   #WhileStmt
    | FOR '('? forInit expr ';' expr ')'? stmt                    #ForStmt
    | FOR expr stmt                                             #ForConditionStmt
    | FOR ID ',' ID ':=' 'range' expr stmt                      #ForRangeStmt
    | BREAK ';'?                                                 #BreakStmt
    | CONTINUE ';'?                                              #ContinueStmt
    | RETURN expr? ';'?                                          #ReturnStmt;


elseifStmt: ELSE IF '('? expr ')'? stmt;
elseStmt: ELSE stmt;


caseStmt: CASE expr ':' declaraciones*;
defaultStmt: DEFAULT ':' declaraciones*;

forInit: varDeclaciones
    | expr ';';

printArgs: expr (',' expr)*;

expr:
    '-' expr                                            #Negate
    | '!' expr                                          #Not
    | ID '++'                                           #Increment
    | ID '--'                                           #Decrement
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
    | '[' ']' typeClause '{' arrayItems? '}'        #SliceLiteral
    | '{' expressionList? '}'                           #ArrayLiteral
    | expr '[' expr ']'                                 #IndexAccess
    | expr '[' expr ']' '=' expr                        #IndexAssign
    | expr '.' ID call                                  #DotCallee
    | ID                                                #Id
    | '(' expr ')'                                      #Parens;

call: '(' arg? ')';
arg: expr (',' expr)*;

arrayItems: arrayItem (',' arrayItem)* ','?;
arrayItem: expr;

expressionList: expr (',' expr)*;



INT: [0-9]+;
BOOL: 'true' | 'false';
FLOAT: [0-9]+ '.' [0-9]+;
STRING: '"' (ESC | ~["\\])* '"';
fragment ESC: '\\' [btnfr"\\];
RUNE: '\'' . '\'';
WS: [ \t\r\n]+ -> skip;


VAR: 'var';

IF: 'if';
ELSE: 'else';

SWITCH: 'switch';
CASE: 'case';
DEFAULT: 'default';

DOT: '.';

FOR: 'for';
WHILE: 'while';
BREAK: 'break';
CONTINUE: 'continue';
RETURN: 'return';
NIL: 'nil';


ID: [a-zA-Z_][a-zA-Z0-9_]*;
COMMENT: '//' ~[\r\n]* -> skip;
ML_COMMENT: '/*' .*? '*/' -> skip;