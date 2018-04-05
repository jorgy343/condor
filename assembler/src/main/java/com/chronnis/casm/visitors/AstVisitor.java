package com.chronnis.casm.visitors;

import com.chronnis.casm.antlr.CasmBaseVisitor;
import com.chronnis.casm.antlr.CasmParser;
import com.chronnis.casm.ast.*;

/**
 * Created by Dylan on 4/3/2018.
 */
public class AstVisitor extends CasmBaseVisitor<Node>
{
    private int instCount = 0;

    @Override
    public Node visitProg(CasmParser.ProgContext ctx) {
        System.out.println("visitProg");
        ProgramNode topNode = new ProgramNode();
        for(CasmParser.StmtContext c: ctx.stmt()) {
            Node stmt = visitStmt(c);
            if(null != stmt)
            {
                topNode.addStatement(stmt);
                instCount++;
            }
        }
        return topNode;
    }

    @Override
    public Node visitStmt(CasmParser.StmtContext ctx) {
        System.out.println("visitStmt");
        CasmParser.OpContext op = ctx.op();
        CasmParser.LabelContext label = ctx.label();
        if(null != op) {
            return visitOp(op);
        }
        else if (null != label) {
            return visitLabel(label);
        }
        return null;
    }

    @Override
    public Node visitOp(CasmParser.OpContext ctx) {
        System.out.println("visitOp");
        Node op;
        if(null != ctx.exprlist()) {
            op = visitExprlist(ctx.exprlist());
        } else {
            op = new Op();
        }
        ((Op)op).setOpCode(OpCode.fromString(ctx.KEYWORD().toString()));
        return op;
    }

    private Node createTriOp(CasmParser.ExprlistContext ctx) {
        System.out.println("createTriOp");
        TriOp op = new TriOp();
        op.setA(visitExpr(ctx.expr(0))); //reg
        op.setB(visitExpr(ctx.expr(1))); //reg
        op.setD(visitExpr(ctx.expr(2))); //reg
        return op;
    }

    private Node createBinOp(CasmParser.ExprlistContext ctx) {
        System.out.println("createBinOp");
        BinOp op = new BinOp();

        op.setA(visitExpr(ctx.expr(0))); // imm or reg
        op.setD(visitExpr(ctx.expr(1))); // reg
        return op;
    }

    private Node createUnOp(CasmParser.ExprlistContext ctx) {
        System.out.println("createUnOp");
        UnOp op = new UnOp();
        op.setD(visitExpr(ctx.expr(0))); // Reg or Label
        return op;
    }

    @Override
    public Node visitExpr(CasmParser.ExprContext ctx) {
        System.out.println("visitExpr");
        if(null != ctx.label()) {
            return visitLabel(ctx.label());
        }
        if(null != ctx.reg()) {
            return visitReg(ctx.reg());
        }
        if(null != ctx.regref()) {
            return visitRegref(ctx.regref());
        }
        if(null != ctx.number()) {
            return visitNumber(ctx.number());
        }
        System.out.println("ERROR BINOP");
        return null;
    }

    @Override
    public Node visitExprlist(CasmParser.ExprlistContext ctx) {
        System.out.println("visitExprlist");
        int numOps = ctx.getChildCount();
        if(numOps == 3) {
            return createTriOp(ctx);
        }
        else if (numOps == 2) {
            return createBinOp(ctx);
        }
        else if (numOps == 1) {
            return createUnOp(ctx);
        }
        return null;
    }

    @Override
    public Node visitLabel(CasmParser.LabelContext ctx)
    {
        return new Label(ctx.LABEL().toString(), 0, 0);
    }

    @Override
    public Node visitNumber(CasmParser.NumberContext ctx)
    {
        return new Immediate(ctx.NUMBER().toString());
    }

    @Override
    public Node visitRegref(CasmParser.RegrefContext ctx)
    {
        return new Register(ctx.REGREF().toString(), true);
    }

    @Override
    public Node visitReg(CasmParser.RegContext ctx)
    {
        return new Register(ctx.REG().toString());
    }
}
