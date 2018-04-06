package com.chronnis.casm.inst;

import com.chronnis.casm.ast.Node;
import com.chronnis.casm.ast.OpCode;

/**
 * Created by Dylan on 4/4/2018.
 */
public abstract class Instruction
{

    protected int d = 0;
    protected int e = 0;
    protected int x = 0;
    protected int b = 0;
    protected int a = 0;

    public abstract void argNum(int i, Node n, int line);
    protected abstract OpCode getOp();
    protected abstract int getFunc();
    protected abstract int getDb();
    protected abstract int getInst();

    public void setImm(int imm) {
        imm = imm & 0xFFFF;
        e = (imm & 0xF000) >> 12;
        x = (imm & 0x0F00) >> 8;
        b = (imm & 0x00F0) >> 4;
        a = (imm & 0x000F);
    }

    public int getImm() {
        return (e << 12) | (x << 8) | (b << 4) | a;
    }

    public void setD(int d)
    {
        this.d = d;
    }

    public void setB(int b)
    {
        this.b = b;
    }

    public void setA(int a)
    {
        this.a = a;
    }

    public Integer serialize() {
        return getFunc() << 29
                | getDb() << 26
                | getInst() << 20
                | d << 16
                | e << 12
                | x << 8
                | b << 4
                | a;
    }

    protected void error(int line, int arg, String expected, Node n) {
        System.out.println(line + ": ERR -- " + getOp().toString() + " arg[" + arg + "] expected " + expected + ", received " + n.getClass().getSimpleName());
    }

    protected void error(int line, String msg) {
        System.out.println(line + ": ERR -- " + msg);
    }
}
