from parse import Identifier, Register, RegRef, Immediate


class InstructionBase:

    def get(self):
        return self

    def __len__(self):
        return 1


class MetaInstruction(InstructionBase):
    def __init__(self):
        super().__init__()
        self.pre = []
        self.post = []
        self.inst = None

    def __len__(self):
        return len(self.pre) + len(self.post) + 1

    def get(self):
        i = []
        i.extend(self.pre)
        i.append(self.inst)
        i.extend(self.post)
        return i


class Instruction(InstructionBase):
    def __init__(self):
        super().__init__()
        self.func = 0
        self.dbus = 0
        self.inst = 0
        self.d = 0
        self.e = 0
        self.x = 0
        self.b = 0
        self.a = 0

    def __repr__(self):
        return '{:08x}'.format(self.func << 29 |
                               self.dbus << 26 |
                               self.inst << 20 |
                               self.d << 16 |
                               self.e << 12 |
                               self.x << 8 |
                               self.b << 4 |
                               self.a)


class J(MetaInstruction):
    def __init__(self, dest):
        super().__init__()

        if isinstance(dest, Identifier):
            if dest.val in Compiler.labels:
                label_pos = Compiler.labels[dest.val]
                self.pre.append(MOVL(Immediate(label_pos), Register('r14')))
                self.pre.append(MOVH(Immediate(label_pos), Register('r14')))
                self.inst = JumpInstruction(Register('r14'))
            else:
                print('error')
        elif isinstance(dest, Register):
            self.inst = JumpInstruction(dest)


class CMP(Instruction):
    pass


class ConditionalJump(J):
    def __init__(self, dest):
        super().__init__(dest)
        self.pre.insert(0, CMP())


class JG(ConditionalJump):
    pass


class JL(ConditionalJump):
    pass


class JE(ConditionalJump):
    pass


class JNE(ConditionalJump):
    pass


class JumpInstruction(Instruction):
    def __init__(self, dest):
        super().__init__()
        self.b = dest.num
        self.func = 2


class MoveImmInstruction(Instruction):
    def __init__(self, dest):
        super().__init__()
        self.d = dest.num

    def imm_to_exba(self, src):
        self.e = (src & int('F000', 16)) >> 12
        self.x = (src & int('0F00', 16)) >> 8
        self.b = (src & int('00F0', 16)) >> 4
        self.a = (src & int('000F', 16))


class AluBinInstruction(Instruction):
    def __init__(self, a, b, d):
        super().__init__()
        self.func = 1
        self.dbus = 3
        self.a = a.num
        self.b = b.num
        self.d = d.num


class SUB(AluBinInstruction):
    def __init__(self, a, b, d):
        super().__init__(a, b, d)
        self.inst = 1


class ADD(AluBinInstruction):
    pass


class MUL(AluBinInstruction):
    def __init__(self, a, b, d):
        super().__init__(a, b, d)
        self.inst = 2


class MoveReg(Instruction):
    def __init__(self, src, dest):
        super().__init__()
        self.dbus = 1
        self.inst = 2
        self.d = dest.num
        if isinstance(src, Register):
            self.a = src.num
        elif isinstance(src, RegRef):
            self.b = src.num
        else:
            print('error')


class MOV(MetaInstruction):
    def __init__(self, src, dest):
        super().__init__()
        if isinstance(src, Identifier):
            if src.val in Compiler.labels:
                pos = Compiler.labels[src.val]
                self.pre.append(MOVL(pos, dest))
                self.post.append(MOVH(pos, dest))
        elif isinstance(src, Register) or isinstance(src, RegRef):
            self.inst = MoveReg(src, dest)


class MOVL(MoveImmInstruction):
    def __init__(self, src, dest):
        super().__init__(dest)
        src = src.val
        self.imm_to_exba((src & int('0000FFFF', 16)))


class MOVH(MoveImmInstruction):
    def __init__(self, src, dest):
        super().__init__(dest)
        src = src.val
        self.inst = 1
        self.imm_to_exba(((src & int('FFFF0000', 16)) >> 16))


class Program:
    def __init__(self):
        self.instruction_list = []

    def __len__(self):
        l = 0
        for inst in self.instruction_list:
            l += len(inst)
        return l

    def add(self, inst):
        if isinstance(inst, list):
            self.instruction_list.extend(inst)
        else:
            self.instruction_list.append(inst)

    def __repr__(self):
        s = ''
        for i in self.instruction_list:
            s += str(i) + '\n'
        return s


class Compiler:
    opmap = {'J': (lambda x: J(x.dest)),
             'JG': (lambda x: JG(x.dest)),
             'JL': (lambda x: JL(x.dest)),
             'JE': (lambda x: JE(x.dest)),
             'JNE': (lambda x: JNE(x.dest)),
             'MOVL': (lambda x: MOVL(x.source, x.dest)),
             'MOV': (lambda x: MOV(x.source, x.dest)),
             'ADD': (lambda x: ADD(x.A, x.B, x.dest)),
             'SUB': (lambda x: SUB(x.A, x.B, x.dest))}

    labels = {}

    def comp(self, result):
        op_list = result.value
        self.create_label_table(op_list)
        print(Compiler.labels)

        prog = Program()
        for op in op_list:
            oper = op[1]
            opcode = str(oper.op)
            opfunc = Compiler.opmap[opcode]

            prog.add(opfunc(oper).get())

        return prog

    def create_label_table(self, op_list):
        table = {}
        pos = 0
        for res in op_list:
            if res[0]:
                table[res[0].name[:-1]] = pos
            pos += len(res[1])

        Compiler.labels = table
