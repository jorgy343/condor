package com.chronnis.casm.visitors;

import com.chronnis.casm.antlr.AsmBaseVisitor;
import com.chronnis.casm.antlr.AsmParser;
import com.chronnis.casm.ast.*;

/**
 * Created by Dylan on 4/3/2018.
 */
public class AstVisitor extends AsmBaseVisitor<Node>
{
    private int instCount = 0;

    @Override
    public Node visitProg(AsmParser.ProgContext ctx) {
        System.out.println("visitProg");
        ProgramNode topNode = new ProgramNode();
        for(AsmParser.StmtContext c: ctx.stmt()) {
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
    public Node visitStmt(AsmParser.StmtContext ctx) {
        System.out.println("visitStmt");
        AsmParser.OpContext op = ctx.op();
        AsmParser.LabelContext label = ctx.label();
        if(null != op) {
            return visitOp(op);
        }
        else if (null != label) {
            return visitLabel(label);
        }
        return null;
    }

    @Override
    public Node visitOp(AsmParser.OpContext ctx) {
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

    private Node createTriOp(AsmParser.ExprlistContext ctx) {
        System.out.println("createTriOp");
        TriOp op = new TriOp();
        op.setA(visitExpr(ctx.expr(0))); //reg
        op.setB(visitExpr(ctx.expr(1))); //reg
        op.setD(visitExpr(ctx.expr(2))); //reg
        return op;
    }

    private Node createBinOp(AsmParser.ExprlistContext ctx) {
        System.out.println("createBinOp");
        BinOp op = new BinOp();

        op.setA(visitExpr(ctx.expr(0))); // imm or reg
        op.setD(visitExpr(ctx.expr(1))); // reg
        return op;
    }

    private Node createUnOp(AsmParser.ExprlistContext ctx) {
        System.out.println("createUnOp");
        UnOp op = new UnOp();
        op.setD(visitExpr(ctx.expr(0))); // Reg or Label
        return op;
    }

    @Override
    public Node visitExpr(AsmParser.ExprContext ctx) {
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
    public Node visitExprlist(AsmParser.ExprlistContext ctx) {
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
    public Node visitLabel(AsmParser.LabelContext ctx)
    {
        return new Label(ctx.LABEL().toString(), 0, 0);
    }

    @Override
    public Node visitNumber(AsmParser.NumberContext ctx)
    {
        return new Immediate(ctx.NUMBER().toString());
    }

    @Override
    public Node visitRegref(AsmParser.RegrefContext ctx)
    {
        return new Register(ctx.REGREF().toString(), true);
    }

    @Override
    public Node visitReg(AsmParser.RegContext ctx)
    {
        return new Register(ctx.REG().toString());
    }
}
