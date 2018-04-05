package com.chronnis.casm.inst;

import com.chronnis.casm.ast.OpCode;

/**
 * Created by Dylan on 4/4/2018.
 */
public class JG extends J
{

    @Override
    protected OpCode getOp()
    {
        return OpCode.JG;
    }

    @Override
    protected int getInst()
    {
        return 4;
    }

}
