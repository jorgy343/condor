package com.chronnis.casm.inst;

import com.chronnis.casm.ast.OpCode;

/**
 * Created by Dylan on 4/4/2018.
 */
public class XOR extends AluBinInst
{
    @Override
    protected OpCode getOp()
    {
        return OpCode.XOR;
    }

    @Override
    protected int getInst()
    {
        return 8;
    }
}
