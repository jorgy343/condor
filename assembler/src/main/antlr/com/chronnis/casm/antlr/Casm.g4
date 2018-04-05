grammar Casm;

@header {
    package com.chronnis.casm.antlr;
}
prog: stmt (EOL stmt)* EOF;

stmt: label? (op | directive)? comment?;

op: KEYWORD exprlist?;
label: LABEL;
directive: DIRECTIVE number;
comment: COMMENT;

exprlist: expr+;

expr: (regref | reg | number | label);
number: NUMBER;
regref: REGREF;
reg: REG;

REG: 'r'(([1][0-5])|NUM);
REGREF: '[' REG ']';

DIRECTIVE: 'd32';

KEYWORD: 'movl'
       | 'movh'
       | 'mov'
       | 'ld'
       | 'str'
       | 'j'
       | 'je'
       | 'jne'
       | 'jl'
       | 'jg'
       | 'add'
       | 'sub'
       | 'mul'
       | 'div'
       | 'rem'
       | 'neg'
       | 'and'
       | 'or'
       | 'xor'
       | 'not'
       | 'lsh'
       | 'rsh'
       | 'arsh'
       ;

LABEL: ID ':';
ID: ALPHA ALPHANUM*;
NUMBER: NUMPREFIX? HEXCHAR+;
NUMPREFIX: '0' [xb];

EOL: '\r'? '\n';

COMMENT: ';' ~[\r\n]* -> skip;
SEP: ',' -> skip;
WS: [ \t] -> skip;

fragment HEXCHAR: [0-9A-Fa-f];
fragment NUM: [0-9];
fragment ALPHANUM: [a-zA-Z0-9_];
fragment ALPHA: [a-zA-Z_];
