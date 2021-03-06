//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Casm.g4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Casm.Antlr {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="CasmParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public interface ICasmListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="CasmParser.prog"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProg([NotNull] CasmParser.ProgContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CasmParser.prog"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProg([NotNull] CasmParser.ProgContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CasmParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] CasmParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CasmParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] CasmParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CasmParser.label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLabel([NotNull] CasmParser.LabelContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CasmParser.label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLabel([NotNull] CasmParser.LabelContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CasmParser.instruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInstruction([NotNull] CasmParser.InstructionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CasmParser.instruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInstruction([NotNull] CasmParser.InstructionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CasmParser.directive"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDirective([NotNull] CasmParser.DirectiveContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CasmParser.directive"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDirective([NotNull] CasmParser.DirectiveContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CasmParser.expressionList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpressionList([NotNull] CasmParser.ExpressionListContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CasmParser.expressionList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpressionList([NotNull] CasmParser.ExpressionListContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CasmParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] CasmParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CasmParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] CasmParser.ExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CasmParser.register"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRegister([NotNull] CasmParser.RegisterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CasmParser.register"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRegister([NotNull] CasmParser.RegisterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CasmParser.registerReference"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRegisterReference([NotNull] CasmParser.RegisterReferenceContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CasmParser.registerReference"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRegisterReference([NotNull] CasmParser.RegisterReferenceContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CasmParser.comment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterComment([NotNull] CasmParser.CommentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CasmParser.comment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitComment([NotNull] CasmParser.CommentContext context);
}
} // namespace Casm.Antlr
