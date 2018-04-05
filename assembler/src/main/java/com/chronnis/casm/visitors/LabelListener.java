package com.chronnis.casm.visitors;

import com.chronnis.casm.antlr.asm.AsmListener;
import com.chronnis.casm.antlr.asm.AsmParser;
import com.chronnis.casm.ast.Label;
import org.antlr.v4.runtime.ParserRuleContext;
import org.antlr.v4.runtime.tree.ErrorNode;
import org.antlr.v4.runtime.tree.TerminalNode;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by Dylan on 4/3/2018.
 */
public class LabelListener implements AsmListener
{
    private Map<String, Label> labelMap = new HashMap<>();
    private int pos = 0;
    private int line = 0;
    private boolean inOp = false;

    @Override
    public void enterProg(AsmParser.ProgContext ctx)
    {

    }

    @Override
    public void exitProg(AsmParser.ProgContext ctx)
    {

    }

    @Override
    public void enterStmt(AsmParser.StmtContext ctx)
    {
        line += 1;
    }

    @Override
    public void exitStmt(AsmParser.StmtContext ctx)
    {

    }

    @Override
    public void enterOp(AsmParser.OpContext ctx)
    {
        this.inOp = true;
    }

    @Override
    public void exitOp(AsmParser.OpContext ctx)
    {
        this.pos += 1;
        this.inOp = false;
    }

    @Override
    public void enterLabel(AsmParser.LabelContext ctx)
    {
        if(!inOp) {
            labelMap.put(ctx.LABEL().toString(), new Label(ctx.LABEL().toString(), this.pos, this.line));
        } else {
            this.pos += 2;
        }
    }

    @Override
    public void exitLabel(AsmParser.LabelContext ctx)
    {

    }

    @Override
    public void enterDirective(AsmParser.DirectiveContext ctx)
    {

    }

    @Override
    public void exitDirective(AsmParser.DirectiveContext ctx)
    {
        this.pos += 1;
    }

    @Override
    public void enterComment(AsmParser.CommentContext ctx)
    {

    }

    @Override
    public void exitComment(AsmParser.CommentContext ctx)
    {

    }

    @Override
    public void enterExprlist(AsmParser.ExprlistContext ctx)
    {

    }

    @Override
    public void exitExprlist(AsmParser.ExprlistContext ctx)
    {

    }

    @Override
    public void enterExpr(AsmParser.ExprContext ctx)
    {

    }

    @Override
    public void exitExpr(AsmParser.ExprContext ctx)
    {

    }

    @Override
    public void enterNumber(AsmParser.NumberContext ctx)
    {

    }

    @Override
    public void exitNumber(AsmParser.NumberContext ctx)
    {

    }

    @Override
    public void enterRegref(AsmParser.RegrefContext ctx)
    {

    }

    @Override
    public void exitRegref(AsmParser.RegrefContext ctx)
    {

    }

    @Override
    public void enterReg(AsmParser.RegContext ctx)
    {

    }

    @Override
    public void exitReg(AsmParser.RegContext ctx)
    {

    }

    @Override
    public void visitTerminal(TerminalNode node)
    {

    }

    @Override
    public void visitErrorNode(ErrorNode node)
    {

    }

    @Override
    public void enterEveryRule(ParserRuleContext ctx)
    {

    }

    @Override
    public void exitEveryRule(ParserRuleContext ctx)
    {

    }

    public Map<String, Label> getLabelMap() {
        return this.labelMap;
    }
}
