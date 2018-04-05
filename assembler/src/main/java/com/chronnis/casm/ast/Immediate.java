package com.chronnis.casm.ast;

/**
 * Created by Dylan on 4/3/2018.
 */
public class Immediate implements Node
{
    private int val;

    public Immediate(String val) {
        parseVal(val);
    }

    public int getVal()
    {
        return val;
    }

    public void setVal(int val)
    {
        this.val = val;
    }

    private void parseVal(String val)
    {
        String prefix = "";

        if (val.length() > 2){
            prefix = val.substring(0, 2);
        }

        if("0b".equals(prefix)) {
            this.val = Integer.parseInt(val.substring(2), 2);
        } else if("0x".equals(prefix)){
            this.val = Integer.parseInt(val.substring(2), 16);
        } else {
            this.val = Integer.parseInt(val);
        }
    }

    @Override
    public String toString() {
        return String.format("0x%04X", val);
    }

}
