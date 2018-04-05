package com.chronnis.casm.inst;

import com.chronnis.casm.ast.Node;
import com.chronnis.casm.ast.OpCode;
import com.chronnis.casm.ast.Register;

/**
 * Created by Dylan on 4/4/2018.
 */
public class MOV extends Instruction
{

    @Override
    protected OpCode getOp()
    {
        return OpCode.MOV;
    }

    @Override
    protected int getFunc()
    {
        return 0;
    }

    @Override
    protected int getDb()
    {
        return 1;
    }

    @Override
    protected int getInst()
    {
        return 2;
    }

    @Override
    public String toString() {
        return String.join(" ", getOp().toString(), String.format("0x%04X", getImm()), "r" + d);
    }

    @Override
    public void argNum(int i, Node n, int line)
    {
        switch (i) {
            case 0:
                if(!Register.class.isAssignableFrom(n.getClass()))
                {
                    error(line, i, "Register", n);
                } else {
                    Register r = (Register) n;
                    setA(r.getRegnum());
                }
                break;
            case 1:
                if(!Register.class.isAssignableFrom(n.getClass())) {
                    error(line, i, "Register", n);
                }
                else {
                    Register r = (Register) n;
                    setD(r.getRegnum());
                }
        }
    }

}
