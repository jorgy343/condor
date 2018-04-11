
class BaseOp:

    jump_ops = ['J', 'JNE', 'JE', 'JG', 'JL']
    mov_ops = ['MOV']

    def __init__(self, op):
        self.op = op

    def __len__(self):
        return 1


class NoArgOp(BaseOp):

    def __repr__(self):
        return '{}'.format(self.op)


class DOp(BaseOp):
    def __init__(self, op, dest):
        super().__init__(op)
        self.dest = dest

    def __repr__(self):
        return '{} {}'.format(self.op, self.dest)

    def __len__(self):
        if self.op.code in BaseOp.jump_ops and isinstance(self.dest, Identifier):
            return 3
        else:
            return 1


class SDOp(BaseOp):
    def __init__(self, op, src, dest):
        super().__init__(op)
        self.source = src
        self.dest = dest

    def __repr__(self):
        return '{} {}, {}'.format(self.op, self.source, self.dest)

    def __len__(self):
        if self.op.code in BaseOp.mov_ops and isinstance(self.source, Identifier):
            return 2
        else:
            return 1


class ABDOp(BaseOp):
    def __init__(self, op, a_op, b_op, dest):
        super().__init__(op)
        self.A = a_op
        self.B = b_op
        self.dest = dest

    def __repr__(self):
        return '{} {}, {}, {}'.format(self.op, self.A, self.B, self.dest)


class Label:
    def __init__(self, name):
        self.name = name

    def __repr__(self):
        return self.name


class Directive:
    def __init__(self, code):
        self.code = code


class Immediate:
    def __init__(self, val):
        self.val = self.convert(val) if isinstance(val, str) else val

    def convert(self, val):
        if len(val) > 2:
            pref = val[0:2]
            if pref == '0x':
                return int(val[2:], 16)
            elif pref == '0b':
                return int(val[2:], 2)
        return int(val)

    def __repr__(self):
        return str(self.val)


class Identifier:
    def __init__(self, ident):
        self.val = str(ident)

    def __repr__(self):
        return self.val


class Register:
    def __init__(self, reg):
        self.num = int(reg.replace('r', ''))

    def __repr__(self):
        return 'r' + str(self.num)


class RegRef:
    def __init__(self, reg):
        self.num = int(reg.replace('r', ''))

    def __repr__(self):
        return '[r' + str(self.num) + ']'


class OpCode:
    def __init__(self, code):
        self.code = str(code).upper()

    def __repr__(self):
        return self.code
