grammar Casm;

prog
    : statement (EOL statement)* EOF
    ;

statement
    : label? (instruction | directive)? comment?
    ;

label
    : LABEL
    ;

instruction
    : NAME expressionList?
    ;

directive
    : DIRECTIVE expressionList?
    ;

expressionList
    : expression (',' expression)*
    ;

expression
    : (register | registerReference | NUMBER | NAME)
    ;

register
    : REGISTER
    ;

registerReference
    : REGISTERREFERENCE
    ;

comment
    : COMMENT
    ;

LABEL
    : NAME ':'
    ;

REGISTERREFERENCE
    : '[' REGISTER ']'
    ;

REGISTER
    : [rR] ([1][0-5] | [0-9])
    ;

DIRECTIVE
    : '@' NAME
    ;

COMMENT
    : ';' ~[\r\n]*
    ;

NAME
    : [a-zA-Z_] [a-zA-Z0-9_]*
    ;

NUMBER
    : (HEXNUMBER | DECIMALNUMBER | BINARYNUMBER)
    ;

HEXNUMBER
    : '0' [xX] [_]* [0-9a-fA-F] [0-9a-fA-F_]*
    ;

DECIMALNUMBER
    : ([0-9]+ | '0' [dD] [_]* [0-9] [0-9_]*)
    ;

BINARYNUMBER
    : '0' [bB] [_]* [0-1] [0-1_]*
    ;

EOL
    : '\r'? '\n'
    ;

WS
    : [ \t] -> skip
    ;