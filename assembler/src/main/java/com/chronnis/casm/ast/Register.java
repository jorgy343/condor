package com.chronnis.casm.ast;

/**
 * Created by Dylan on 4/3/2018.
 */
public class Register implements Node
{
    private int regnum;
    private boolean isRef;

    public Register(String val, boolean isRef) {
        this(val);
        this.isRef = isRef;
    }

    public Register(String val) {
        int idx = val.indexOf(']');
        val = val.substring(val.indexOf('r') + 1, (idx >= 0 ? idx : val.length()));
        this.regnum = Integer.parseInt(val);
    }

    @Override
    public String toString() {
        String s = "r" + regnum;
        if(isRef){
            s = '[' + s + ']';
        }
        return s;
    }

    public int getRegnum()
    {
        return regnum;
    }
}
