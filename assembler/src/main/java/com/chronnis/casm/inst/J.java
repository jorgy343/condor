package com.chronnis.casm.inst;

import com.chronnis.casm.ast.Label;
import com.chronnis.casm.ast.Node;
import com.chronnis.casm.ast.OpCode;
import com.chronnis.casm.ast.Register;

/**
 * Created by Dylan on 4/4/2018.
 */
public class J extends Instruction
{

    @Override
    public void argNum(int i, Node n, int line)
    {
        if(i > 0) {
            error(line, "Too many args for " + getOp());
            return;
        }
        if(Register.class.isAssignableFrom(n.getClass())) {
            Register r = (Register) n;
            setB(r.getRegnum());
        } else if (Label.class.isAssignableFrom(n.getClass())) {
            setB(14);
        }
    }

    @Override
    protected OpCode getOp()
    {
        return OpCode.J;
    }

    @Override
    protected int getFunc()
    {
        return 2;
    }

    @Override
    protected int getDb()
    {
        return 0;
    }

    @Override
    protected int getInst()
    {
        return 0;
    }

}
