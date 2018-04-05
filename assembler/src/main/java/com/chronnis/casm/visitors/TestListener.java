package com.chronnis.casm.visitors;

import com.chronnis.casm.antlr.CasmParser;
import com.chronnis.casm.antlr.CasmBaseListener;
import com.chronnis.casm.ast.*;
import com.chronnis.casm.inst.Instruction;
import com.chronnis.casm.inst.InstructionMagic;
import com.chronnis.casm.inst.MOVH;
import com.chronnis.casm.inst.MOVL;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * Created by Dylan on 4/4/2018.
 */
public class TestListener extends CasmBaseListener
{
    private List<Instruction> instructionList = new ArrayList<>();
    private Map<String, Label> labelMap;
    private OpCode currentOp;
    private Instruction currentInstruction;
    private int argNum = 0;
    private int line = 0;
    private int scratchReg = 14;

    public TestListener(Map<String, Label> labelMap) {
        this.labelMap = labelMap;
    }

    @Override
    public void enterStmt(CasmParser.StmtContext ctx) {
        line++;
    }

    @Override
    public void enterOp(CasmParser.OpContext ctx) {
        this.currentOp = OpCode.fromString(ctx.KEYWORD().toString());
        try
        {
            this.currentInstruction = InstructionMagic.getInstruction(this.currentOp);
        } catch (ClassNotFoundException e)
        {
            System.out.println("ERROR: Instruction not implemented - " + currentOp);
        } catch (IllegalAccessException | InstantiationException e)
        {
            e.printStackTrace();
        }
    }

    @Override
    public void exitOp(CasmParser.OpContext ctx) {
        this.instructionList.add(this.currentInstruction);
        this.currentInstruction = null;
        this.currentOp = null;
        this.argNum = 0;
    }

    @Override
    public void enterExprlist(CasmParser.ExprlistContext ctx) {
        switch (this.currentOp) {
            case MOVL:
            case MOVH:
                assert ctx.expr().size() == 2;
                assert ctx.expr(0).number() != null;
                assert ctx.expr(1).reg() != null;
                break;
            case MOV:
                assert ctx.expr().size() == 2;
                assert ctx.expr(0).reg() != null;
                assert ctx.expr(1).reg() != null;
                break;
            case J:
                assert ctx.expr().size() == 1;
                assert ctx.expr(0).reg() != null | ctx.expr(0).label() != null;
                if(ctx.expr(0).label() != null) {
                    MOVL l = new MOVL();
                    l.setD(scratchReg);
                    l.setImm(mask(labelMap.get(ctx.expr(0).label().getText()).getPos(), false));
                    MOVH h = new MOVH();
                    h.setD(scratchReg);
                    h.setImm(mask(labelMap.get(ctx.expr(0).label().getText()).getPos(), true));
                    this.instructionList.add(l);
                    this.instructionList.add(h);
                }
                break;
        }

    }

    private int mask(int i, boolean high) {
        i = i & 0xFFFF;
        if(high) {
            return i & 0xFF00;
        }
        return i & 0x00FF;
    }

    @Override
    public void enterExpr(CasmParser.ExprContext ctx) {
        this.currentInstruction.argNum(argNum, exprContextToNode(ctx), line);
    }

    private Node exprContextToNode(CasmParser.ExprContext ctx) {
        if(null != ctx.label()) {
            return labelMap.get(ctx.label().getText());
        }
        if(null != ctx.reg()) {
            return new Register(ctx.reg().getText());
        }
        if(null != ctx.regref()) {
            return new Register(ctx.regref().getText());
        }
        if(null != ctx.number()) {
            return new Immediate(ctx.number().getText());
        }
        return null;
    }

    @Override
    public void exitExpr(CasmParser.ExprContext ctx) {
        this.argNum++;
    }

    public List<Instruction> getInstructionList()
    {
        return instructionList;
    }

    public void printSerializedInstructionList() {
        StringBuilder str = new StringBuilder();
        for(Instruction i: instructionList) {
            str.append(String.format("%08X", i.serialize()));
            str.append(" ");
        }
        System.out.println(str);
    }
}
