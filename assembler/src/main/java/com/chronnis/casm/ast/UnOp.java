package com.chronnis.casm.ast;

/**
 * Created by Dylan on 4/3/2018.
 */
public class UnOp extends Op
{
    private Node D;

    public Node getD()
    {
        return D;
    }

    public void setD(Node d)
    {
        if( null == d ) {
            System.out.println("null d");
        }
        D = d;
    }

    @Override
    public String toString() {
        return String.join(" ", getOpCode().toString(), getD().toString());
    }
}
