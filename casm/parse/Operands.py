class Label:
    labels = {}

    @classmethod
    def get(cls, name):
        name = str(name).replace(':', '')
        if name in cls.labels:
            print('returning cached label {}'.format(name))
            return cls.labels[name]
        else:
            print('creating new label {}'.format(name))
            lbl = Label(name)
            cls.labels[name] = lbl
            return lbl

    def __init__(self, name):
        self.name = str(name).replace(':', '')
        self.pos = 0

    def __repr__(self):
        return '{}: {}'.format(self.name, self.pos)


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

