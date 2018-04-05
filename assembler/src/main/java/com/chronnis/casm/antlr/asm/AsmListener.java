// Generated from Asm.g by ANTLR 4.7.1

    package com.chronnis.casm.antlr.asm;

import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link AsmParser}.
 */
public interface AsmListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link AsmParser#prog}.
	 * @param ctx the parse tree
	 */
	void enterProg(AsmParser.ProgContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#prog}.
	 * @param ctx the parse tree
	 */
	void exitProg(AsmParser.ProgContext ctx);
	/**
	 * Enter a parse tree produced by {@link AsmParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterStmt(AsmParser.StmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitStmt(AsmParser.StmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link AsmParser#op}.
	 * @param ctx the parse tree
	 */
	void enterOp(AsmParser.OpContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#op}.
	 * @param ctx the parse tree
	 */
	void exitOp(AsmParser.OpContext ctx);
	/**
	 * Enter a parse tree produced by {@link AsmParser#label}.
	 * @param ctx the parse tree
	 */
	void enterLabel(AsmParser.LabelContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#label}.
	 * @param ctx the parse tree
	 */
	void exitLabel(AsmParser.LabelContext ctx);
	/**
	 * Enter a parse tree produced by {@link AsmParser#directive}.
	 * @param ctx the parse tree
	 */
	void enterDirective(AsmParser.DirectiveContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#directive}.
	 * @param ctx the parse tree
	 */
	void exitDirective(AsmParser.DirectiveContext ctx);
	/**
	 * Enter a parse tree produced by {@link AsmParser#comment}.
	 * @param ctx the parse tree
	 */
	void enterComment(AsmParser.CommentContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#comment}.
	 * @param ctx the parse tree
	 */
	void exitComment(AsmParser.CommentContext ctx);
	/**
	 * Enter a parse tree produced by {@link AsmParser#exprlist}.
	 * @param ctx the parse tree
	 */
	void enterExprlist(AsmParser.ExprlistContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#exprlist}.
	 * @param ctx the parse tree
	 */
	void exitExprlist(AsmParser.ExprlistContext ctx);
	/**
	 * Enter a parse tree produced by {@link AsmParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterExpr(AsmParser.ExprContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitExpr(AsmParser.ExprContext ctx);
	/**
	 * Enter a parse tree produced by {@link AsmParser#number}.
	 * @param ctx the parse tree
	 */
	void enterNumber(AsmParser.NumberContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#number}.
	 * @param ctx the parse tree
	 */
	void exitNumber(AsmParser.NumberContext ctx);
	/**
	 * Enter a parse tree produced by {@link AsmParser#regref}.
	 * @param ctx the parse tree
	 */
	void enterRegref(AsmParser.RegrefContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#regref}.
	 * @param ctx the parse tree
	 */
	void exitRegref(AsmParser.RegrefContext ctx);
	/**
	 * Enter a parse tree produced by {@link AsmParser#reg}.
	 * @param ctx the parse tree
	 */
	void enterReg(AsmParser.RegContext ctx);
	/**
	 * Exit a parse tree produced by {@link AsmParser#reg}.
	 * @param ctx the parse tree
	 */
	void exitReg(AsmParser.RegContext ctx);
}