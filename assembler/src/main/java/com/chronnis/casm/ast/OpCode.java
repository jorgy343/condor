package com.chronnis.casm.ast;

/**
 * Created by Dylan on 4/3/2018.
 */
public enum OpCode
{
    MOVL,
    MOVH,
    MOV,
    LD,
    STR,
    J,
    JE,
    JNE,
    JL,
    JG,
    ADD,
    SUB,
    MUL,
    DIV,
    REM,
    NEG,
    AND,
    OR,
    XOR,
    NOT,
    LSH,
    RSH,
    ARSH;

    public static OpCode fromString(String code) {
        return OpCode.valueOf(code.toUpperCase());
    }

}
