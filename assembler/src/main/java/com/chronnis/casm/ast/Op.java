package com.chronnis.casm.ast;

/**
 * Created by Dylan on 4/3/2018.
 */
public class Op implements Node
{
    private OpCode opCode;

    public OpCode getOpCode()
    {
        return opCode;
    }

    public void setOpCode(OpCode opCode)
    {
        this.opCode = opCode;
    }

    @Override
    public String toString() {
        return getOpCode().toString();
    }
}
