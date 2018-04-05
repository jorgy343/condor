package com.chronnis.casm.ast;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Dylan on 4/3/2018.
 */
public class ProgramNode implements Node
{
    private List<Op> opList = new ArrayList<>();

    public void addStatement(Node stmt) {
        this.opList.add((Op)stmt);
    }

    public List<Op> getOps() {
        return opList;
    }

    @Override
    public String toString() {
        StringBuilder str = new StringBuilder();
        for(Op n: opList) {
            str.append(n.toString());
            str.append('\n');
        }
        return str.toString();
    }
}
