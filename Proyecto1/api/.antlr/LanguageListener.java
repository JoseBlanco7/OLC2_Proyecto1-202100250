// Generated from c:/Users/cjbg9/Desktop/compi2/G4_Fase3_FortranPEG/OLC2_Proyecto1-202100250/Proyecto1/api/Language.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link LanguageParser}.
 */
public interface LanguageListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link LanguageParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(LanguageParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(LanguageParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#declaraciones}.
	 * @param ctx the parse tree
	 */
	void enterDeclaraciones(LanguageParser.DeclaracionesContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#declaraciones}.
	 * @param ctx the parse tree
	 */
	void exitDeclaraciones(LanguageParser.DeclaracionesContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#varDeclaciones}.
	 * @param ctx the parse tree
	 */
	void enterVarDeclaciones(LanguageParser.VarDeclacionesContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#varDeclaciones}.
	 * @param ctx the parse tree
	 */
	void exitVarDeclaciones(LanguageParser.VarDeclacionesContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#typeClause}.
	 * @param ctx the parse tree
	 */
	void enterTypeClause(LanguageParser.TypeClauseContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#typeClause}.
	 * @param ctx the parse tree
	 */
	void exitTypeClause(LanguageParser.TypeClauseContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ExprStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterExprStmt(LanguageParser.ExprStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ExprStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitExprStmt(LanguageParser.ExprStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code PrintStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterPrintStmt(LanguageParser.PrintStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code PrintStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitPrintStmt(LanguageParser.PrintStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BlockStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterBlockStmt(LanguageParser.BlockStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BlockStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitBlockStmt(LanguageParser.BlockStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IfStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterIfStmt(LanguageParser.IfStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IfStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitIfStmt(LanguageParser.IfStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code SwitchStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterSwitchStmt(LanguageParser.SwitchStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code SwitchStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitSwitchStmt(LanguageParser.SwitchStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code WhileStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterWhileStmt(LanguageParser.WhileStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code WhileStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitWhileStmt(LanguageParser.WhileStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ForStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterForStmt(LanguageParser.ForStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ForStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitForStmt(LanguageParser.ForStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ForConditionStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterForConditionStmt(LanguageParser.ForConditionStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ForConditionStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitForConditionStmt(LanguageParser.ForConditionStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ForRangeStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterForRangeStmt(LanguageParser.ForRangeStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ForRangeStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitForRangeStmt(LanguageParser.ForRangeStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BreakStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterBreakStmt(LanguageParser.BreakStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BreakStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitBreakStmt(LanguageParser.BreakStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ContinueStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterContinueStmt(LanguageParser.ContinueStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ContinueStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitContinueStmt(LanguageParser.ContinueStmtContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ReturnStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void enterReturnStmt(LanguageParser.ReturnStmtContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ReturnStmt}
	 * labeled alternative in {@link LanguageParser#stmt}.
	 * @param ctx the parse tree
	 */
	void exitReturnStmt(LanguageParser.ReturnStmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#elseifStmt}.
	 * @param ctx the parse tree
	 */
	void enterElseifStmt(LanguageParser.ElseifStmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#elseifStmt}.
	 * @param ctx the parse tree
	 */
	void exitElseifStmt(LanguageParser.ElseifStmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#elseStmt}.
	 * @param ctx the parse tree
	 */
	void enterElseStmt(LanguageParser.ElseStmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#elseStmt}.
	 * @param ctx the parse tree
	 */
	void exitElseStmt(LanguageParser.ElseStmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#caseStmt}.
	 * @param ctx the parse tree
	 */
	void enterCaseStmt(LanguageParser.CaseStmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#caseStmt}.
	 * @param ctx the parse tree
	 */
	void exitCaseStmt(LanguageParser.CaseStmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#defaultStmt}.
	 * @param ctx the parse tree
	 */
	void enterDefaultStmt(LanguageParser.DefaultStmtContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#defaultStmt}.
	 * @param ctx the parse tree
	 */
	void exitDefaultStmt(LanguageParser.DefaultStmtContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#forInit}.
	 * @param ctx the parse tree
	 */
	void enterForInit(LanguageParser.ForInitContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#forInit}.
	 * @param ctx the parse tree
	 */
	void exitForInit(LanguageParser.ForInitContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#printArgs}.
	 * @param ctx the parse tree
	 */
	void enterPrintArgs(LanguageParser.PrintArgsContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#printArgs}.
	 * @param ctx the parse tree
	 */
	void exitPrintArgs(LanguageParser.PrintArgsContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Callee}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterCallee(LanguageParser.CalleeContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Callee}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitCallee(LanguageParser.CalleeContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Or}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterOr(LanguageParser.OrContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Or}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitOr(LanguageParser.OrContext ctx);
	/**
	 * Enter a parse tree produced by the {@code SliceLiteral}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterSliceLiteral(LanguageParser.SliceLiteralContext ctx);
	/**
	 * Exit a parse tree produced by the {@code SliceLiteral}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitSliceLiteral(LanguageParser.SliceLiteralContext ctx);
	/**
	 * Enter a parse tree produced by the {@code MulDiv}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterMulDiv(LanguageParser.MulDivContext ctx);
	/**
	 * Exit a parse tree produced by the {@code MulDiv}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitMulDiv(LanguageParser.MulDivContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Parens}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterParens(LanguageParser.ParensContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Parens}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitParens(LanguageParser.ParensContext ctx);
	/**
	 * Enter a parse tree produced by the {@code DotCallee}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterDotCallee(LanguageParser.DotCalleeContext ctx);
	/**
	 * Exit a parse tree produced by the {@code DotCallee}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitDotCallee(LanguageParser.DotCalleeContext ctx);
	/**
	 * Enter a parse tree produced by the {@code String}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterString(LanguageParser.StringContext ctx);
	/**
	 * Exit a parse tree produced by the {@code String}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitString(LanguageParser.StringContext ctx);
	/**
	 * Enter a parse tree produced by the {@code AssignSub}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterAssignSub(LanguageParser.AssignSubContext ctx);
	/**
	 * Exit a parse tree produced by the {@code AssignSub}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitAssignSub(LanguageParser.AssignSubContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Int}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterInt(LanguageParser.IntContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Int}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitInt(LanguageParser.IntContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Bool}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterBool(LanguageParser.BoolContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Bool}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitBool(LanguageParser.BoolContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Increment}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterIncrement(LanguageParser.IncrementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Increment}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitIncrement(LanguageParser.IncrementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Equality}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterEquality(LanguageParser.EqualityContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Equality}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitEquality(LanguageParser.EqualityContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Decrement}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterDecrement(LanguageParser.DecrementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Decrement}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitDecrement(LanguageParser.DecrementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code AddSub}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterAddSub(LanguageParser.AddSubContext ctx);
	/**
	 * Exit a parse tree produced by the {@code AddSub}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitAddSub(LanguageParser.AddSubContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IndexAccess}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterIndexAccess(LanguageParser.IndexAccessContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IndexAccess}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitIndexAccess(LanguageParser.IndexAccessContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Relational}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterRelational(LanguageParser.RelationalContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Relational}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitRelational(LanguageParser.RelationalContext ctx);
	/**
	 * Enter a parse tree produced by the {@code AssignAdd}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterAssignAdd(LanguageParser.AssignAddContext ctx);
	/**
	 * Exit a parse tree produced by the {@code AssignAdd}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitAssignAdd(LanguageParser.AssignAddContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ArrayLiteral}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterArrayLiteral(LanguageParser.ArrayLiteralContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ArrayLiteral}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitArrayLiteral(LanguageParser.ArrayLiteralContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Nil}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterNil(LanguageParser.NilContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Nil}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitNil(LanguageParser.NilContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Float}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterFloat(LanguageParser.FloatContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Float}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitFloat(LanguageParser.FloatContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Not}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterNot(LanguageParser.NotContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Not}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitNot(LanguageParser.NotContext ctx);
	/**
	 * Enter a parse tree produced by the {@code And}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterAnd(LanguageParser.AndContext ctx);
	/**
	 * Exit a parse tree produced by the {@code And}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitAnd(LanguageParser.AndContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IndexAssign}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterIndexAssign(LanguageParser.IndexAssignContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IndexAssign}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitIndexAssign(LanguageParser.IndexAssignContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Assign}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterAssign(LanguageParser.AssignContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Assign}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitAssign(LanguageParser.AssignContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Negate}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterNegate(LanguageParser.NegateContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Negate}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitNegate(LanguageParser.NegateContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Id}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterId(LanguageParser.IdContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Id}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitId(LanguageParser.IdContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Rune}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void enterRune(LanguageParser.RuneContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Rune}
	 * labeled alternative in {@link LanguageParser#expr}.
	 * @param ctx the parse tree
	 */
	void exitRune(LanguageParser.RuneContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#call}.
	 * @param ctx the parse tree
	 */
	void enterCall(LanguageParser.CallContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#call}.
	 * @param ctx the parse tree
	 */
	void exitCall(LanguageParser.CallContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#arg}.
	 * @param ctx the parse tree
	 */
	void enterArg(LanguageParser.ArgContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#arg}.
	 * @param ctx the parse tree
	 */
	void exitArg(LanguageParser.ArgContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#arrayItems}.
	 * @param ctx the parse tree
	 */
	void enterArrayItems(LanguageParser.ArrayItemsContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#arrayItems}.
	 * @param ctx the parse tree
	 */
	void exitArrayItems(LanguageParser.ArrayItemsContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#arrayItem}.
	 * @param ctx the parse tree
	 */
	void enterArrayItem(LanguageParser.ArrayItemContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#arrayItem}.
	 * @param ctx the parse tree
	 */
	void exitArrayItem(LanguageParser.ArrayItemContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#expressionList}.
	 * @param ctx the parse tree
	 */
	void enterExpressionList(LanguageParser.ExpressionListContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#expressionList}.
	 * @param ctx the parse tree
	 */
	void exitExpressionList(LanguageParser.ExpressionListContext ctx);
}