from parse.Instructions import *
from parse.ParseTree import Program
from parse.ParseTree import parser


class Compiler:

    def comp(self, program):

        self.resolve_labels(program)
        self.expand_meta_ops(program)
        self.compile_program(program)
        print(program)

        return program

    def resolve_labels(self, prog):
        pos = 0
        for op in prog.op_list:
            print('{} - {}'.format(pos, op.__class__.__name__))
            lbl = op.get_label()
            if lbl:
                print('lbl: {} -- {}'.format(lbl, pos))
                prog.labels[lbl.name].pos = pos
                print('lbl: {} -- {}'.format(lbl, pos))
            pos += len(op)

        print(prog.labels)

    def expand_meta_ops(self, prog):
        op_list = prog.op_list
        new_list = self.expand_op(op_list)
        print(len(op_list))
        print(len(new_list))
        prog.op_list = new_list

    def expand_op(self, op_list):
        l = []
        for op in op_list:
            if isinstance(op, Instruction):
                l.append(op)
            elif isinstance(op, MetaInstruction):
                l.extend(self.expand_op(op.get()))
            else:
                print('ERR: Non Instruction in Op List')
        return l

    def compile_program(self, program):
        op_list = program.op_list
        for op in op_list:
            op.compile_instruction()
        pass
