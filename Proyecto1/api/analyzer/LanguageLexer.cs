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
using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.2")]
[System.CLSCompliant(false)]
public partial class LanguageLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, INT=31, BOOL=32, 
		FLOAT=33, STRING=34, RUNE=35, WS=36, VAR=37, FOR=38, IF=39, ELSE=40, WHILE=41, 
		BREAK=42, CONTINUE=43, RETURN=44, NIL=45, ID=46, COMMENT=47, ML_COMMENT=48;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "T__23", "T__24", 
		"T__25", "T__26", "T__27", "T__28", "T__29", "INT", "BOOL", "FLOAT", "STRING", 
		"ESC", "RUNE", "WS", "VAR", "FOR", "IF", "ELSE", "WHILE", "BREAK", "CONTINUE", 
		"RETURN", "NIL", "ID", "COMMENT", "ML_COMMENT"
	};


	public LanguageLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public LanguageLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'='", "';'", "':='", "'int'", "'float64'", "'bool'", "'string'", 
		"'rune'", "'fmt.Println('", "')'", "'{'", "'}'", "'('", "','", "'-'", 
		"'!'", "'*'", "'/'", "'%'", "'+'", "'>'", "'<'", "'>='", "'<='", "'=='", 
		"'!='", "'&&'", "'||'", "'+='", "'-='", null, null, null, null, null, 
		null, "'var'", "'for'", "'if'", "'else'", "'while'", "'break'", "'continue'", 
		"'return'", "'nil'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, "INT", "BOOL", "FLOAT", "STRING", 
		"RUNE", "WS", "VAR", "FOR", "IF", "ELSE", "WHILE", "BREAK", "CONTINUE", 
		"RETURN", "NIL", "ID", "COMMENT", "ML_COMMENT"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Language.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static LanguageLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,48,329,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,7,33,2,34,7,34,2,35,
		7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,2,40,7,40,2,41,7,41,2,42,
		7,42,2,43,7,43,2,44,7,44,2,45,7,45,2,46,7,46,2,47,7,47,2,48,7,48,1,0,1,
		0,1,1,1,1,1,2,1,2,1,2,1,3,1,3,1,3,1,3,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,
		1,5,1,5,1,5,1,5,1,5,1,6,1,6,1,6,1,6,1,6,1,6,1,6,1,7,1,7,1,7,1,7,1,7,1,
		8,1,8,1,8,1,8,1,8,1,8,1,8,1,8,1,8,1,8,1,8,1,8,1,8,1,9,1,9,1,10,1,10,1,
		11,1,11,1,12,1,12,1,13,1,13,1,14,1,14,1,15,1,15,1,16,1,16,1,17,1,17,1,
		18,1,18,1,19,1,19,1,20,1,20,1,21,1,21,1,22,1,22,1,22,1,23,1,23,1,23,1,
		24,1,24,1,24,1,25,1,25,1,25,1,26,1,26,1,26,1,27,1,27,1,27,1,28,1,28,1,
		28,1,29,1,29,1,29,1,30,4,30,200,8,30,11,30,12,30,201,1,31,1,31,1,31,1,
		31,1,31,1,31,1,31,1,31,1,31,3,31,213,8,31,1,32,4,32,216,8,32,11,32,12,
		32,217,1,32,1,32,4,32,222,8,32,11,32,12,32,223,1,33,1,33,1,33,5,33,229,
		8,33,10,33,12,33,232,9,33,1,33,1,33,1,34,1,34,1,34,1,35,1,35,1,35,1,35,
		1,36,4,36,244,8,36,11,36,12,36,245,1,36,1,36,1,37,1,37,1,37,1,37,1,38,
		1,38,1,38,1,38,1,39,1,39,1,39,1,40,1,40,1,40,1,40,1,40,1,41,1,41,1,41,
		1,41,1,41,1,41,1,42,1,42,1,42,1,42,1,42,1,42,1,43,1,43,1,43,1,43,1,43,
		1,43,1,43,1,43,1,43,1,44,1,44,1,44,1,44,1,44,1,44,1,44,1,45,1,45,1,45,
		1,45,1,46,1,46,5,46,300,8,46,10,46,12,46,303,9,46,1,47,1,47,1,47,1,47,
		5,47,309,8,47,10,47,12,47,312,9,47,1,47,1,47,1,48,1,48,1,48,1,48,5,48,
		320,8,48,10,48,12,48,323,9,48,1,48,1,48,1,48,1,48,1,48,1,321,0,49,1,1,
		3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,21,11,23,12,25,13,27,14,29,15,
		31,16,33,17,35,18,37,19,39,20,41,21,43,22,45,23,47,24,49,25,51,26,53,27,
		55,28,57,29,59,30,61,31,63,32,65,33,67,34,69,0,71,35,73,36,75,37,77,38,
		79,39,81,40,83,41,85,42,87,43,89,44,91,45,93,46,95,47,97,48,1,0,7,1,0,
		48,57,2,0,34,34,92,92,7,0,34,34,92,92,98,98,102,102,110,110,114,114,116,
		116,3,0,9,10,13,13,32,32,3,0,65,90,95,95,97,122,4,0,48,57,65,90,95,95,
		97,122,2,0,10,10,13,13,337,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,
		0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,
		19,1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,
		0,0,0,0,31,1,0,0,0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,1,0,0,0,0,39,1,0,0,0,
		0,41,1,0,0,0,0,43,1,0,0,0,0,45,1,0,0,0,0,47,1,0,0,0,0,49,1,0,0,0,0,51,
		1,0,0,0,0,53,1,0,0,0,0,55,1,0,0,0,0,57,1,0,0,0,0,59,1,0,0,0,0,61,1,0,0,
		0,0,63,1,0,0,0,0,65,1,0,0,0,0,67,1,0,0,0,0,71,1,0,0,0,0,73,1,0,0,0,0,75,
		1,0,0,0,0,77,1,0,0,0,0,79,1,0,0,0,0,81,1,0,0,0,0,83,1,0,0,0,0,85,1,0,0,
		0,0,87,1,0,0,0,0,89,1,0,0,0,0,91,1,0,0,0,0,93,1,0,0,0,0,95,1,0,0,0,0,97,
		1,0,0,0,1,99,1,0,0,0,3,101,1,0,0,0,5,103,1,0,0,0,7,106,1,0,0,0,9,110,1,
		0,0,0,11,118,1,0,0,0,13,123,1,0,0,0,15,130,1,0,0,0,17,135,1,0,0,0,19,148,
		1,0,0,0,21,150,1,0,0,0,23,152,1,0,0,0,25,154,1,0,0,0,27,156,1,0,0,0,29,
		158,1,0,0,0,31,160,1,0,0,0,33,162,1,0,0,0,35,164,1,0,0,0,37,166,1,0,0,
		0,39,168,1,0,0,0,41,170,1,0,0,0,43,172,1,0,0,0,45,174,1,0,0,0,47,177,1,
		0,0,0,49,180,1,0,0,0,51,183,1,0,0,0,53,186,1,0,0,0,55,189,1,0,0,0,57,192,
		1,0,0,0,59,195,1,0,0,0,61,199,1,0,0,0,63,212,1,0,0,0,65,215,1,0,0,0,67,
		225,1,0,0,0,69,235,1,0,0,0,71,238,1,0,0,0,73,243,1,0,0,0,75,249,1,0,0,
		0,77,253,1,0,0,0,79,257,1,0,0,0,81,260,1,0,0,0,83,265,1,0,0,0,85,271,1,
		0,0,0,87,277,1,0,0,0,89,286,1,0,0,0,91,293,1,0,0,0,93,297,1,0,0,0,95,304,
		1,0,0,0,97,315,1,0,0,0,99,100,5,61,0,0,100,2,1,0,0,0,101,102,5,59,0,0,
		102,4,1,0,0,0,103,104,5,58,0,0,104,105,5,61,0,0,105,6,1,0,0,0,106,107,
		5,105,0,0,107,108,5,110,0,0,108,109,5,116,0,0,109,8,1,0,0,0,110,111,5,
		102,0,0,111,112,5,108,0,0,112,113,5,111,0,0,113,114,5,97,0,0,114,115,5,
		116,0,0,115,116,5,54,0,0,116,117,5,52,0,0,117,10,1,0,0,0,118,119,5,98,
		0,0,119,120,5,111,0,0,120,121,5,111,0,0,121,122,5,108,0,0,122,12,1,0,0,
		0,123,124,5,115,0,0,124,125,5,116,0,0,125,126,5,114,0,0,126,127,5,105,
		0,0,127,128,5,110,0,0,128,129,5,103,0,0,129,14,1,0,0,0,130,131,5,114,0,
		0,131,132,5,117,0,0,132,133,5,110,0,0,133,134,5,101,0,0,134,16,1,0,0,0,
		135,136,5,102,0,0,136,137,5,109,0,0,137,138,5,116,0,0,138,139,5,46,0,0,
		139,140,5,80,0,0,140,141,5,114,0,0,141,142,5,105,0,0,142,143,5,110,0,0,
		143,144,5,116,0,0,144,145,5,108,0,0,145,146,5,110,0,0,146,147,5,40,0,0,
		147,18,1,0,0,0,148,149,5,41,0,0,149,20,1,0,0,0,150,151,5,123,0,0,151,22,
		1,0,0,0,152,153,5,125,0,0,153,24,1,0,0,0,154,155,5,40,0,0,155,26,1,0,0,
		0,156,157,5,44,0,0,157,28,1,0,0,0,158,159,5,45,0,0,159,30,1,0,0,0,160,
		161,5,33,0,0,161,32,1,0,0,0,162,163,5,42,0,0,163,34,1,0,0,0,164,165,5,
		47,0,0,165,36,1,0,0,0,166,167,5,37,0,0,167,38,1,0,0,0,168,169,5,43,0,0,
		169,40,1,0,0,0,170,171,5,62,0,0,171,42,1,0,0,0,172,173,5,60,0,0,173,44,
		1,0,0,0,174,175,5,62,0,0,175,176,5,61,0,0,176,46,1,0,0,0,177,178,5,60,
		0,0,178,179,5,61,0,0,179,48,1,0,0,0,180,181,5,61,0,0,181,182,5,61,0,0,
		182,50,1,0,0,0,183,184,5,33,0,0,184,185,5,61,0,0,185,52,1,0,0,0,186,187,
		5,38,0,0,187,188,5,38,0,0,188,54,1,0,0,0,189,190,5,124,0,0,190,191,5,124,
		0,0,191,56,1,0,0,0,192,193,5,43,0,0,193,194,5,61,0,0,194,58,1,0,0,0,195,
		196,5,45,0,0,196,197,5,61,0,0,197,60,1,0,0,0,198,200,7,0,0,0,199,198,1,
		0,0,0,200,201,1,0,0,0,201,199,1,0,0,0,201,202,1,0,0,0,202,62,1,0,0,0,203,
		204,5,116,0,0,204,205,5,114,0,0,205,206,5,117,0,0,206,213,5,101,0,0,207,
		208,5,102,0,0,208,209,5,97,0,0,209,210,5,108,0,0,210,211,5,115,0,0,211,
		213,5,101,0,0,212,203,1,0,0,0,212,207,1,0,0,0,213,64,1,0,0,0,214,216,7,
		0,0,0,215,214,1,0,0,0,216,217,1,0,0,0,217,215,1,0,0,0,217,218,1,0,0,0,
		218,219,1,0,0,0,219,221,5,46,0,0,220,222,7,0,0,0,221,220,1,0,0,0,222,223,
		1,0,0,0,223,221,1,0,0,0,223,224,1,0,0,0,224,66,1,0,0,0,225,230,5,34,0,
		0,226,229,3,69,34,0,227,229,8,1,0,0,228,226,1,0,0,0,228,227,1,0,0,0,229,
		232,1,0,0,0,230,228,1,0,0,0,230,231,1,0,0,0,231,233,1,0,0,0,232,230,1,
		0,0,0,233,234,5,34,0,0,234,68,1,0,0,0,235,236,5,92,0,0,236,237,7,2,0,0,
		237,70,1,0,0,0,238,239,5,39,0,0,239,240,9,0,0,0,240,241,5,39,0,0,241,72,
		1,0,0,0,242,244,7,3,0,0,243,242,1,0,0,0,244,245,1,0,0,0,245,243,1,0,0,
		0,245,246,1,0,0,0,246,247,1,0,0,0,247,248,6,36,0,0,248,74,1,0,0,0,249,
		250,5,118,0,0,250,251,5,97,0,0,251,252,5,114,0,0,252,76,1,0,0,0,253,254,
		5,102,0,0,254,255,5,111,0,0,255,256,5,114,0,0,256,78,1,0,0,0,257,258,5,
		105,0,0,258,259,5,102,0,0,259,80,1,0,0,0,260,261,5,101,0,0,261,262,5,108,
		0,0,262,263,5,115,0,0,263,264,5,101,0,0,264,82,1,0,0,0,265,266,5,119,0,
		0,266,267,5,104,0,0,267,268,5,105,0,0,268,269,5,108,0,0,269,270,5,101,
		0,0,270,84,1,0,0,0,271,272,5,98,0,0,272,273,5,114,0,0,273,274,5,101,0,
		0,274,275,5,97,0,0,275,276,5,107,0,0,276,86,1,0,0,0,277,278,5,99,0,0,278,
		279,5,111,0,0,279,280,5,110,0,0,280,281,5,116,0,0,281,282,5,105,0,0,282,
		283,5,110,0,0,283,284,5,117,0,0,284,285,5,101,0,0,285,88,1,0,0,0,286,287,
		5,114,0,0,287,288,5,101,0,0,288,289,5,116,0,0,289,290,5,117,0,0,290,291,
		5,114,0,0,291,292,5,110,0,0,292,90,1,0,0,0,293,294,5,110,0,0,294,295,5,
		105,0,0,295,296,5,108,0,0,296,92,1,0,0,0,297,301,7,4,0,0,298,300,7,5,0,
		0,299,298,1,0,0,0,300,303,1,0,0,0,301,299,1,0,0,0,301,302,1,0,0,0,302,
		94,1,0,0,0,303,301,1,0,0,0,304,305,5,47,0,0,305,306,5,47,0,0,306,310,1,
		0,0,0,307,309,8,6,0,0,308,307,1,0,0,0,309,312,1,0,0,0,310,308,1,0,0,0,
		310,311,1,0,0,0,311,313,1,0,0,0,312,310,1,0,0,0,313,314,6,47,0,0,314,96,
		1,0,0,0,315,316,5,47,0,0,316,317,5,42,0,0,317,321,1,0,0,0,318,320,9,0,
		0,0,319,318,1,0,0,0,320,323,1,0,0,0,321,322,1,0,0,0,321,319,1,0,0,0,322,
		324,1,0,0,0,323,321,1,0,0,0,324,325,5,42,0,0,325,326,5,47,0,0,326,327,
		1,0,0,0,327,328,6,48,0,0,328,98,1,0,0,0,11,0,201,212,217,223,228,230,245,
		301,310,321,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace analyzer
