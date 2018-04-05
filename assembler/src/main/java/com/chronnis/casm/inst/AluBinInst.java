package com.chronnis.casm.inst;

import com.chronnis.casm.ast.Node;
import com.chronnis.casm.ast.Register;

/**
 * Created by Dylan on 4/4/2018.
 */
public abstract class AluBinInst extends Instruction
{
    @Override
    public void argNum(int i, Node n, int line)
    {
        if(!isReg(n)) {
            error(line, i, "Register", n);
            return;
        }
        Register r = (Register) n;
        switch (i) {
            case 0:
                setA(r.getRegnum());
                break;
            case 1:
                setB(r.getRegnum());
                break;
            case 2:
                setD(r.getRegnum());
                break;
        }
    }

    private boolean isReg(Node n) {
        return Register.class.isAssignableFrom(n.getClass());
    }

    @Override
    protected int getFunc()
    {
        return 1;
    }

    @Override
    protected int getDb()
    {
        return 3;
    }

}
