#!/usr/bin/env python3
from lex import *
from parse import parser
from compile import Compiler

keywords = '|'.join([
    'movl',
    'movh',
    'mov',
    'ld',
    'str',
    'jne',
    'je',
    'jl',
    'jg',
    'j',
    'add',
    'sub',
    'mul',
    'div',
    'rem',
    'neg',
    'and',
    'or',
    'xor',
    'not',
    'lsh',
    'arsh',
    'rsh'
])
print(keywords)

token_list = [
    (r';[^\n]*', None),
    (r'[ \n\t,]+', None),
    (keywords, KEY),
    (r'\[(1[0-5]|[0-9])\]', REF),
    (r'r(1[0-5]|[0-9])', REG),
    (r'[a-zA-Z][a-zA-Z0-9_]*:', LABEL),
    (r'@[a-zA-Z0-9_]+', DIR),
    (r'[a-zA-Z][a-zA-Z0-9_]*', ID),
    (r'(0[xb])?[0-9]+', INT)
]


def casm_parse(tokens):
    ast = parser()(tokens, 0)
    return ast


def main():
    tokens = []
    with open("test2.casm", 'r') as source:
        for line in source:
            print(line)
            tokens.extend(lex(line, token_list))
    for t in tokens:
        print(t)
    result = casm_parse(tokens)
    for item in result.value:
        print(item)

    program = Compiler().comp(result)

    print(program)


if __name__ == '__main__':
    main()
