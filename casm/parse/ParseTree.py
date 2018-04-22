from lex import ID, INT, REG, REF, KEY, LABEL, DIR
from parse.Combinator import *
from parse.Operands import *
from parse.Instructions import *

idtag = Tag(ID) ^ (lambda i: Label.get(i))

num = Tag(INT) ^ (lambda i: Immediate(i))

reg = Tag(REG) ^ (lambda i: Register(i))

ref = Tag(REF) ^ (lambda i: RegRef(i))

key = Tag(KEY) ^ (lambda i: str(i).upper())

lbl = Tag(LABEL) ^ (lambda i: Label.get(i))

dirtag = Tag(DIR) ^ (lambda i: Directive(i))

opmap = {'J': (lambda dest: J(dest)),
         'JG': (lambda dest: JG(dest)),
         'JL': (lambda dest: JL(dest)),
         'JE': (lambda dest: JE(dest)),
         'JNE': (lambda dest: JNE(dest)),
         'MOVL': (lambda source, dest: MOVL(source, dest)),
         'MOV': (lambda source, dest: MOV(source, dest)),
         'ADD': (lambda a, b, dest: ADD(a, b, dest)),
         'SUB': (lambda a, b, dest: SUB(a, b, dest))}


def three_arg():
    return (key + reg + reg + reg) ^ (lambda f: opmap[f[0][0][0]](f[0][0][1], f[0][1], f[1]))


def two_arg():
    return (key + (reg | ref | num | idtag) + (reg | ref)) ^ (lambda f: opmap[f[0][0]](f[0][1], f[1]))


def one_arg():
    return (key + (reg | idtag)) ^ (lambda f: opmap[f[0]](f[1]))


def no_arg():
    return key ^ (lambda f: opmap[f])


def parser():
    return Phrase(program()) ^ (lambda x: Program(x))


def instruction():
    return Lazy(three_arg) | Lazy(two_arg) | Lazy(one_arg) | Lazy(no_arg)


def directive():
    return dirtag + num


def operation():
    return Opt(lbl) + (instruction() | directive())


def program():
    return Rep(operation())


class Program:
    def __init__(self, op_list=None):
        for label, op in op_list:
            op.label = label
        labels, ops = zip(*op_list)
        print('oplist')
        for i in op_list:
            print(i)
        self.op_list = ops
        self.labels = {x.name: x for x in labels if x}
        self.base_address = 0

    def __len__(self):
        l = 0
        for inst in self.op_list:
            l += len(inst)
        return l

    def add(self, op):
        if isinstance(op, list):
            self.op_list.extend(op)
        else:
            self.op_list.append(op)

    def __repr__(self):
        return '\n'.join([str(i) for i in self.op_list])
