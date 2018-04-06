package com.chronnis.casm.inst;

import com.chronnis.casm.ast.OpCode;

/**
 * Created by Dylan on 4/4/2018.
 */
public class JL extends J
{

    @Override
    protected OpCode getOp()
    {
        return OpCode.JL;
    }

    @Override
    protected int getInst()
    {
        return 3;
    }

}
