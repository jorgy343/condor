package com.chronnis.casm.ast;

/**
 * Created by Dylan on 4/3/2018.
 */
public class TriOp extends BinOp implements Node
{
    private Node B;


    public Node getB()
    {
        return B;
    }

    public void setB(Node b)
    {
        if( null == b ) {
            System.out.println("null b");
        }
        B = b;
    }

    @Override
    public String toString() {
        return String.join(" ", getOpCode().toString(), getA().toString(), getB().toString(), getD().toString());
    }
}
