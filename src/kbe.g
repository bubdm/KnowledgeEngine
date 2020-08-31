header {
using System.Collections.Generic;

using KnowledgeEngineRules.Core;
}

options {
	language = "CSharp";
	namespace = "KnowledgeEngineRules.Assembler";
}


/********************************************************************
	KBE PARSER

*/
class RulesEngineParser extends Parser;
options
{
	buildAST = true;   // uses CommonAST by default
	ASTLabelType = "antlr.CommonAST"; // change default of "AST"
	classHeaderPrefix = "";
}
{
	private Engine _engine = null;
	public void SetEngine(Engine engine)
	{
		_engine = engine;
	}


	private RuleBase _rules = new RuleBase();
	public RuleBase GetRuleBase()
	{
		return _rules;
	}

	private KnowledgeBase _kb = new KnowledgeBase();
	public KnowledgeBase GetKnowledgeBase()
	{
		return _kb;
	}
}

document
	:	( fact )* ( rule )* EOF!
	;

//	fact Owns (Nono M1-Missile 123);
//	fact Owns (?x ?y ?z);
fact
{
	Relationship r = null;
}
	:	FACT! r=factliteral { _kb.Add(r); } SEMI!
	;

factliteral returns [Relationship r = null]
{
	string id = null;
	object o = null;
}
	:	id=identifier					{ r = new Relationship(id, _kb); }
		LPAREN!
		(	id=identifier				{ r.Add(new SetItem(id, _kb)); }
		|	o=atom						{ r.Add(new SetItem(o, _kb)); }
		)+
		RPAREN!
	;



//rule	name
//		match (ship is running)
//		then
//		assert (ship is ready-for-action)
//		;
rule
{
	Rule r = new Rule(_kb);
	string id = null;
	ICondition c = null;
	IAction a = null;
}
	:	RULE!
		id=identifier 					{ r.ID = id; }
										{ if (_rules.ContainsKey(r.ID)) { reportError("Rule '" + r.ID + "' already exists"); } else { _rules.Add(r); } }
		( c=condition[r]				{ r.AddCondition(c); } )+
		THEN!
		( a=action[r]					{ r.AddAction(a); } )+
		SEMI!
	;


condition [Rule rule] returns [ICondition c = null]
{
	Variable v = null;
	Relationship r = null;
	Expression expr = null;
}
	:	MATCH! r=factliteral				{ c = new MatchCondition(rule, r); }
	|	NOMATCH! r=factliteral				{ c = new NoMatchCondition(rule, r); }
	|	TEST! expr=expression[rule]			{ c = new TestCondition(rule, expr);}
	|	BINDEXISTS! v=variable_identifier	{ c = new BindExistsCondition(rule, v);}
	;


action [Rule rule] returns [IAction a = null]
{
	string id = null;
	Variable v = null;
	Relationship r = null;
	Expression expr = null;
	ExpressionNode exprnode = null;
	ExpressionNode rightnode = null;
	ExpressionPath exprpath = null;
}
	:	ASSERT! r=factliteral			{ a = new AssertAction(rule, r); }
	|	RETRACT! r=factliteral			{ a = new RetractAction(rule, r); }
	|	BIND!
		v=variable_identifier
		LPAREN!
		expr=expression[rule]			{ a = new BindAction(rule, v, expr); }
		RPAREN!
	|	UNBIND!
		v=variable_identifier			{ a = new UnBindAction(rule, v); }
	|	EXEC!
		LPAREN!
		expr=expression[rule]			{ a = new ExecAction(rule, expr); }
		RPAREN!
	|	EXECASSIGN!
		exprpath=refassign
		LPAREN!
		rightnode=logexpr
		RPAREN!							{ a = new ExecAssignAction(rule, exprpath, rightnode, _engine); }
	|	FIRERULE! id=identifier			{ a = new FireRuleAction(rule, _rules, id); }
	|	STOP!							{ a = new StopAction(rule); }
	;

refassign returns [ExpressionPath ep = null]
{
	ExpressionPath ep1 = null;
	string id = null;
}
	:	DOLLAR!
		id=identifier					{ ep = new ExpressionPath(id); }
		(
			ep1=refassignnode			{ ep.Paths.AddLast(ep1); }
		)+
	;

refassignnode returns [ExpressionPath ep = null]
{
	ExpressionNode en = null;
	string id = null;
}
	:	DOT!
		id=identifier					{ ep = new ExpressionPath(id); }
	;


expression [Rule rule] returns [Expression e = new Expression(rule)]
{
	ExpressionNode expr = null;
}
	:	expr=logexpr					{ e.Compile(expr, _engine); }
	;

logexpr returns [ExpressionNode en = new ExpressionNode()]
{
	ExpressionNode left = null;
	ExpressionNode right = null;
}
	:	left=strexpr					{ en.Left = left; }
		(	(	LOG_AND					{ en.Opr = ExpressionOperators.And; }
			|	LOG_OR					{ en.Opr = ExpressionOperators.Or; }
			|	LOG_XOR					{ en.Opr = ExpressionOperators.Xor; }
			|	LOG_EQ					{ en.Opr = ExpressionOperators.Eq; }
			|	LOG_NE					{ en.Opr = ExpressionOperators.Ne; }
			|	LOG_GE					{ en.Opr = ExpressionOperators.Ge; }
			|	LOG_GT					{ en.Opr = ExpressionOperators.Gt; }
			|	LOG_LE					{ en.Opr = ExpressionOperators.Le; }
			|	LOG_LT					{ en.Opr = ExpressionOperators.Lt; }
			)
			right=strexpr				{ en.Right.Add(right); }
		)*
	;

strexpr returns [ExpressionNode en = new ExpressionNode()]
{
	ExpressionNode left = null;
	ExpressionNode right = null;
}
	:	left=addexpr					{ en.Left = left; }
		(	AMPER						{ en.Opr = ExpressionOperators.Str; }
			right=addexpr				{ en.Right.Add(right); }
		)*
	;

addexpr returns [ExpressionNode en = new ExpressionNode()]
{
	ExpressionNode left = null;
	ExpressionNode right = null;
}
	:	left=multexpr					{ en.Left = left; }
		(	(	PLUS					{ en.Opr = ExpressionOperators.Add; }
			|	MINUS					{ en.Opr = ExpressionOperators.Sub; }
			)
			right=multexpr				{ en.Right.Add(right); }
		)*
	;

multexpr returns [ExpressionNode en = new ExpressionNode()]
{
	ExpressionNode left = null;
	ExpressionNode right = null;
}
	:	left=unaryexpr					{ en.Left = left; }
		(	(	STAR					{ en.Opr = ExpressionOperators.Mul; }
			|	DIV						{ en.Opr = ExpressionOperators.Div; }
			)
			right=unaryexpr				{ en.Right.Add(right); }
		)*
	;

unaryexpr returns [ExpressionNode en = new ExpressionNode()]
{
	ExpressionNode e = null;
}
	:	(	(	LOG_NOT!					{ en.Opr = ExpressionOperators.Not; }
			|	MINUS!						{ en.Opr = ExpressionOperators.Neg; }
			)?
			e=exprvalue						{ en.Right.Add(e); }
		)
		|	ISNULL LPAREN! e=logexpr RPAREN! { en.Opr = ExpressionOperators.IsNull; en.Right.Add(e); }
	;

exprvalue returns [ExpressionNode en = new ExpressionNode()]
{
	ExpressionNode left = null;
	object c = null;
}
	:	LPAREN! left=logexpr RPAREN!	{ en.Left = left; }
	|	c=refexpr						{ en.Value = c; }
	|	c=atom							{ en.Value = c; }
	;

refexpr returns [ExpressionPath ep = null]
{
	ExpressionPath ep1 = null;
	string id = null;
}
	:	DOLLAR!
		id=identifier					{ ep = new ExpressionPath(id); }
		(
			ep1=refexprnode				{ ep.Paths.AddLast(ep1); }
		)+
	;

refexprnode returns [ExpressionPath ep = null]
{
	ExpressionNode en = null;
	string id = null;
}
	:	DOT!
		id=identifier					{ ep = new ExpressionPath(id); }
		(
			LPAREN!						{ ep.IsMethod = true; }
			(
				en=logexpr				{ ep.Args.Add(en); }
				(
					COMMA!
					en=logexpr			{ ep.Args.Add(en); }
				)*
			)?
			RPAREN!
		)?
	;


identifier returns [string s = null]
	:	i:IDENT							{ s = i.getText(); }
	;


variable_identifier returns [Variable v = null]
	:	v1:VARIABLE_IDENT				{ v = new Variable(v1.getText()); }
	;


atom returns [object o = null]
	:	n1:INT_LIT						{ o = Convert.ToInt32(n1.getText()); }
	|	n2:FLOAT_LIT					{ o = Convert.ToSingle(n2.getText()); }
	|	s1:STRING_LIT					{ o = s1.getText().Substring(1, s1.getText().Length - 2); }
	|	v:VARIABLE_IDENT				{ o = new Variable(v.getText()); }
	|	TRUE							{ o = true; }
	|	FALSE							{ o = false; }
	;






/********************************************************************
	KBE LEXER

*/
class RulesEngineLexer extends Lexer;
options {
	classHeaderPrefix = "";
	k=2;
}
tokens {
	FACT = "fact"; RULE = "rule"; THEN = "then";
	ASSERT = "assert"; RETRACT = "retract";
	MATCH = "match"; NOMATCH = "nomatch"; TEST = "test";
	BIND = "bind"; UNBIND = "unbind"; BINDEXISTS = "bindexists";
	EXEC = "exec"; EXECASSIGN = "execassign";
	FIRERULE = "firerule"; STOP = "stop";
	TRUE = "true"; FALSE = "false";	MOD = "mod"; ISNULL = "isnull";
	LOG_AND	= "and"; LOG_NOT = "not"; LOG_OR = "or"; LOG_XOR = "xor";
}

// OPERATORS
EQUALS			:	'='		;
AT				:	'@'		;
AMPER			:	'&'		;
DOLLAR			:	'$'		;
QUESTION		:	'?'		;
LPAREN			:	'('		;
RPAREN			:	')'		;
LBRACK			:	'['		;
RBRACK			:	']'		;
LCURLY			:	'{'		;
RCURLY			:	'}'		;
COLON			:	':'		;
COMMA			:	','		;
DOT				:	'.'		;
DIV				:	'/'		;
PLUS			:	'+'		;
MINUS			:	'-'		;
STAR			:	'*'		;
LOG_EQ			:	"=="	;
LOG_NE			:	"!="	;
LOG_GE			:	">="	;
LOG_GT			:	">"		;
LOG_LE			:	"<="	;
LOG_LT			:	"<"		;
SEMI			:	';'		;


// Whitespace -- ignored
WS	:	(	' '
		|	'\t'
		|	'\f'
			// handle newlines
		|	(	options {generateAmbigWarnings=false;}
			:	"\r\n"	// Evil DOS
			|	'\r'	// Macintosh
			|	'\n'	// Unix (the right way)
			)
			{ newline(); }
		)+
		{ _ttype = Token.SKIP; }
	;

// Single-line comments
SL_COMMENT
	:	"//"
		(~('\n'|'\r'))* ('\n'|'\r'('\n')?)
		{$setType(Token.SKIP); newline();}
	;

// multiple-line comments
ML_COMMENT
	:	"/*"
		(	/*	'\r' '\n' can be matched in one alternative or by matching
				'\r' in one iteration and '\n' in another. I am trying to
				handle any flavor of newline that comes in, but the language
				that allows both "\r\n" and "\r" and "\n" to all be valid
				newline is ambiguous. Consequently, the resulting grammar
				must be ambiguous. I'm shutting this warning off.
			 */
			options {
				generateAmbigWarnings=false;
			}
		:
			{ LA(2)!='/' }? '*'
		|	'\r' '\n'		{newline();}
		|	'\r'			{newline();}
		|	'\n'			{newline();}
		|	~('*'|'\n'|'\r')
		)*
		"*/"
		{$setType(Token.SKIP);}
	;

// escape sequence -- note that this is protected; it can only be called
// from another lexer rule -- it will not ever directly return a token to
// the parser
// There are various ambiguities hushed in this rule. The optional
// '0'...'9' digit matches should be matched here rather than letting
// them go back to STRING_LIT to be matched. ANTLR does the
// right thing by matching immediately; hence, it's ok to shut off
// the FOLLOW ambig warnings.

// hexadecimal digit (again, note it's protected!)
protected
HEX_DIGIT
	:	('0'..'9'|'A'..'F'|'a'..'f')
	;

protected
ESC
	:	'\\'
		(	'n'
		|	'r'
		|	't'
		|	'b'
		|	'f'
		|	'"'
		|	'\''
		|	'\\'
		|	('u')+ HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT
		|	'0'..'3'
			(
				options {
					warnWhenFollowAmbig = false;
				}
			:	'0'..'7'
				(
					options {
						warnWhenFollowAmbig = false;
					}
				:	'0'..'7'
				)?
			)?
		|	'4'..'7'
			(
				options {
					warnWhenFollowAmbig = false;
				}
			:	'0'..'7'
			)?
		)
	;

// an identifier. Note that testLiterals is set to true! This means
// that after we match the rule, we look in the literals table to see
// if it's a literal or really an identifer
IDENT options { paraphrase = "an indentifier"; testLiterals = true; }
	:	('a'..'z' | 'A'..'Z') ('a'..'z' | 'A'..'Z' | '_' | '-' | '0'..'9')*
	;

VARIABLE_IDENT options { paraphrase = "a variable_identifier"; }
	: '?' ('a'..'z' | 'A'..'Z') ('a'..'z' | 'A'..'Z' | '_' | '0'..'9')*
	;

INT_LIT options { paraphrase = "a number"; }
	:	('0'..'9')+ ( '.' ('0'..'9')+ { $setType(FLOAT_LIT); } )?
	;

STRING_LIT options { paraphrase = "a string"; }
	:	'"' ( ESC | ~( '"' | '\\' | '\n' | '\r' ))* '"'
	;