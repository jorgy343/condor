package com.chronnis.casm.visitors;

import com.chronnis.casm.antlr.CasmParser;
import com.chronnis.casm.antlr.CasmListener;
import com.chronnis.casm.ast.Label;
import org.antlr.v4.runtime.ParserRuleContext;
import org.antlr.v4.runtime.tree.ErrorNode;
import org.antlr.v4.runtime.tree.TerminalNode;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by Dylan on 4/3/2018.
 */
public class LabelListener implements CasmListener
{
    private Map<String, Label> labelMap = new HashMap<>();
    private int pos = 0;
    private int line = 0;
    private boolean inOp = false;

    @Override
    public void enterProg(CasmParser.ProgContext ctx)
    {

    }

    @Override
    public void exitProg(CasmParser.ProgContext ctx)
    {

    }

    @Override
    public void enterStmt(CasmParser.StmtContext ctx)
    {
        line += 1;
    }

    @Override
    public void exitStmt(CasmParser.StmtContext ctx)
    {

    }

    @Override
    public void enterOp(CasmParser.OpContext ctx)
    {
        this.inOp = true;
    }

    @Override
    public void exitOp(CasmParser.OpContext ctx)
    {
        this.pos += 1;
        this.inOp = false;
    }

    @Override
    public void enterLabel(CasmParser.LabelContext ctx)
    {
        if(!inOp) {
            labelMap.put(ctx.LABEL().toString(), new Label(ctx.LABEL().toString(), this.pos, this.line));
        } else {
            this.pos += 2;
        }
    }

    @Override
    public void exitLabel(CasmParser.LabelContext ctx)
    {

    }

    @Override
    public void enterDirective(CasmParser.DirectiveContext ctx)
    {

    }

    @Override
    public void exitDirective(CasmParser.DirectiveContext ctx)
    {
        this.pos += 1;
    }

    @Override
    public void enterComment(CasmParser.CommentContext ctx)
    {

    }

    @Override
    public void exitComment(CasmParser.CommentContext ctx)
    {

    }

    @Override
    public void enterExprlist(CasmParser.ExprlistContext ctx)
    {

    }

    @Override
    public void exitExprlist(CasmParser.ExprlistContext ctx)
    {

    }

    @Override
    public void enterExpr(CasmParser.ExprContext ctx)
    {

    }

    @Override
    public void exitExpr(CasmParser.ExprContext ctx)
    {

    }

    @Override
    public void enterNumber(CasmParser.NumberContext ctx)
    {

    }

    @Override
    public void exitNumber(CasmParser.NumberContext ctx)
    {

    }

    @Override
    public void enterRegref(CasmParser.RegrefContext ctx)
    {

    }

    @Override
    public void exitRegref(CasmParser.RegrefContext ctx)
    {

    }

    @Override
    public void enterReg(CasmParser.RegContext ctx)
    {

    }

    @Override
    public void exitReg(CasmParser.RegContext ctx)
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
