from lex import *
from parse.Combinator import *
from parse.Oper import *


def keyword(kw):
    return Reserved(kw, KEY)


idtag = Tag(ID) ^ (lambda i: Identifier(i))

num = Tag(INT) ^ (lambda i: Immediate(i))

reg = Tag(REG) ^ (lambda i: Register(i))

ref = Tag(REF) ^ (lambda i: RegRef(i))

key = Tag(KEY) ^ (lambda i: OpCode(i))

lbl = Tag(LABEL) ^ (lambda i: Label(i))

dirtag = Tag(DIR) ^ (lambda i: Directive(i))


def sdop_val(i):
    return SDOp(i[0][0], i[0][1], i[1])


def abdop_val(i):
    return ABDOp(i[0][0][0], i[0][0][1], i[0][1], i[1])


def dop_val(i):
    return DOp(i[0], i[1])


def noarg_val(i):
    return NoArgOp(i)


def three_arg():
    print('three')
    return (key + reg + reg + reg) ^ abdop_val


def two_arg():
    print('two')
    return (key + (reg | ref | num | idtag) + (reg | ref)) ^ sdop_val


def one_arg():
    print('one')
    return (key + (reg | idtag)) ^ dop_val


def no_arg():
    print('noarg')
    return key ^ noarg_val


def parser():
    return Phrase(stmt_list())


def op():
    print('op')
    return Lazy(three_arg) | Lazy(two_arg) | Lazy(one_arg) | Lazy(no_arg)


def directive():
    return dirtag + num


def stmt():
    print('stmt')
    return Opt(lbl) + (op() | directive())


def stmt_list():
    return Rep(stmt())

