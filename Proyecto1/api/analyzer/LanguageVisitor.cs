//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Language.g4 by ANTLR 4.13.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace analyzer {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="LanguageParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.2")]
[System.CLSCompliant(false)]
public interface ILanguageVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] LanguageParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.declaraciones"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclaraciones([NotNull] LanguageParser.DeclaracionesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.varDeclaciones"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarDeclaciones([NotNull] LanguageParser.VarDeclacionesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.typeClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeClause([NotNull] LanguageParser.TypeClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ExprStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprStmt([NotNull] LanguageParser.ExprStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PrintStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrintStmt([NotNull] LanguageParser.PrintStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BlockStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlockStmt([NotNull] LanguageParser.BlockStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IfStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStmt([NotNull] LanguageParser.IfStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>SwitchStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSwitchStmt([NotNull] LanguageParser.SwitchStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>WhileStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStmt([NotNull] LanguageParser.WhileStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ForStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForStmt([NotNull] LanguageParser.ForStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ForConditionStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForConditionStmt([NotNull] LanguageParser.ForConditionStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ForRangeStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForRangeStmt([NotNull] LanguageParser.ForRangeStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BreakStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBreakStmt([NotNull] LanguageParser.BreakStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ContinueStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitContinueStmt([NotNull] LanguageParser.ContinueStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ReturnStmt</c>
	/// labeled alternative in <see cref="LanguageParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnStmt([NotNull] LanguageParser.ReturnStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.elseifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseifStmt([NotNull] LanguageParser.ElseifStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.elseStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseStmt([NotNull] LanguageParser.ElseStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.caseStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCaseStmt([NotNull] LanguageParser.CaseStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.defaultStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDefaultStmt([NotNull] LanguageParser.DefaultStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.forInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForInit([NotNull] LanguageParser.ForInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.printArgs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrintArgs([NotNull] LanguageParser.PrintArgsContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Callee</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCallee([NotNull] LanguageParser.CalleeContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOr([NotNull] LanguageParser.OrContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>SliceLiteral</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSliceLiteral([NotNull] LanguageParser.SliceLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>MulDiv</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMulDiv([NotNull] LanguageParser.MulDivContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Parens</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParens([NotNull] LanguageParser.ParensContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DotCallee</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDotCallee([NotNull] LanguageParser.DotCalleeContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>String</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitString([NotNull] LanguageParser.StringContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AssignSub</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignSub([NotNull] LanguageParser.AssignSubContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Int</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInt([NotNull] LanguageParser.IntContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Bool</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBool([NotNull] LanguageParser.BoolContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Increment</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIncrement([NotNull] LanguageParser.IncrementContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Equality</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEquality([NotNull] LanguageParser.EqualityContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Decrement</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecrement([NotNull] LanguageParser.DecrementContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AddSub</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddSub([NotNull] LanguageParser.AddSubContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IndexAccess</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIndexAccess([NotNull] LanguageParser.IndexAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Relational</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelational([NotNull] LanguageParser.RelationalContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AssignAdd</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignAdd([NotNull] LanguageParser.AssignAddContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ArrayLiteral</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayLiteral([NotNull] LanguageParser.ArrayLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Nil</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNil([NotNull] LanguageParser.NilContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Float</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFloat([NotNull] LanguageParser.FloatContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNot([NotNull] LanguageParser.NotContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnd([NotNull] LanguageParser.AndContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IndexAssign</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIndexAssign([NotNull] LanguageParser.IndexAssignContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Assign</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssign([NotNull] LanguageParser.AssignContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Negate</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNegate([NotNull] LanguageParser.NegateContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Id</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitId([NotNull] LanguageParser.IdContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Rune</c>
	/// labeled alternative in <see cref="LanguageParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRune([NotNull] LanguageParser.RuneContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCall([NotNull] LanguageParser.CallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.arg"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArg([NotNull] LanguageParser.ArgContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.arrayItems"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayItems([NotNull] LanguageParser.ArrayItemsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.arrayItem"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayItem([NotNull] LanguageParser.ArrayItemContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.expressionList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionList([NotNull] LanguageParser.ExpressionListContext context);
}
} // namespace analyzer
