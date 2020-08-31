using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using CommonAST = antlr.CommonAST;
using CharBuffer = antlr.CharBuffer;
using RecognitionException = antlr.RecognitionException;
using TokenStreamException = antlr.TokenStreamException;

using KnowledgeEngineRules.Core;

namespace KnowledgeEngineRules.Assembler
{
    class Compiler
    {
        #region Member Variables

        public event CompilerErrorEventHandler Error;
        private TextReader _tr;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Compiler(string code)
            : this(new StringReader(code))
        {
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Compiler(StringReader sr)
            : this((TextReader)sr)
        {
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Compiler(StreamReader sr)
            : this((TextReader)sr)
        {
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Compiler(TextReader tr)
        {
            _tr = tr;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compiles the script
        /// </summary>
        public void Compile(Engine engine)
        {
            try
            {
                RulesEngineLexer lexer = new RulesEngineLexer(new CharBuffer(_tr));
                lexer.setFilename("<code>");

                RulesEngineParser parser = new RulesEngineParser(lexer);
                parser.setFilename("<code>");

                // Parse the input expression
                parser.SetEngine(engine);
                parser.document();
                _knowledgeBase = parser.GetKnowledgeBase();
                _ruleBase = parser.GetRuleBase();
            }
            catch (TokenStreamException e)
            {
                OnError(new CompilerErrorEventArgs(e.Message));
            }
            catch (RecognitionException e)
            {
                OnError(new CompilerErrorEventArgs(e.Message));
            }
            catch (Exception e)
            {
                OnError(new CompilerErrorEventArgs(e.Message));
            }
        }

        private void OnError(CompilerErrorEventArgs e)
        {
            if (Error != null)
            {
                Error(this, e);
            }
            else
            {
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the facts from the parser
        /// </summary>
        public KnowledgeBase KnowledgeBase
        {
            get { return _knowledgeBase; }
        }
        private KnowledgeBase _knowledgeBase;

        /// <summary>
        /// Gets the rules from the parser
        /// </summary>
        public RuleBase RuleBase
        {
            get { return _ruleBase; }
        }
        private RuleBase _ruleBase;

        #endregion
    }
}