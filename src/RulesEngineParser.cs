// $ANTLR 2.7.7 (20060930): "kbe.g" -> "RulesEngineParser.cs"$

using System.Collections.Generic;

using KnowledgeEngineRules.Core;

namespace KnowledgeEngineRules.Assembler
{
	// Generate the header common to all output files.
	using System;
	
	using TokenBuffer              = antlr.TokenBuffer;
	using TokenStreamException     = antlr.TokenStreamException;
	using TokenStreamIOException   = antlr.TokenStreamIOException;
	using ANTLRException           = antlr.ANTLRException;
	using LLkParser = antlr.LLkParser;
	using Token                    = antlr.Token;
	using IToken                   = antlr.IToken;
	using TokenStream              = antlr.TokenStream;
	using RecognitionException     = antlr.RecognitionException;
	using NoViableAltException     = antlr.NoViableAltException;
	using MismatchedTokenException = antlr.MismatchedTokenException;
	using SemanticException        = antlr.SemanticException;
	using ParserSharedInputState   = antlr.ParserSharedInputState;
	using BitSet                   = antlr.collections.impl.BitSet;
	using AST                      = antlr.collections.AST;
	using ASTPair                  = antlr.ASTPair;
	using ASTFactory               = antlr.ASTFactory;
	using ASTArray                 = antlr.collections.impl.ASTArray;
	
/********************************************************************
	KBE PARSER

*/
	 	class RulesEngineParser : antlr.LLkParser
	{
		public const int EOF = 1;
		public const int NULL_TREE_LOOKAHEAD = 3;
		public const int FACT = 4;
		public const int SEMI = 5;
		public const int LPAREN = 6;
		public const int RPAREN = 7;
		public const int RULE = 8;
		public const int THEN = 9;
		public const int MATCH = 10;
		public const int NOMATCH = 11;
		public const int TEST = 12;
		public const int BINDEXISTS = 13;
		public const int ASSERT = 14;
		public const int RETRACT = 15;
		public const int BIND = 16;
		public const int UNBIND = 17;
		public const int EXEC = 18;
		public const int EXECASSIGN = 19;
		public const int FIRERULE = 20;
		public const int STOP = 21;
		public const int DOLLAR = 22;
		public const int DOT = 23;
		public const int LOG_AND = 24;
		public const int LOG_OR = 25;
		public const int LOG_XOR = 26;
		public const int LOG_EQ = 27;
		public const int LOG_NE = 28;
		public const int LOG_GE = 29;
		public const int LOG_GT = 30;
		public const int LOG_LE = 31;
		public const int LOG_LT = 32;
		public const int AMPER = 33;
		public const int PLUS = 34;
		public const int MINUS = 35;
		public const int STAR = 36;
		public const int DIV = 37;
		public const int LOG_NOT = 38;
		public const int ISNULL = 39;
		public const int COMMA = 40;
		public const int IDENT = 41;
		public const int VARIABLE_IDENT = 42;
		public const int INT_LIT = 43;
		public const int FLOAT_LIT = 44;
		public const int STRING_LIT = 45;
		public const int TRUE = 46;
		public const int FALSE = 47;
		public const int MOD = 48;
		public const int EQUALS = 49;
		public const int AT = 50;
		public const int QUESTION = 51;
		public const int LBRACK = 52;
		public const int RBRACK = 53;
		public const int LCURLY = 54;
		public const int RCURLY = 55;
		public const int COLON = 56;
		public const int WS = 57;
		public const int SL_COMMENT = 58;
		public const int ML_COMMENT = 59;
		public const int HEX_DIGIT = 60;
		public const int ESC = 61;
		
		
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
		
		protected void initialize()
		{
			tokenNames = tokenNames_;
			initializeFactory();
		}
		
		
		protected RulesEngineParser(TokenBuffer tokenBuf, int k) : base(tokenBuf, k)
		{
			initialize();
		}
		
		public RulesEngineParser(TokenBuffer tokenBuf) : this(tokenBuf,1)
		{
		}
		
		protected RulesEngineParser(TokenStream lexer, int k) : base(lexer,k)
		{
			initialize();
		}
		
		public RulesEngineParser(TokenStream lexer) : this(lexer,1)
		{
		}
		
		public RulesEngineParser(ParserSharedInputState state) : base(state,1)
		{
			initialize();
		}
		
	public void document() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST document_AST = null;
		
		try {      // for error handling
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==FACT))
					{
						fact();
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
					}
					else
					{
						goto _loop3_breakloop;
					}
					
				}
_loop3_breakloop:				;
			}    // ( ... )*
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==RULE))
					{
						rule();
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
					}
					else
					{
						goto _loop5_breakloop;
					}
					
				}
_loop5_breakloop:				;
			}    // ( ... )*
			match(Token.EOF_TYPE);
			document_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_0_);
		}
		returnAST = document_AST;
	}
	
	public void fact() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST fact_AST = null;
		
			Relationship r = null;
		
		
		try {      // for error handling
			match(FACT);
			r=factliteral();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			_kb.Add(r);
			match(SEMI);
			fact_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_1_);
		}
		returnAST = fact_AST;
	}
	
	public void rule() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST rule_AST = null;
		
			Rule r = new Rule(_kb);
			string id = null;
			ICondition c = null;
			IAction a = null;
		
		
		try {      // for error handling
			match(RULE);
			id=identifier();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			r.ID = id;
			if (_rules.ContainsKey(r.ID)) { reportError("Rule '" + r.ID + "' already exists"); } else { _rules.Add(r); }
			{ // ( ... )+
				int _cnt12=0;
				for (;;)
				{
					if (((LA(1) >= MATCH && LA(1) <= BINDEXISTS)))
					{
						c=condition(r);
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
						r.AddCondition(c);
					}
					else
					{
						if (_cnt12 >= 1) { goto _loop12_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
					}
					
					_cnt12++;
				}
_loop12_breakloop:				;
			}    // ( ... )+
			match(THEN);
			{ // ( ... )+
				int _cnt14=0;
				for (;;)
				{
					if (((LA(1) >= ASSERT && LA(1) <= STOP)))
					{
						a=action(r);
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
						r.AddAction(a);
					}
					else
					{
						if (_cnt14 >= 1) { goto _loop14_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
					}
					
					_cnt14++;
				}
_loop14_breakloop:				;
			}    // ( ... )+
			match(SEMI);
			rule_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_2_);
		}
		returnAST = rule_AST;
	}
	
	public Relationship  factliteral() //throws RecognitionException, TokenStreamException
{
		Relationship r = null;
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST factliteral_AST = null;
		
			string id = null;
			object o = null;
		
		
		try {      // for error handling
			id=identifier();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			r = new Relationship(id, _kb);
			match(LPAREN);
			{ // ( ... )+
				int _cnt9=0;
				for (;;)
				{
					switch ( LA(1) )
					{
					case IDENT:
					{
						id=identifier();
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
						r.Add(new SetItem(id, _kb));
						break;
					}
					case VARIABLE_IDENT:
					case INT_LIT:
					case FLOAT_LIT:
					case STRING_LIT:
					case TRUE:
					case FALSE:
					{
						o=atom();
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
						r.Add(new SetItem(o, _kb));
						break;
					}
					default:
					{
						if (_cnt9 >= 1) { goto _loop9_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
					}
					break; }
					_cnt9++;
				}
_loop9_breakloop:				;
			}    // ( ... )+
			match(RPAREN);
			factliteral_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_3_);
		}
		returnAST = factliteral_AST;
		return r;
	}
	
	public string  identifier() //throws RecognitionException, TokenStreamException
{
		string s = null;
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST identifier_AST = null;
		IToken  i = null;
		antlr.CommonAST i_AST = null;
		
		try {      // for error handling
			i = LT(1);
			i_AST = (antlr.CommonAST) astFactory.create(i);
			astFactory.addASTChild(ref currentAST, (AST)i_AST);
			match(IDENT);
			s = i.getText();
			identifier_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_4_);
		}
		returnAST = identifier_AST;
		return s;
	}
	
	public object  atom() //throws RecognitionException, TokenStreamException
{
		object o = null;
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST atom_AST = null;
		IToken  n1 = null;
		antlr.CommonAST n1_AST = null;
		IToken  n2 = null;
		antlr.CommonAST n2_AST = null;
		IToken  s1 = null;
		antlr.CommonAST s1_AST = null;
		IToken  v = null;
		antlr.CommonAST v_AST = null;
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case INT_LIT:
			{
				n1 = LT(1);
				n1_AST = (antlr.CommonAST) astFactory.create(n1);
				astFactory.addASTChild(ref currentAST, (AST)n1_AST);
				match(INT_LIT);
				o = Convert.ToInt32(n1.getText());
				atom_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case FLOAT_LIT:
			{
				n2 = LT(1);
				n2_AST = (antlr.CommonAST) astFactory.create(n2);
				astFactory.addASTChild(ref currentAST, (AST)n2_AST);
				match(FLOAT_LIT);
				o = Convert.ToSingle(n2.getText());
				atom_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case STRING_LIT:
			{
				s1 = LT(1);
				s1_AST = (antlr.CommonAST) astFactory.create(s1);
				astFactory.addASTChild(ref currentAST, (AST)s1_AST);
				match(STRING_LIT);
				o = s1.getText().Substring(1, s1.getText().Length - 2);
				atom_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case VARIABLE_IDENT:
			{
				v = LT(1);
				v_AST = (antlr.CommonAST) astFactory.create(v);
				astFactory.addASTChild(ref currentAST, (AST)v_AST);
				match(VARIABLE_IDENT);
				o = new Variable(v.getText());
				atom_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case TRUE:
			{
				antlr.CommonAST tmp9_AST = null;
				tmp9_AST = (antlr.CommonAST) astFactory.create(LT(1));
				astFactory.addASTChild(ref currentAST, (AST)tmp9_AST);
				match(TRUE);
				o = true;
				atom_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case FALSE:
			{
				antlr.CommonAST tmp10_AST = null;
				tmp10_AST = (antlr.CommonAST) astFactory.create(LT(1));
				astFactory.addASTChild(ref currentAST, (AST)tmp10_AST);
				match(FALSE);
				o = false;
				atom_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_5_);
		}
		returnAST = atom_AST;
		return o;
	}
	
	public ICondition  condition(
		Rule rule
	) //throws RecognitionException, TokenStreamException
{
		ICondition c = null;
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST condition_AST = null;
		
			Variable v = null;
			Relationship r = null;
			Expression expr = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case MATCH:
			{
				match(MATCH);
				r=factliteral();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				c = new MatchCondition(rule, r);
				condition_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case NOMATCH:
			{
				match(NOMATCH);
				r=factliteral();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				c = new NoMatchCondition(rule, r);
				condition_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case TEST:
			{
				match(TEST);
				expr=expression(rule);
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				c = new TestCondition(rule, expr);
				condition_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case BINDEXISTS:
			{
				match(BINDEXISTS);
				v=variable_identifier();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				c = new BindExistsCondition(rule, v);
				condition_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_6_);
		}
		returnAST = condition_AST;
		return c;
	}
	
	public IAction  action(
		Rule rule
	) //throws RecognitionException, TokenStreamException
{
		IAction a = null;
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST action_AST = null;
		
			string id = null;
			Variable v = null;
			Relationship r = null;
			Expression expr = null;
			ExpressionNode exprnode = null;
			ExpressionNode rightnode = null;
			ExpressionPath exprpath = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case ASSERT:
			{
				match(ASSERT);
				r=factliteral();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				a = new AssertAction(rule, r);
				action_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case RETRACT:
			{
				match(RETRACT);
				r=factliteral();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				a = new RetractAction(rule, r);
				action_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case BIND:
			{
				match(BIND);
				v=variable_identifier();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				match(LPAREN);
				expr=expression(rule);
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				a = new BindAction(rule, v, expr);
				match(RPAREN);
				action_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case UNBIND:
			{
				match(UNBIND);
				v=variable_identifier();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				a = new UnBindAction(rule, v);
				action_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case EXEC:
			{
				match(EXEC);
				match(LPAREN);
				expr=expression(rule);
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				a = new ExecAction(rule, expr);
				match(RPAREN);
				action_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case EXECASSIGN:
			{
				match(EXECASSIGN);
				exprpath=refassign();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				match(LPAREN);
				rightnode=logexpr();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				match(RPAREN);
				a = new ExecAssignAction(rule, exprpath, rightnode, _engine);
				action_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case FIRERULE:
			{
				match(FIRERULE);
				id=identifier();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				a = new FireRuleAction(rule, _rules, id);
				action_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case STOP:
			{
				match(STOP);
				a = new StopAction(rule);
				action_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_7_);
		}
		returnAST = action_AST;
		return a;
	}
	
	public Expression  expression(
		Rule rule
	) //throws RecognitionException, TokenStreamException
{
		Expression e = new Expression(rule);
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST expression_AST = null;
		
			ExpressionNode expr = null;
		
		
		try {      // for error handling
			expr=logexpr();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			e.Compile(expr, _engine);
			expression_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_8_);
		}
		returnAST = expression_AST;
		return e;
	}
	
	public Variable  variable_identifier() //throws RecognitionException, TokenStreamException
{
		Variable v = null;
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST variable_identifier_AST = null;
		IToken  v1 = null;
		antlr.CommonAST v1_AST = null;
		
		try {      // for error handling
			v1 = LT(1);
			v1_AST = (antlr.CommonAST) astFactory.create(v1);
			astFactory.addASTChild(ref currentAST, (AST)v1_AST);
			match(VARIABLE_IDENT);
			v = new Variable(v1.getText());
			variable_identifier_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_9_);
		}
		returnAST = variable_identifier_AST;
		return v;
	}
	
	public ExpressionPath  refassign() //throws RecognitionException, TokenStreamException
{
		ExpressionPath ep = null;
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST refassign_AST = null;
		
			ExpressionPath ep1 = null;
			string id = null;
		
		
		try {      // for error handling
			match(DOLLAR);
			id=identifier();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			ep = new ExpressionPath(id);
			{ // ( ... )+
				int _cnt19=0;
				for (;;)
				{
					if ((LA(1)==DOT))
					{
						ep1=refassignnode();
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
						ep.Paths.AddLast(ep1);
					}
					else
					{
						if (_cnt19 >= 1) { goto _loop19_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
					}
					
					_cnt19++;
				}
_loop19_breakloop:				;
			}    // ( ... )+
			refassign_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_10_);
		}
		returnAST = refassign_AST;
		return ep;
	}
	
	public ExpressionNode  logexpr() //throws RecognitionException, TokenStreamException
{
		ExpressionNode en = new ExpressionNode();
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST logexpr_AST = null;
		
			ExpressionNode left = null;
			ExpressionNode right = null;
		
		
		try {      // for error handling
			left=strexpr();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			en.Left = left;
			{    // ( ... )*
				for (;;)
				{
					if (((LA(1) >= LOG_AND && LA(1) <= LOG_LT)))
					{
						{
							switch ( LA(1) )
							{
							case LOG_AND:
							{
								antlr.CommonAST tmp30_AST = null;
								tmp30_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp30_AST);
								match(LOG_AND);
								en.Opr = ExpressionOperators.And;
								break;
							}
							case LOG_OR:
							{
								antlr.CommonAST tmp31_AST = null;
								tmp31_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp31_AST);
								match(LOG_OR);
								en.Opr = ExpressionOperators.Or;
								break;
							}
							case LOG_XOR:
							{
								antlr.CommonAST tmp32_AST = null;
								tmp32_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp32_AST);
								match(LOG_XOR);
								en.Opr = ExpressionOperators.Xor;
								break;
							}
							case LOG_EQ:
							{
								antlr.CommonAST tmp33_AST = null;
								tmp33_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp33_AST);
								match(LOG_EQ);
								en.Opr = ExpressionOperators.Eq;
								break;
							}
							case LOG_NE:
							{
								antlr.CommonAST tmp34_AST = null;
								tmp34_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp34_AST);
								match(LOG_NE);
								en.Opr = ExpressionOperators.Ne;
								break;
							}
							case LOG_GE:
							{
								antlr.CommonAST tmp35_AST = null;
								tmp35_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp35_AST);
								match(LOG_GE);
								en.Opr = ExpressionOperators.Ge;
								break;
							}
							case LOG_GT:
							{
								antlr.CommonAST tmp36_AST = null;
								tmp36_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp36_AST);
								match(LOG_GT);
								en.Opr = ExpressionOperators.Gt;
								break;
							}
							case LOG_LE:
							{
								antlr.CommonAST tmp37_AST = null;
								tmp37_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp37_AST);
								match(LOG_LE);
								en.Opr = ExpressionOperators.Le;
								break;
							}
							case LOG_LT:
							{
								antlr.CommonAST tmp38_AST = null;
								tmp38_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp38_AST);
								match(LOG_LT);
								en.Opr = ExpressionOperators.Lt;
								break;
							}
							default:
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							 }
						}
						right=strexpr();
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
						en.Right.Add(right);
					}
					else
					{
						goto _loop25_breakloop;
					}
					
				}
_loop25_breakloop:				;
			}    // ( ... )*
			logexpr_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_11_);
		}
		returnAST = logexpr_AST;
		return en;
	}
	
	public ExpressionPath  refassignnode() //throws RecognitionException, TokenStreamException
{
		ExpressionPath ep = null;
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST refassignnode_AST = null;
		
			ExpressionNode en = null;
			string id = null;
		
		
		try {      // for error handling
			match(DOT);
			id=identifier();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			ep = new ExpressionPath(id);
			refassignnode_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_12_);
		}
		returnAST = refassignnode_AST;
		return ep;
	}
	
	public ExpressionNode  strexpr() //throws RecognitionException, TokenStreamException
{
		ExpressionNode en = new ExpressionNode();
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST strexpr_AST = null;
		
			ExpressionNode left = null;
			ExpressionNode right = null;
		
		
		try {      // for error handling
			left=addexpr();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			en.Left = left;
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==AMPER))
					{
						antlr.CommonAST tmp40_AST = null;
						tmp40_AST = (antlr.CommonAST) astFactory.create(LT(1));
						astFactory.addASTChild(ref currentAST, (AST)tmp40_AST);
						match(AMPER);
						en.Opr = ExpressionOperators.Str;
						right=addexpr();
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
						en.Right.Add(right);
					}
					else
					{
						goto _loop28_breakloop;
					}
					
				}
_loop28_breakloop:				;
			}    // ( ... )*
			strexpr_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_13_);
		}
		returnAST = strexpr_AST;
		return en;
	}
	
	public ExpressionNode  addexpr() //throws RecognitionException, TokenStreamException
{
		ExpressionNode en = new ExpressionNode();
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST addexpr_AST = null;
		
			ExpressionNode left = null;
			ExpressionNode right = null;
		
		
		try {      // for error handling
			left=multexpr();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			en.Left = left;
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==PLUS||LA(1)==MINUS))
					{
						{
							switch ( LA(1) )
							{
							case PLUS:
							{
								antlr.CommonAST tmp41_AST = null;
								tmp41_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp41_AST);
								match(PLUS);
								en.Opr = ExpressionOperators.Add;
								break;
							}
							case MINUS:
							{
								antlr.CommonAST tmp42_AST = null;
								tmp42_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp42_AST);
								match(MINUS);
								en.Opr = ExpressionOperators.Sub;
								break;
							}
							default:
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							 }
						}
						right=multexpr();
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
						en.Right.Add(right);
					}
					else
					{
						goto _loop32_breakloop;
					}
					
				}
_loop32_breakloop:				;
			}    // ( ... )*
			addexpr_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_14_);
		}
		returnAST = addexpr_AST;
		return en;
	}
	
	public ExpressionNode  multexpr() //throws RecognitionException, TokenStreamException
{
		ExpressionNode en = new ExpressionNode();
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST multexpr_AST = null;
		
			ExpressionNode left = null;
			ExpressionNode right = null;
		
		
		try {      // for error handling
			left=unaryexpr();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			en.Left = left;
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==STAR||LA(1)==DIV))
					{
						{
							switch ( LA(1) )
							{
							case STAR:
							{
								antlr.CommonAST tmp43_AST = null;
								tmp43_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp43_AST);
								match(STAR);
								en.Opr = ExpressionOperators.Mul;
								break;
							}
							case DIV:
							{
								antlr.CommonAST tmp44_AST = null;
								tmp44_AST = (antlr.CommonAST) astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, (AST)tmp44_AST);
								match(DIV);
								en.Opr = ExpressionOperators.Div;
								break;
							}
							default:
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							 }
						}
						right=unaryexpr();
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
						en.Right.Add(right);
					}
					else
					{
						goto _loop36_breakloop;
					}
					
				}
_loop36_breakloop:				;
			}    // ( ... )*
			multexpr_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_15_);
		}
		returnAST = multexpr_AST;
		return en;
	}
	
	public ExpressionNode  unaryexpr() //throws RecognitionException, TokenStreamException
{
		ExpressionNode en = new ExpressionNode();
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST unaryexpr_AST = null;
		
			ExpressionNode e = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case LPAREN:
			case DOLLAR:
			case MINUS:
			case LOG_NOT:
			case VARIABLE_IDENT:
			case INT_LIT:
			case FLOAT_LIT:
			case STRING_LIT:
			case TRUE:
			case FALSE:
			{
				{
					{
						switch ( LA(1) )
						{
						case LOG_NOT:
						{
							match(LOG_NOT);
							en.Opr = ExpressionOperators.Not;
							break;
						}
						case MINUS:
						{
							match(MINUS);
							en.Opr = ExpressionOperators.Neg;
							break;
						}
						case LPAREN:
						case DOLLAR:
						case VARIABLE_IDENT:
						case INT_LIT:
						case FLOAT_LIT:
						case STRING_LIT:
						case TRUE:
						case FALSE:
						{
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					e=exprvalue();
					astFactory.addASTChild(ref currentAST, (AST)returnAST);
					en.Right.Add(e);
				}
				unaryexpr_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case ISNULL:
			{
				antlr.CommonAST tmp47_AST = null;
				tmp47_AST = (antlr.CommonAST) astFactory.create(LT(1));
				astFactory.addASTChild(ref currentAST, (AST)tmp47_AST);
				match(ISNULL);
				match(LPAREN);
				e=logexpr();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				match(RPAREN);
				en.Opr = ExpressionOperators.IsNull; en.Right.Add(e);
				unaryexpr_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_16_);
		}
		returnAST = unaryexpr_AST;
		return en;
	}
	
	public ExpressionNode  exprvalue() //throws RecognitionException, TokenStreamException
{
		ExpressionNode en = new ExpressionNode();
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST exprvalue_AST = null;
		
			ExpressionNode left = null;
			object c = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case LPAREN:
			{
				match(LPAREN);
				left=logexpr();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				match(RPAREN);
				en.Left = left;
				exprvalue_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case DOLLAR:
			{
				c=refexpr();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				en.Value = c;
				exprvalue_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			case VARIABLE_IDENT:
			case INT_LIT:
			case FLOAT_LIT:
			case STRING_LIT:
			case TRUE:
			case FALSE:
			{
				c=atom();
				astFactory.addASTChild(ref currentAST, (AST)returnAST);
				en.Value = c;
				exprvalue_AST = (antlr.CommonAST)currentAST.root;
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_16_);
		}
		returnAST = exprvalue_AST;
		return en;
	}
	
	public ExpressionPath  refexpr() //throws RecognitionException, TokenStreamException
{
		ExpressionPath ep = null;
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST refexpr_AST = null;
		
			ExpressionPath ep1 = null;
			string id = null;
		
		
		try {      // for error handling
			match(DOLLAR);
			id=identifier();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			ep = new ExpressionPath(id);
			{ // ( ... )+
				int _cnt43=0;
				for (;;)
				{
					if ((LA(1)==DOT))
					{
						ep1=refexprnode();
						astFactory.addASTChild(ref currentAST, (AST)returnAST);
						ep.Paths.AddLast(ep1);
					}
					else
					{
						if (_cnt43 >= 1) { goto _loop43_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
					}
					
					_cnt43++;
				}
_loop43_breakloop:				;
			}    // ( ... )+
			refexpr_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_16_);
		}
		returnAST = refexpr_AST;
		return ep;
	}
	
	public ExpressionPath  refexprnode() //throws RecognitionException, TokenStreamException
{
		ExpressionPath ep = null;
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		antlr.CommonAST refexprnode_AST = null;
		
			ExpressionNode en = null;
			string id = null;
		
		
		try {      // for error handling
			match(DOT);
			id=identifier();
			astFactory.addASTChild(ref currentAST, (AST)returnAST);
			ep = new ExpressionPath(id);
			{
				switch ( LA(1) )
				{
				case LPAREN:
				{
					match(LPAREN);
					ep.IsMethod = true;
					{
						switch ( LA(1) )
						{
						case LPAREN:
						case DOLLAR:
						case MINUS:
						case LOG_NOT:
						case ISNULL:
						case VARIABLE_IDENT:
						case INT_LIT:
						case FLOAT_LIT:
						case STRING_LIT:
						case TRUE:
						case FALSE:
						{
							en=logexpr();
							astFactory.addASTChild(ref currentAST, (AST)returnAST);
							ep.Args.Add(en);
							{    // ( ... )*
								for (;;)
								{
									if ((LA(1)==COMMA))
									{
										match(COMMA);
										en=logexpr();
										astFactory.addASTChild(ref currentAST, (AST)returnAST);
										ep.Args.Add(en);
									}
									else
									{
										goto _loop48_breakloop;
									}
									
								}
_loop48_breakloop:								;
							}    // ( ... )*
							break;
						}
						case RPAREN:
						{
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					match(RPAREN);
					break;
				}
				case RPAREN:
				case THEN:
				case MATCH:
				case NOMATCH:
				case TEST:
				case BINDEXISTS:
				case DOT:
				case LOG_AND:
				case LOG_OR:
				case LOG_XOR:
				case LOG_EQ:
				case LOG_NE:
				case LOG_GE:
				case LOG_GT:
				case LOG_LE:
				case LOG_LT:
				case AMPER:
				case PLUS:
				case MINUS:
				case STAR:
				case DIV:
				case COMMA:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			refexprnode_AST = (antlr.CommonAST)currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_17_);
		}
		returnAST = refexprnode_AST;
		return ep;
	}
	
	public new antlr.CommonAST getAST()
	{
		return (antlr.CommonAST) returnAST;
	}
	
	private void initializeFactory()
	{
		if (astFactory == null)
		{
			astFactory = new ASTFactory("antlr.CommonAST");
		}
		initializeASTFactory( astFactory );
	}
	static public void initializeASTFactory( ASTFactory factory )
	{
		factory.setMaxNodeType(61);
	}
	
	public static readonly string[] tokenNames_ = new string[] {
		@"""<0>""",
		@"""EOF""",
		@"""<2>""",
		@"""NULL_TREE_LOOKAHEAD""",
		@"""fact""",
		@"""SEMI""",
		@"""LPAREN""",
		@"""RPAREN""",
		@"""rule""",
		@"""then""",
		@"""match""",
		@"""nomatch""",
		@"""test""",
		@"""bindexists""",
		@"""assert""",
		@"""retract""",
		@"""bind""",
		@"""unbind""",
		@"""exec""",
		@"""execassign""",
		@"""firerule""",
		@"""stop""",
		@"""DOLLAR""",
		@"""DOT""",
		@"""and""",
		@"""or""",
		@"""xor""",
		@"""LOG_EQ""",
		@"""LOG_NE""",
		@"""LOG_GE""",
		@"""LOG_GT""",
		@"""LOG_LE""",
		@"""LOG_LT""",
		@"""AMPER""",
		@"""PLUS""",
		@"""MINUS""",
		@"""STAR""",
		@"""DIV""",
		@"""not""",
		@"""isnull""",
		@"""COMMA""",
		@"""an indentifier""",
		@"""a variable_identifier""",
		@"""a number""",
		@"""FLOAT_LIT""",
		@"""a string""",
		@"""true""",
		@"""false""",
		@"""mod""",
		@"""EQUALS""",
		@"""AT""",
		@"""QUESTION""",
		@"""LBRACK""",
		@"""RBRACK""",
		@"""LCURLY""",
		@"""RCURLY""",
		@"""COLON""",
		@"""WS""",
		@"""SL_COMMENT""",
		@"""ML_COMMENT""",
		@"""HEX_DIGIT""",
		@"""ESC"""
	};
	
	private static long[] mk_tokenSet_0_()
	{
		long[] data = { 2L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_0_ = new BitSet(mk_tokenSet_0_());
	private static long[] mk_tokenSet_1_()
	{
		long[] data = { 274L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_1_ = new BitSet(mk_tokenSet_1_());
	private static long[] mk_tokenSet_2_()
	{
		long[] data = { 258L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_2_ = new BitSet(mk_tokenSet_2_());
	private static long[] mk_tokenSet_3_()
	{
		long[] data = { 4193824L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_3_ = new BitSet(mk_tokenSet_3_());
	private static long[] mk_tokenSet_4_()
	{
		long[] data = { 280650338795232L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_4_ = new BitSet(mk_tokenSet_4_());
	private static long[] mk_tokenSet_5_()
	{
		long[] data = { 280650326228608L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_5_ = new BitSet(mk_tokenSet_5_());
	private static long[] mk_tokenSet_6_()
	{
		long[] data = { 15872L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_6_ = new BitSet(mk_tokenSet_6_());
	private static long[] mk_tokenSet_7_()
	{
		long[] data = { 4177952L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_7_ = new BitSet(mk_tokenSet_7_());
	private static long[] mk_tokenSet_8_()
	{
		long[] data = { 16000L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_8_ = new BitSet(mk_tokenSet_8_());
	private static long[] mk_tokenSet_9_()
	{
		long[] data = { 4193888L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_9_ = new BitSet(mk_tokenSet_9_());
	private static long[] mk_tokenSet_10_()
	{
		long[] data = { 64L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_10_ = new BitSet(mk_tokenSet_10_());
	private static long[] mk_tokenSet_11_()
	{
		long[] data = { 1099511643776L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_11_ = new BitSet(mk_tokenSet_11_());
	private static long[] mk_tokenSet_12_()
	{
		long[] data = { 8388672L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_12_ = new BitSet(mk_tokenSet_12_());
	private static long[] mk_tokenSet_13_()
	{
		long[] data = { 1108084801152L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_13_ = new BitSet(mk_tokenSet_13_());
	private static long[] mk_tokenSet_14_()
	{
		long[] data = { 1116674735744L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_14_ = new BitSet(mk_tokenSet_14_());
	private static long[] mk_tokenSet_15_()
	{
		long[] data = { 1168214343296L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_15_ = new BitSet(mk_tokenSet_15_());
	private static long[] mk_tokenSet_16_()
	{
		long[] data = { 1374372773504L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_16_ = new BitSet(mk_tokenSet_16_());
	private static long[] mk_tokenSet_17_()
	{
		long[] data = { 1374381162112L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_17_ = new BitSet(mk_tokenSet_17_());
	
}
}
