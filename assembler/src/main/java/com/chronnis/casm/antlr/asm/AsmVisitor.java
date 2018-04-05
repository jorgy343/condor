// Generated from Asm.g by ANTLR 4.7.1

    package com.chronnis.casm.antlr.asm;

import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link AsmParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface AsmVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link AsmParser#prog}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitProg(AsmParser.ProgContext ctx);
	/**
	 * Visit a parse tree produced by {@link AsmParser#stmt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStmt(AsmParser.StmtContext ctx);
	/**
	 * Visit a parse tree produced by {@link AsmParser#op}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitOp(AsmParser.OpContext ctx);
	/**
	 * Visit a parse tree produced by {@link AsmParser#label}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLabel(AsmParser.LabelContext ctx);
	/**
	 * Visit a parse tree produced by {@link AsmParser#directive}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDirective(AsmParser.DirectiveContext ctx);
	/**
	 * Visit a parse tree produced by {@link AsmParser#comment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitComment(AsmParser.CommentContext ctx);
	/**
	 * Visit a parse tree produced by {@link AsmParser#exprlist}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExprlist(AsmParser.ExprlistContext ctx);
	/**
	 * Visit a parse tree produced by {@link AsmParser#expr}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExpr(AsmParser.ExprContext ctx);
	/**
	 * Visit a parse tree produced by {@link AsmParser#number}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNumber(AsmParser.NumberContext ctx);
	/**
	 * Visit a parse tree produced by {@link AsmParser#regref}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitRegref(AsmParser.RegrefContext ctx);
	/**
	 * Visit a parse tree produced by {@link AsmParser#reg}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitReg(AsmParser.RegContext ctx);
}