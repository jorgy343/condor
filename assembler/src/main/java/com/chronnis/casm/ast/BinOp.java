package com.chronnis.casm.ast;

/**
 * Created by Dylan on 4/3/2018.
 */
public class BinOp extends UnOp
{
    private Node A;

    public Node getA()
    {
        return A;
    }

    public void setA(Node a)
    {
        if( null == a ) {
            System.out.println("null a");
        }
        A = a;
    }

    @Override
    public String toString() {
        return String.join(" ", getOpCode().toString(), getA().toString(), getD().toString());
    }

}
