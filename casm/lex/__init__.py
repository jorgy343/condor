import sys
from enum import Enum
import re


def lex(chars, token_map):
    pos = 0
    tokens = []
    while pos < len(chars):
        match = None
        for token_expr in token_map:
            pattern, tag = token_expr
            pattern = re.compile(pattern)
            match = pattern.match(chars, pos)
            if match:
                text = match.group(0)
                if tag:
                    token = (text, tag)
                    tokens.append(token)
                break
        if not match:
            sys.stderr.write('Illegal character: %s\n' % chars[pos])
            sys.exit(1)
        else:
            pos = match.end(0)
    return tokens


class TokenType(Enum):
    ID = 1
    LABEL = 2
    REG = 3
    KEY = 4
    INT = 5
    REF = 6
    DIR = 7

    def __repr__(self):
        return self.name


ID = TokenType.ID
LABEL = TokenType.LABEL
REG = TokenType.REG
KEY = TokenType.KEY
INT = TokenType.INT
REF = TokenType.REF
DIR = TokenType.DIR
