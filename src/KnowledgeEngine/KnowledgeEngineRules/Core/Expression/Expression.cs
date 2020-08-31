using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class Expression
    {
        #region Member Variables

        protected ExpressionInterpreter _interpreter;
        protected KnowledgeBase _knowledge;
        protected VariableBase _variables;
        protected Rule _rule;

        #endregion

        #region Constructors

        public Expression(Rule rule)
        {
            _rule = rule;
            _knowledge = _rule.KnowledgeBase;
            _variables = _knowledge.Variables;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compiles the expression
        /// </summary>
        public void Compile(ExpressionNode node, Engine engine)
        {
            _interpreter = new ExpressionInterpreter(_rule);
            _interpreter.Compile(node, engine);
            _root = node;
        }

        /// <summary>
        /// Gets the resulting value of this expression
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            return _interpreter.Run();
        }

        /// <summary>
        /// Runs the expression, but does not return anything
        /// </summary>
        /// <returns></returns>
        public void Exec()
        {
            _interpreter.Exec();
        }

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public bool IsTrue()
        {
            return Convert.ToBoolean(GetValue());
        }

        public override string ToString()
        {
            return _root.ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the root expression node
        /// </summary>
        public ExpressionNode Root
        {
            get { return _root; }
        }
        private ExpressionNode _root;


        #endregion
    }
}
