package com.chronnis.casm.ast;

/**
 * Created by Dylan on 4/3/2018.
 */
public class Label implements Node
{
    private String name;
    private int pos;
    private int line;

    public Label(String name, int pos, int line) {
        this.name = name;
        this.pos = pos;
        this.line = line;
    }

    public int getPos()
    {
        return pos;
    }

    @Override
    public String toString() {
        return this.name + ':' + this.pos;
    }
}
