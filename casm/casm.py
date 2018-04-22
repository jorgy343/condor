#!/usr/bin/env python3
from lex import *
from parse import parser, Compiler

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
    return ast.value


def main():
    tokens = []
    with open("test2.casm", 'r') as source:
        # print(line)
        tokens.extend(lex(source.read(), token_list))
    # for t in tokens:
    #     print(t)
    program = casm_parse(tokens)
    print(program)
    # print(type(program))

    program = Compiler().comp(program)
    #
    # print(program)


if __name__ == '__main__':
    main()
