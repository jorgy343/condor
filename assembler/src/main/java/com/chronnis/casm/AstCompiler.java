package com.chronnis.casm;

import com.chronnis.casm.ast.Label;
import com.chronnis.casm.ast.Node;
import com.chronnis.casm.ast.Op;
import com.chronnis.casm.ast.ProgramNode;

import java.util.Map;

/**
 * Created by Dylan on 4/4/2018.
 */
public class AstCompiler
{

    private Map<String, Label> labelMap;

    public AstCompiler(Map<String, Label> labelMap) {
        this.labelMap = labelMap;
    }

    public void compile(Node p)
    {
        if(!(p instanceof ProgramNode)) {
            System.out.println("weird ass compilation error");
            return;
        }

        ProgramNode root = (ProgramNode) p;
        for(Op op: root.getOps()) {
            compile(op);
        }

    }

    private void compile(Op op) {

    }
}
