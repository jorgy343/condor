from abc import abstractmethod

from parse.Operands import Label, Register, Immediate


class InstructionBase:
    def __init__(self):
        self.label = None

    def get_label(self):
        return self.label

    def get(self):
        return self

    def __len__(self):
        return 1

    def validate(self, param, types):
        valid = False
        for t in types:
            if isinstance(param, t):
                valid = True
                break
        if valid:
            return param
        else:
            print('ERR: {} expected {} but received {}'.format(self.__class__.__name__, types, type(param)))
            return None


class MetaInstruction(InstructionBase):
    def __init__(self):
        super().__init__()
        self.inst_list = []

    def __len__(self):
        return len(self.inst_list)

    def get(self):
        return self.inst_list

    def __repr__(self):
        return '\n'.join([str(x) for x in self.get()])


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

    @abstractmethod
    def compile_instruction(self):
        pass


class JumpMeta(MetaInstruction):
    def __init__(self, dest):
        super().__init__()
        self.dest = self.validate(dest, [Register, Label])

    def meta_init(self, inst_num):
        if isinstance(self.dest, Label):
            self.inst_list.append(MOVL(self.dest, Register('r14')))
            self.inst_list.append(MOVH(self.dest, Register('r14')))
            j_inst = JumpInstruction(Register('r14'))
            j_inst.inst = inst_num
            self.inst_list.append(j_inst)

        elif isinstance(self.dest, Register):
            j_inst = JumpInstruction(self.dest)
            j_inst.inst = inst_num
            self.inst_list.append(j_inst)


class CMP(Instruction):
    def compile_instruction(self):
        pass


class J(JumpMeta):
    def __init__(self, dest):
        super().__init__(dest)
        self.meta_init(0)


class JG(JumpMeta):
    def __init__(self, dest):
        super().__init__(dest)
        self.meta_init(4)


class JL(JumpMeta):
    def __init__(self, dest):
        super().__init__(dest)
        self.meta_init(3)


class JE(JumpMeta):
    def __init__(self, dest):
        super().__init__(dest)
        self.meta_init(1)


class JNE(JumpMeta):
    def __init__(self, dest):
        super().__init__(dest)
        self.meta_init(2)


class JumpInstruction(Instruction):
    def __init__(self, dest):
        super().__init__()
        self.dest = self.validate(dest, [Register])
        self.dbus = 1
        self.func = 2

    def compile_instruction(self):
        self.a = self.dest.num


class AluBinInstruction(Instruction):
    def __init__(self, op1, op2, dest):
        super().__init__()
        self.func = 1
        self.dbus = 3
        self.op1 = self.validate(op1, [Register])
        self.op2 = self.validate(op2, [Register])
        self.dest = self.validate(dest, [Register])

    def compile_instruction(self):
        self.a = self.op1.num
        self.b = self.op2.num
        self.d = self.dest.num


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
        self.dest = self.validate(dest, [Register])
        self.src = self.validate(src, [Register])
        # if isinstance(src, Register):
        #     self.a = src.num
        # elif isinstance(src, RegRef):
        #     self.b = src.num

    def compile_instruction(self):
        self.d = self.dest.num
        self.a = self.src.num


class MOV(MetaInstruction):
    def __init__(self, src, dest):
        super().__init__()
        self.src = self.validate(src, [Label, Immediate, Register])
        self.dest = self.validate(dest, [Register])
        if isinstance(src, Label):
            self.inst_list.append(MOVL(src, dest))
            self.inst_list.append(MOVH(src, dest))
        elif isinstance(src, Register):
            self.inst_list.append(MoveReg(src, dest))


class MoveImmInstruction(Instruction):
    def __init__(self, src, dest):
        super().__init__()
        self.src = self.validate(src, [Immediate, Label])
        self.dest = self.validate(dest, [Register])

    def imm_to_exba(self, src, high):
        if high:
            src = (src & int('FFFF0000', 16)) >> 16
        else:
            src = src & int('0000FFFF', 16)

        self.e = (src & int('F000', 16)) >> 12
        self.x = (src & int('0F00', 16)) >> 8
        self.b = (src & int('00F0', 16)) >> 4
        self.a = (src & int('000F', 16))

    @abstractmethod
    def compile_instruction(self):
        pass


class MOVL(MoveImmInstruction):
    def __init__(self, src, dest):
        super().__init__(src, dest)

    def compile_instruction(self):
        self.d = self.dest.num
        if isinstance(self.src, Label):
            print('src: {}'.format(self.src))
            imm_val = self.src.pos
        else:
            imm_val = self.src.val

        self.imm_to_exba(imm_val, False)


class MOVH(MoveImmInstruction):
    def __init__(self, src, dest):
        super().__init__(src, dest)
        self.inst = 1

    def compile_instruction(self):
        self.d = self.dest.num
        if isinstance(self.src, Label):
            imm_val = self.src.pos
        else:
            imm_val = self.src.val

        self.imm_to_exba(imm_val, True)
